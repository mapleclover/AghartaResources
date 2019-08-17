using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonsterControl : MonoBehaviour
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
        Vector2 lineCastPos = myTrans.position - myTrans.right * (myWidth - 0.3f);
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * 3);
        bool isLand = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down * 3, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() *.02f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f, enemyMask);

        //땅이 없을 경우,턴 또는 막혔을때, 턴
        if (!isLand || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }
        //항상 앞으로
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * Speed;
        myBody.velocity = myVel;
    }
    private void Update()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);
    }
}
