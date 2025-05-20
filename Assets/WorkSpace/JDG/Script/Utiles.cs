using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public static class Utiles
    {
        public static void Shuffle(List<Vector2Int> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                int randomNum = Random.Range(i, list.Count);
                Vector2Int temp = list[i];
                list[i] = list[randomNum];
                list[randomNum] = temp;
            }
        }
    }
}
