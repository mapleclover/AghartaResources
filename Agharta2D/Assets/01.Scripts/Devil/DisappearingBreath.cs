using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBreath : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().HP = 0;
        }
    }
}
