using System.Collections.Generic;
using UnityEngine;

public class RandPerm
{
    public List<int> RandomPerm(int size)
    {
        List<int> numberList = new List<int>();
        for (int i = 0; i <= size; i++)
        {
            numberList.Add(i);
        }

        int index = size;
        
        while (index > 1)
        {
            index--;
            int j = Random.Range(0, index + 1);
            int temp = numberList[j];
            numberList[j] = numberList[index];
            numberList[index] = temp;
        }

        numberList.RemoveAt(numberList.Count -1);

        return numberList;
    }

}
