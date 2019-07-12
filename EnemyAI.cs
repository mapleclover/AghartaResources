using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    Vector3 Player_pos;
    Vector3 Enemy_pos;
    Vector3 dash_pos;
    int speed;
    bool chasing;
    bool dash;
    
    private void Awake()
    {
        speed = 2;
        chasing = true;
        dash = false;
    }


    IEnumerator ChaseDelay()
    {
        dash_pos = Player_pos;
        yield return new WaitForSeconds(1f);
        dash = true;
    }

    IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(2f);
        chasing = true;
        //while (Enemy_pos.x - Player_pos.x <= 3 && Enemy_pos.x - Player_pos.x >= -3)
        //{
        //    Player_pos = Player.transform.position;
        //    Enemy_pos = this.transform.position;
        //    if (Enemy_pos.x - Player_pos.x >= 3 || Enemy_pos.x - Player_pos.x <= -3)
        //        break;
        //    yield return null;
        //}
    }

    void Move()
    {
        if (Enemy_pos.x - Player_pos.x <= 3 && Enemy_pos.x - Player_pos.x >= -3 && PlayerMove.Instance.isGround)
        {
            chasing = false;
            StartCoroutine("ChaseDelay");
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

        
        if (dash_pos.x < Enemy_pos.x)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (dash_pos.x > Enemy_pos.x)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }


        if (Enemy_pos.x - dash_pos.x <= 2 && Enemy_pos.x - dash_pos.x >= -2)
        {
            speed = 2;
            dash = false;
            StartCoroutine("DashDelay");
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
