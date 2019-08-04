using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class TrackingMonsterControl : MonoBehaviour
{
    public LayerMask enemyMask;
    public float Speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth;
    GameObject target;
    Vector3 targetPos;
    float Distance;
    bool isTracking = false;

    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isLand = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f, enemyMask);

        //추적조건
        targetPos = target.transform.position;
        Distance = Vector3.Distance(targetPos, myTrans.position);
        
        if (Mathf.Abs(Distance) < 2.5f)
        {
            isTracking = true;
            if(isTracking)
            {
                if (targetPos.x > myTrans.position.x)
                {
                    if (!isLand)
                    {
                        myTrans.Translate(new Vector3(0, 0, 0));
                    }
                    else if(isLand)
                    {
                        Vector3 currRot = myTrans.eulerAngles;
                        currRot.y = 180;
                        myTrans.eulerAngles = currRot;
                        //myTrans.position = Vector3.MoveTowards(myTrans.position, targetPos, (Speed+1) * Time.deltaTime);
                        myTrans.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * (Speed + 0.5f));
                    }
                    
                }
                if(targetPos.x<myTrans.position.x)
                {
                    if (!isLand)
                    {
                        myTrans.Translate(new Vector3(0, 0, 0));
                    }
                    else if(isLand)
                    {
                        Vector3 currRot = myTrans.eulerAngles;
                        currRot.y = -360;
                        myTrans.eulerAngles = currRot;
                        //myTrans.position = Vector3.MoveTowards(myTrans.position, targetPos, (Speed+1) * Time.deltaTime);
                        myTrans.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * (Speed + 0.5f));
                    }
                    
                }
                
                
            }
            
        }
        
        else
        {
            isTracking = false;
        }


            //땅이 없을 경우,턴 또는 막혔을때, 턴
            if ((!isLand&&!isTracking) || (isBlocked && !isTracking))
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }
        //항상 앞으로
        if(!isTracking)
        {
            Vector2 myVel = myBody.velocity;
            myVel.x = -myTrans.right.x * Speed;
            myBody.velocity = myVel;
        }
        
    }
    private void Update()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);
    }











}

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    /**{

    Animator animator;
    bool isMoving = true;
    int MoveFlag = 0;
    Vector3 targetPos;
    GameObject target;
    string dir;
    float Distance;
    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;
    bool tracking=false;

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
            speed = 0f;

        }
        else
        {
            animator.SetBool("isMoving", true);
            speed = 2f;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("ChangeMovement");
    }
    private void FixedUpdate()
    {
        if(tracking==true)
        {
            move();
            Debug.Log("A");
        }
        else if(tracking==false)
        {
            go();
            Debug.Log("B");
        }

        
       
    }

    void move()
    {
        Vector3 MoveVelocity = Vector3.zero;
        dir = "";
        Vector3 targetPos = target.transform.position;
        //trace or random
        float Distance = Vector3.Distance(target.transform.position, transform.position);
        if (Mathf.Abs(Distance) < 4)//일정거리가 되면 작동
        {
            MoveFlag = Random.Range(1, 3);
            tracking = true;
            if (targetPos.x < transform.position.x)
            {
                dir = "Left";
            }
            else if (targetPos.x > transform.position.x)
            {
                dir = "Right";
            }

        }
        else if(Mathf.Abs(Distance) >= 4)
        {
            tracking = false;
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

        transform.position += MoveVelocity * speed * Time.deltaTime;
    }
    void go()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundinfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);

        if (groundinfo.collider == false)
        {

            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;

                
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                
            }
            
        }
        else if (Mathf.Abs(Distance) < 4)
        {
            tracking = true;
        }
    }


    //화면밖 못 나가게
    void Update()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x < 0.12f) pos.x = 0.12f;

            if (pos.x > 0.97f) pos.x = 0.97f;

            if (pos.y < 0f) pos.y = 0f;

            if (pos.y > 1f) pos.y = 1f;

            transform.position = Camera.main.ViewportToWorldPoint(pos);

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);
        }
}**/



