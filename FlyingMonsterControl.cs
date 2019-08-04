using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FlyingMonsterControl : MonoBehaviour
{
    public LayerMask enemyMask;
    public static float Speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth;
    GameObject target;
    Vector3 targetPos;
    float Distance;
    bool isTracking = false;
    int a = 1;
    float curx;
    Vector3 localScale;
    void Start()
    {
        myTrans = this.transform;
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        target = GameObject.FindGameObjectWithTag("Player");
        localScale = transform.localScale;
        curx = transform.localScale.x;
    }

    void FixedUpdate()
    {
        
        /**Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isLand = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f, enemyMask);**/


        //추적조건
        targetPos = target.transform.position;
        Distance = Vector3.Distance(targetPos, myTrans.position);
        Speed = 2f;
        if (Mathf.Abs(Distance) < 3)
        {
            
            isTracking = true;
            if (isTracking)
            {
                if (targetPos.x > myTrans.position.x)
                {
                    
                    transform.localScale = new Vector3(0.7848346f, localScale.y, localScale.z);//방향
                }
                if (targetPos.x < myTrans.position.x)
                {

                    
                    //myTrans.position = Vector3.MoveTowards(myTrans.position, targetPos, (Speed+1) * Time.deltaTime);
                    transform.localScale = new Vector3(-0.7848346f, localScale.y, localScale.z);
                }
                

            }

        }

        else
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y = 0;
            myTrans.eulerAngles = currRot;
            isTracking = false;
            if (!isTracking)
            {
                if (transform.localPosition.x < -7.0f)
                {
                    
                    a = -1;
                    localScale.x *= -1;
                }
                else if (transform.localPosition.x > 7.0f)
                {
                   
                    a = 1;
                    localScale.x *= -1;
                }
                
                transform.localScale = localScale;
                transform.Translate(Vector3.left * Speed * Time.deltaTime * a);
            }
        
            
        }
        

        //땅이 없을 경우,턴 또는 막혔을때, 턴
        /**if ((!isLand && !isTracking) || (isBlocked && !isTracking))
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }**/
        //항상 앞으로



    }
    
    



    /**Animator animator;
    public float movePower = 2f;
    bool isMoving = true;
    int MoveFlag = 0;  //0:normal 1:lft  2:right 
    Vector3 targetPos;
    GameObject target;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
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
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        yield return new WaitForSeconds(1f);

        StartCoroutine("ChangeMovement");//ChangeMovenet 다시 시작
    }

    private void FixedUpdate()
    {
        Move();
    }
    //움직임 설정
    void Move()
    {
        Vector3 MoveVelocity = Vector3.zero;
        string dir = "";
        Vector3 targetPos = target.transform.position;
        float Distance = Vector3.Distance(target.transform.position, transform.position);
        if (Mathf.Abs(Distance) < 4)
        {


            if (targetPos.x < transform.position.x)
            {
                dir = "Left";
                movePower = 0f;

            }
            else if (targetPos.x > transform.position.x)
            {
                dir = "Right";
                movePower = 0f;
            }
        }
        else
        {
            if (MoveFlag == 1)
            {
                dir = "Left";
                movePower = 2f;
            }
            else if (MoveFlag == 2)
            {
                dir = "Right";
                movePower = 2f;
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

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);



        if (pos.x < 0.12f) pos.x = 0.12f;

        if (pos.x > 0.97f) pos.x = 0.97f;

        if (pos.y < 0f) pos.y = 0f;

        if (pos.y > 1f) pos.y = 1f;



        transform.position = Camera.main.ViewportToWorldPoint(pos);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);

    }**/

}


