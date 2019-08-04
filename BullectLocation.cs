//총알 날리는 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BullectLocation : MonoBehaviour
{

    public GameObject target;
    public float speed = 7f;
    Rigidbody2D rb;
    
    

    
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player"); 
        GetComponent<Rigidbody2D>();
        Vector2 direction = (target.transform.position - transform.position).normalized * speed;
        Vector3 targetPos = target.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y);
        float angle = Mathf.Atan2(targetPos.x, targetPos.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }
    private void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;

        if (pos.x > 1f) pos.x = 1f;

        if (pos.y < 0f) pos.y = 0f;

        if (pos.y > 1f) pos.y = 1f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.tag=="Ground")
        {
            Destroy(gameObject);
        }
    }
    

}



