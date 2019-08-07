using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CrushMonsterControl : MonoBehaviour
{
    public LayerMask enemyMask;
    Rigidbody2D myBody;
    Transform myTrans;
    public GameObject Player;
    Vector3 Player_pos;
    Vector3 Enemy_pos;
    Vector3 dash_pos;
    Vector3 targetPos;
    Vector3 movement;
    float speed;
    float myWidth;
    float Distance;
    bool chasing;
    bool dash;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        chasing = true;
        dash = false;
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;


    }
   
    IEnumerator ChaseDelay()
    {
        dash_pos = Player_pos;
        yield return new WaitForSeconds(2f);
        dash = true;
    }

    IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(1f);
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
        
        targetPos = Player.transform.position;
        Distance = Vector3.Distance(targetPos, myTrans.position);

        if (Mathf.Abs(Distance) < 5)
        {
            chasing = false;
            StartCoroutine("ChaseDelay");
        }
        else
        {
            

            speed = 2;
            Vector2 myVel = myBody.velocity;
            myVel.x = -myTrans.right.x * speed;
            myBody.velocity = myVel;
        }
    }

    void Dash()
    {

        speed = 4;
        Player_pos = Player.transform.position;
        Enemy_pos = myTrans.position;
        Distance = Vector3.Distance(Player_pos, Enemy_pos);

        if(Mathf.Abs(Distance)>=5)
        {
            speed = 0;
            myTrans.Translate(new Vector3(0, 0, 0));
            dash = false;
            StartCoroutine("DashDelay");
        }
        
        if (Player_pos.x < Enemy_pos.x)
        {
            if (Mathf.Abs(Distance) < 2)
            {
                myTrans.Translate(new Vector3(0, 0, 0));
                dash = false;
                StartCoroutine("DashDelay");
            }
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y = 360;
            myTrans.eulerAngles = currRot;
            //myTrans.position = Vector3.MoveTowards(myTrans.position, targetPos, (Speed+1) * Time.deltaTime);
            myTrans.Translate(Vector3.left * Time.deltaTime * (speed + 0.5f));
            Debug.Log("A");
            
        }

        if (Player_pos.x > Enemy_pos.x)
        {
            if (Mathf.Abs(Distance) < 2)
            {
                speed = 0;
                myTrans.Translate(new Vector3(0, 0, 0));
                dash = false;
                StartCoroutine("DashDelay");
            }
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y = -180;
            myTrans.eulerAngles = currRot;
            //myTrans.position = Vector3.MoveTowards(myTrans.position, targetPos, (Speed+1) * Time.deltaTime);
            myTrans.Translate(Vector3.left * Time.deltaTime * (speed + 0.5f));
            Debug.Log("b");
            
        }




    }


    void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isLand = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f, enemyMask);

        Player_pos = Player.transform.position;
        Enemy_pos = this.transform.position;

        
        if (chasing)
        {
            Move();
            if (!isLand || isBlocked)
            {
                Vector3 currRot = myTrans.eulerAngles;
                currRot.y += 180;
                myTrans.eulerAngles = currRot;
            }

        }

        if (dash)
        {
            Dash();
            if(!isLand)
            {
                speed = 0;
            }
        }

    }
}
    

