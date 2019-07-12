using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject Fire;
    private float x, y;

    void Awake()
    {
        instance = this;
    }

    public void CreateFire(Vector3 exppos)//폭발위치
    {
        int i = 0;
        Collider2D[] HitColliders = Physics2D.OverlapCircleAll(exppos, 3f);
        while(i < HitColliders.Length)
        {
            if(HitColliders[i].tag == "Ground")
            {
                GameObject Go = Instantiate(Fire) as GameObject;
                x = exppos.x;
                y = HitColliders[i].transform.position.y;
                Go.transform.position = new Vector2(x,y);
                break;
            }
            i++;
        }       
    }
}