using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DigMonsterControl : MonoBehaviour
{
    public LayerMask enemyMask;
    public float Speed;
    Rigidbody2D myBody;
    Transform myTrans;
    GameObject target;
    Vector3 targetPos;
    float Distance;
    float myWidth;
    float Gposy;
    bool isDig = true;
    GameObject Ground;
    Vector3 current;
    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        target = GameObject.FindGameObjectWithTag("Player");
        Ground = GameObject.FindGameObjectWithTag("Hide");
        current = transform.position;
        Gposy = Ground.transform.position.y;
    }

    void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isLand = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f, enemyMask);

        //시작부터 땅에 들어가 있음
        //플레이어가 가까이 온 경우

        targetPos = target.transform.position;
        Distance = Vector3.Distance(targetPos, myTrans.position);
        if (Mathf.Abs(Distance) < 3)
        {
            
            isDig = false;
            Invoke("TurnOn", 1);
            if (targetPos.x > myTrans.position.x)
            {
                if (!isLand)
                {
                    myTrans.Translate(new Vector3(0, 0, 0));
                }
                else if (isLand)
                {
                    Vector3 currRot = myTrans.eulerAngles;
                    currRot.y = 180;
                    myTrans.eulerAngles = currRot;
                    //myTrans.position = Vector3.MoveTowards(myTrans.position, targetPos, (Speed+1) * Time.deltaTime);
                    myTrans.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * (Speed + 0.5f));
                }

            }

            if (targetPos.x < myTrans.position.x)
            {
                if (!isLand)
                {
                    myTrans.Translate(new Vector3(0, 0, 0));
                }
                else if (isLand)
                {
                    Vector3 currRot = myTrans.eulerAngles;
                    currRot.y = -360;
                    myTrans.eulerAngles = currRot;
                    //myTrans.position = Vector3.MoveTowards(myTrans.position, targetPos, (Speed+1) * Time.deltaTime);
                    myTrans.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * (Speed + 0.5f));
                }
            }
        }
        //플레이어가 멀어진 경우
        else
        {

            if(isDig)
            {
                current.y = Gposy + 0.5f;
                current.x = myTrans.position.x;
                myTrans.position = current;
            }
            
  
            Invoke("TurnOff", 2);
            Speed = 0;
        }


        
        //땅이 없을 경우,턴 또는 막혔을때, 턴
        if ((!isLand && !isDig) || (isBlocked && !isDig))
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        /**else if(!isDig)
        {
            Vector2 myVel = myBody.velocity;
            myVel.x = -myTrans.right.x * Speed;
            myBody.velocity = myVel;
        }**/

    }

    void TurnOn()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
        isDig = false;
    }

    void TurnOff()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        isDig = true;
    }
    


    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        


        if (pos.x < 0.12f) pos.x = 0.12f;

        if (pos.x > 0.97f) pos.x = 0.97f;

        if (pos.y < 0.05f) pos.y = 0.05f;

        if (pos.y > 1f) pos.y = 1f;



        transform.position = Camera.main.ViewportToWorldPoint(pos);
        





        /**Animator animator;
        bool isMoving = true;
        int MoveFlag = 0;
        public float movePower = 2f;
        Vector3 targetPos;
        GameObject target;

        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        void Start()

        {

            animator = gameObject.GetComponentInChildren<Animator>();
            StartCoroutine("ChangeMovement");
        }
        IEnumerator ChangeMovement()
        {
            MoveFlag = Random.Range(0, 3);
            if (MoveFlag == 0)
            {
                animator.SetBool("isMoving", false);
                movePower = 0f;
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
            yield return new WaitForSeconds(1f);
            StartCoroutine("ChangeMovement");
        }
        private void FixedUpdate()
        {
            move();
        }

        void move()
        {
            Vector3 MoveVelocity = Vector3.zero;
            string dir = "";
            Vector3 targetPos = target.transform.position;
            //trace or random
            float Distance = Vector3.Distance(target.transform.position, transform.position);
            if (Mathf.Abs(Distance) < 4)//일정거리가 되면 작동
            {
                if(Mathf.Abs(Distance)<2)
                {
                    Invoke("TurnOn", 1);
                }

                if (targetPos.x < transform.position.x)
                {
                    dir = "Left";
                }
                else if (targetPos.x > transform.position.x)
                {
                    dir = "Right";
                }
            }
            else
            {
                Invoke("TurnOff", 1);
                if (MoveFlag == 1)
                {
                    dir = "Left";
                }
                else if (MoveFlag == 2)
                {
                    dir = "Right";
                }
               
            }

            if (dir == "Left")
            {
                MoveVelocity = Vector3.left;
                transform.localScale = new Vector3(0.96995f, 1.50865f, 1);//방향
            }
            else if (dir == "Right")
            {
                MoveVelocity = Vector3.right;
                transform.localScale = new Vector3(-0.96995f, 1.50865f, 1);//방향
            }

            transform.position += MoveVelocity * movePower * Time.deltaTime;
        }
        void TurnOn()
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        void TurnOff()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        void Update()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);



            if (pos.x < 0.12f) pos.x = 0.12f;

            if (pos.x > 0.97f) pos.x = 0.97f;

            if (pos.y < 0.05f) pos.y = 0.05f;

            if (pos.y > 1f) pos.y = 1f;



            transform.position = Camera.main.ViewportToWorldPoint(pos);

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);
        }**/
    }
}
