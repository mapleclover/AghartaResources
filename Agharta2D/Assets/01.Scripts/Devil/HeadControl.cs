using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class HeadControl : MonoBehaviour
{
    public SpriteRenderer Head;
    private bool canAttack = false;
    public GameObject BreathAttack;

    private void Start()
    {
        StartCoroutine(Appear());
    }
    
    IEnumerator Appear()
    {
        for (byte a = 0; a <= 255; a += 4)
        {
            if (a > 250)
                a = 255;
            Head.color = new Color32(255, 255, 255, a);
            if (a == 255) break;
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(this.gameObject, 5.0f);
        canAttack = true;
    }

    private void Update()
    {
        if (canAttack)
        {
            canAttack = false;
            GameObject Breath = Instantiate(BreathAttack) as GameObject;
            Breath.transform.position = new Vector2(0, 3.7f);
        }
    }
}
