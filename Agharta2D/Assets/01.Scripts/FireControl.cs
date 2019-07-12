using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class FireControl : MonoBehaviour
{
    public GameObject Enemy;
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DevilArmControl>() != null)
        {
            collision.GetComponent<DevilArmControl>().ApplyDOT(5);
        }
    }
}