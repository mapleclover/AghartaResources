using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    Vector3 Player_pos;
    Vector3 Enemy_pos;
    int speed;
    bool chasing;
    bool dash;
    
    private void Awake()
    {
        speed = 2;
        chasing = true;
        dash = false;
    }


    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(2f);
        
        if (Enemy_pos.x - Player_pos.x >= 3 || Enemy_pos.x - Player_pos.x <= -3)
            chasing = true;
        else
            dash = true;
    }

    void Move()
    {
        if (Enemy_pos.x - Player_pos.x <= 3 && Enemy_pos.x - Player_pos.x >= -3)
        {
            chasing = false;
            StartCoroutine("DelayTime");
        }
        else
        {
            if (Player_pos.x < Enemy_pos.x)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if (Player_pos.x > Enemy_pos.x)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }

    void Dash()
    {
        speed = 10;


        if (Enemy_pos.x - Player_pos.x <= 2 && Enemy_pos.x - Player_pos.x >= -2)
        {
            speed = 2;
            dash = false;
            StartCoroutine("DelayTime");
        }

        else
        {
            if (Player_pos.x < Enemy_pos.x)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if (Player_pos.x > Enemy_pos.x)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
    
    private void Update()
    {
        Player_pos = Player.transform.position;
        Enemy_pos = this.transform.position;

        if (chasing)
            Move();
        if(dash)
            Dash();

    }
    
}
