using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float frequency = 20f;

    [SerializeField]
    float magnitude = 0.5f;

    bool facingRight = true;
    GameObject target;
    Vector3 Pos, localScale, Pos1;
    Vector3 targetPos;
    bool MovingBack = false;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Pos = transform.position;

        localScale = transform.localScale;
    }

    void Update()
    {
        CheckWhereToForce();

        targetPos = target.transform.position;
        float Distance = Vector3.Distance(targetPos, transform.position);

        if (Mathf.Abs(Distance) <= 3)
        {

            if (targetPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(0.96995f, 1.50865f, 1);
                Vector3 MoveVelocity = Vector3.right;
                transform.position = Vector3.Lerp(transform.position, targetPos, (moveSpeed - 3) * Time.deltaTime);

            }
            else if (targetPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(-0.96995f, 1.50865f, 1);
                Vector3 MoveVelocity = Vector3.left;
                transform.position = Vector3.Lerp(transform.position, targetPos, (moveSpeed - 3) * Time.deltaTime);
            }

        }
        else if (Mathf.Abs(Distance) > 3)
        {
            if (facingRight)
            {
                MoveRight();
                Debug.Log("C");

            }
            else if (!facingRight)
            {
                MoveLeft();
                Debug.Log("D");

            }

        }

    }
    void CheckWhereToForce()
    {
        if (Pos.x < -7f)
        {
            facingRight = true;
        }
        else if (Pos.x > 7f)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    void MoveRight()
    {
        Pos += transform.right * Time.deltaTime * (moveSpeed-2);
        transform.position = Pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        
    }
    void MoveLeft()
    {
        Pos -= transform.right * Time.deltaTime * (moveSpeed-2);
        transform.position = Pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;

    }
    void Back()
    {
        transform.position = Vector3.MoveTowards(transform.position, Pos, moveSpeed * Time.deltaTime);
        Debug.Log("H");
        
    }
    void GetBack()
    {
        MovingBack = true;
        
    }
    void Down()
    {
        MovingBack = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="Player")
        {
            Invoke("Back", 2);
        }
    }
}

