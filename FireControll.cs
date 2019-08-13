using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControll : MonoBehaviour
{
    public float speed;
    private float distance = 0.5f;
    public LayerMask isLayer;
    void Start()
    {
        Invoke("DestroyFire", 2);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
             DestroyFire();
        }
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right*-1 * speed * Time.deltaTime);
        }
    }

    void DestroyFire()
    {
        Destroy(gameObject);
    }
}
