using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class DevilArmControl : MonoBehaviour
{
    public int health = 100;

    public List<int> DOTtimer = new List<int>();
   
    public void ApplyDOT(int i)
    {
        if(DOTtimer.Count <= 0)
        {
            DOTtimer.Add(i);
            StartCoroutine(DOT());
        }
        else
        {
            DOTtimer.Add(i);
        }
    }

    IEnumerator DOT()
    {
        while (DOTtimer.Count > 0)
        {
            for(int i = 0; i < DOTtimer.Count; i++)
            {
                DOTtimer[i]--; 
            }
            health -= 1;
            DOTtimer.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
