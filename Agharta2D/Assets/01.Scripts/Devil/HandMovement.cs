using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class HandMovement : MonoBehaviour
{
    public float speed;
    public float downY,upY;
    public bool Hit = false;//주먹이 땅에 맞았나?


    void Update()
    {
        if (!Hit)//땅에 맞지 않았다면 
        {
            transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);//주먹을 바닥방향으로 내림
        }
        else//맞았다면
        {
            Invoke("UpMove", 3.0f);//3초뒤 주먹을 위로 올림
        }
            
        
        if (transform.position.y <= downY)//y축 값으로 바닥에 맞았는지 안맞았는지 확인
        {
            Hit = true;
        }
        else if(transform.position.y >= upY)//y축이 일정값이상으로 올라가면 숨기고 바닥에 맞지않았음으로 설정
        {
            Destroy(this.gameObject);
            Hit = false;
        }
    }

    void UpMove()
    {
        transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);
    }
}

 