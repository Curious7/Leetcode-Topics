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

