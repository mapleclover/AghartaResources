using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class HandHP : MonoBehaviour
{
    public static int health = 50;

    public List<int> DOTtimer = new List<int>();

    public void ApplyDOT(int i)
    {
        if (DOTtimer.Count <= 0)
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
            for (int i = 0; i < DOTtimer.Count; i++)
            {
                DOTtimer[i]--;
            }
            health -= 1;
            Debug.Log(health);
            DOTtimer.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDisable()
    {
        DOTtimer.Clear();
    }
    void OnDestroy()
    {
        DOTtimer.Clear();
    }
}