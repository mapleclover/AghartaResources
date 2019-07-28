using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSignControl : MonoBehaviour
{
    public SpriteRenderer SRW;

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        int countTime = 0;
        while (countTime < 20)
        {
            if (countTime % 4 < 2)
            {
                SRW.color = new Color32(255, 255, 255, 130);
            }
            else
            {
                SRW.color = new Color32(255, 255, 255, 255);
            }
            yield return new WaitForSeconds(0.1f);
            countTime++;
        }
        Destroy(this.gameObject);
    }
}
