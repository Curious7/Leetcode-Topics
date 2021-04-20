// Subsets 1: Find all subsets of a set.
// The parent set contains unique numbers
// Number of subsets a set of n members will is 2^n

public class Solution {
    public IList<IList<int>> Subsets(int[] nums) 
    {
        var result = new List<IList<int>>();
        backtrack(nums,0,new List<int>(), result);
        return result;
    }
    
    public void backtrack(int[] a, int ind, List<int> list, List<IList<int>> listset)
    {
        listset.Add(new List<int>(list));       
        if (ind == a.Length) return;       
        for (int i=ind; i<a.Length; i++)
        {
            list.Add(a[i]);            
            backtrack(a,i+1,list,listset);
            list.Remove(a[i]);
        }
    }
}
//-----------------------------------------------------------------------------------------------
// Subsets 2: Find all subsets
// Parent set contains duplicate numbers
// Idea: Include the second and onwards duplicate if the previous number is already inlcuded

public class Solution {
    public IList<IList<int>> SubsetsWithDup(int[] nums) 
    {
        Array.Sort(nums);
        var result = new List<IList<int>>();
        backtrack(nums, 0, new List<int>(), result);
        return result;
    }
    
    public void backtrack(int[] a, int ind, List<int> list, List<IList<int>> result)
    {
        result.Add(new List<int>(list));        
        if (ind == a.Length) return;        
        for (int i=ind; i<a.Length; i++)
        {
            if (i > ind && a[i] == a[i-1]) continue;
            list.Add(a[i]);
            backtrack(a,i+1,list,result);
            list.Remove(a[i]);
        }
    }
}
//-----------------------------------------------------------------------------------------------
//Permutations 1: Permutation of unique set of numbers

public class Solution {
    public IList<IList<int>> Permute(int[] a) 
    {
        var result = new List<IList<int>>();
        generate(a, new List<int>(),result);
        return result;
    }
    
    public void generate(int[] a, List<int> list, List<IList<int>> result)
    {
        if (list.Count == a.Length)
        {
            result.Add(new List<int>(list));
            return;
        }      
        for (int i=0; i<a.Length; i++)
        {
            if (list.Contains(a[i])) continue;
            list.Add(a[i]);
            generate(a,list,result);
            list.Remove(a[i]);
        }
    }
}
//-----------------------------------------------------------------------------------------------
//Permutations 2: Permute a set of numbers when it contains duplicates
//strategy same as finding all subsets with duplicate

public class Solution {
    public IList<IList<int>> PermuteUnique(int[] a) 
    {
        Array.Sort(a);
        var result = new List<IList<int>>();
        generate(a, new List<int>(),result, new bool[a.Length]);
        return result;
    }
    
    public void generate(int[] a, List<int> list, List<IList<int>> result, bool[] visited)
    {
        if (list.Count == a.Length)
        {
            result.Add(new List<int>(list));
            return;
        }       
        for (int i=0; i<a.Length; i++)
        {
            if (visited[i] || i>0 && a[i] == a[i-1] && visited[i-1] == false) continue;
            visited[i] = true;
            list.Add(a[i]);
            generate(a,list,result,visited);
            list.RemoveAt(list.Count-1);		// note: line from prev question has been changed from list.Remove(a[i])
            visited[i] = false;
        }
    }
}
//-----------------------------------------------------------------------------------------------
// #39: combination sum [Duplicates allowed]
// find all unique sets of numbers whose sum is equal to target
// The array contains distinct numbers but each number can be used as many number of times in a particular set whose total is the target

public class Solution {
    public IList<IList<int>> CombinationSum(int[] candidates, int target) 
    {
        var result = new List<IList<int>>();
        findcomb(candidates, target, 0 ,result, new List<int>(), 0);
        return result;
    }
    
    public void findcomb(int[] a, int target, int sum, List<IList<int>> result, List<int> temp, int ind)
    {
        if (sum == target)
        {
            result.Add(new List<int>(temp));
            return;
        }             
        for(int i=ind; i<a.Length; i++)
        {
            if (sum+a[i] <= target)
            {
                temp.Add(a[i]);
                findcomb(a,target,sum+a[i],result,temp, i);
                temp.RemoveAt(temp.Count-1);
            }
        }
    }
//-----------------------------------------------------------------------------------------------
// #40: combination sum 2 [Duplicates not allowed]
// Time Complexity: O(2^N)
// Space Complexity: O(N)

public class Solution {
    public IList<IList<int>> CombinationSum2(int[] candidates, int target) 
    {
        Array.Sort(candidates);
        var result = new List<IList<int>>();
        findcomb(candidates, target, 0 ,result, new List<int>(), 0);
        return result;
    }   
    public void findcomb(int[] a, int target, int sum, List<IList<int>> result, List<int> temp, int ind)
    {
        if (sum == target)
        {
            result.Add(new List<int>(temp));
            return;
        }    
        if (sum > target)
            return;       
        for(int i=ind; i<a.Length; i++)
        {
            if (i > ind && a[i] == a[i-1]) continue;
            if (sum+a[i] <= target)
            {
                temp.Add(a[i]);
                findcomb(a,target,sum+a[i],result,temp, i+1);
                temp.RemoveAt(temp.Count-1);
            }
        }
    }
}
//---------------------------------------------------------------------------------------------------
// #131. Palindrome partitioning
// Partition a given string into substrings where each substring is a palindrome
// "aab" ---> ["a", "a", "b"], ["aa", "b"]
// Time Complexity: O(N*2^N) , O(2^N) to generate all substrings and O(N) to generate a substring and check if its a palindrome
// This can further optimized by checking for a palindrome in DP matrix, but time complexity remains same

public class Solution {
    public IList<IList<string>> Partition(string s) 
    {
        var result = new List<IList<string>>();
        findpal(s,0,result,new List<string>());
        return result;
    }  
    public void findpal(string s, int ind, List<IList<string>> result, List<string> temp)
    {
        if (ind == s.Length)
        {
            result.Add(new List<string>(temp));
            return;
        }
        for (int len=1; len<s.Length-ind+1; len++)
        {
            if (IsPalindrome(s.Substring(ind,len)))
            {
                temp.Add(s.Substring(ind,len));
                findpal(s,ind+len,result,temp);
                temp.RemoveAt(temp.Count-1);
            }
        }
    }    
    public bool IsPalindrome(string s)
    {
        int i=0, j=s.Length-1;
        while(i<j)
        {
            if (s[i] != s[j])
                return false;
            i++; j--;
        }      
        return true;
    }
}