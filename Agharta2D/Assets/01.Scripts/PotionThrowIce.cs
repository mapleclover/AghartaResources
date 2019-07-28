using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class PotionThrowIce : MonoBehaviour
{
    public GameObject PC;
    public GameObject GM;
    public Rigidbody2D rgd;
    public int type;
    public float throwPower;
    public float throwAngle;
    private int dir;
    public float speed;
    public float Power;

    void Awake()
    {
        PC = GameObject.Find("PC");//캐릭터 오브젝트 가져오기
        GM = GameObject.Find("GameManager");//소리 재생에 필요한 스크립트 불러올 곳
        type = PC.GetComponent<PlayerControl>().skillNumber;
        Power = PC.GetComponent<PlayerControl>().Str;
        if (PC.GetComponent<PlayerControl>().facingRight)//캐릭터 방향확인
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        speed = PC.GetComponent<PlayerControl>().speed;
        rgd = GetComponent<Rigidbody2D>();
        throwAngle = throwAngle / 180.0f * Mathf.PI;//발사각도를 레디언 각도로 변경
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GM.GetComponent<SoundControl>().SoundGlassBreak();
            if (collision.GetComponent<MonsterControl>() != null)
            {
                if (collision.gameObject.GetComponent<MonsterControl>().IceWeakness == true)
                {
                    collision.gameObject.GetComponent<MonsterControl>().Damaged();
                }
                collision.gameObject.GetComponent<MonsterControl>().Damaged();
            }
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            GM.GetComponent<SoundControl>().SoundGlassBreak();
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        rgd.AddForce(Power * new Vector2((throwPower * Mathf.Cos(throwAngle) + speed * 0.5f * Mathf.Abs(Input.GetAxis("Horizontal"))) * dir, throwPower * Mathf.Sin(throwAngle)), ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.Rotate(0, 0, -3.0f);
        if (this.transform.position.y < -2)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        PC.GetComponent<PlayerControl>().ammo++;
    }
}