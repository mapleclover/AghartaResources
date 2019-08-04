using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//왕환민
public class MonsterControl : MonoBehaviour
{
    public LayerMask enemyMask;
    public float Speed;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth;

    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isLand = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2()*.02f);
        bool isBlocked= Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2()*.02f, enemyMask);

        //땅이 없을 경우,턴 또는 막혔을때, 턴
        if (!isLand || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }
        //항상 앞으로
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x*Speed;
        myBody.velocity = myVel;
    }
    private void Update()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);
    }
}
/**{
    Animator animator; 
    public float movePower = 1f;
    Vector3 movement;
    private bool isMoving = true;
    int movementFlag = 0;  //0:normal 1:lft  2:right 
    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>(); 

        StartCoroutine("ChangeMovement");
    }
    
    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag ==0)
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

        StartCoroutine("ChangeMovement");//ChangeMovenet 다시 시작
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundinfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
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

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);







    }

    

   
}
Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);



        if (pos.x< 0.12f) pos.x = 0.12f;

        if (pos.x > 0.97f) pos.x = 0.97f;

        if (pos.y< 0f) pos.y = 0f;

        if (pos.y > 1f) pos.y = 1f;



        transform.position = Camera.main.ViewportToWorldPoint(pos);**/