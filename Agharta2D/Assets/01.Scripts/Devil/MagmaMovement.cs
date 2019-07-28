using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaMovement : MonoBehaviour
//유은호
{
    public float speed;
    public float downY, upY;
    public bool Raise = false;//용암이 최대치까지 솟았나?


    void Update()
    {
        if (!Raise)//최대치까지 솟지않았다면
        {
            transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);//용암을 올림
        }
        else//솟았다면
        {
            Invoke("DownMove", 1.0f);//1초뒤 용암을 내림
        }


        if (transform.position.y >= upY)//y축 값으로 최대치와 비교
        {
            Raise = true;
        }
        else if (transform.position.y <= downY)//y축이 일정값이상으로 내려가면 이동을 멈춤
        {
            Destroy(this.gameObject);
            Raise = false;
        }
    }

    void DownMove()
    {
        transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().HP = 0;
        }
    }
}

