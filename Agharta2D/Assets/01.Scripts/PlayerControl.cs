using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//유은호
public class PlayerControl : MonoBehaviour
{
    public enum State
    {
        idle,
        run,
        jump,
        throwing,
        hurt,
        climb,
        dead
    }
    public State state = State.idle;

    public float horizontal, vertical; //종횡움직임 감지 변수
    public float speed, jump; //횡움직임 속도 점프 속도 변수
    public float climbSpeed; //종움직임/속도변수
    
    public Rigidbody2D Rigidbody;
    public Animator Anim;
    public SpriteRenderer SR;

    public int hashRun, hashJump, hashThrowing, hashHurt, hashDead;

    public float checkDistance; //사다리 확인 체크
    public LayerMask whatIsLadder,whatIsGround; //사다리,땅 레이어

    public bool canJump = false; //점프 확인 플래그
    public bool facingRight = true; //오른쪽/왼쪽 방향 플래그
    public bool canShoot = true; //공격 가능 확인 플래그
    public bool canMove = true; //이동 가능 확인 플래그
    public bool invinc = false; //무적 시간 적용 플래그
    public bool invincFire = false; //불 도트데미지 적용시 무적시간 플래그
    public bool isDead = false; //생존 플래그
    public bool isClimbing = false; //사다리를 타고있는가

    public int HP = 5;
    public int ammo; //쏠수있는 총알 수
    public float Str = 0.0f;

    public GameObject[] Skill;
    public int skillNumber = 0;
    public int stage = 0;
    Vector3 playerPos;

    public string Door = null;

    void Awake()
    {
        hashRun = Animator.StringToHash("IsMoving");
        hashJump = Animator.StringToHash("IsJumping");
        hashThrowing = Animator.StringToHash("IsThrowing");
        hashHurt = Animator.StringToHash("IsHurt");
        hashDead = Animator.StringToHash("IsDead");
    }
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        StartCoroutine(CheckPCState());
        StartCoroutine(PCAction());
    }

    IEnumerator CheckPCState()
    {
        while (!isDead)
        {
            if (HP <= 0)
            {
                state = State.dead;
            }
            else if (!canMove)
            {
                state = State.hurt;
            }
            else if (!canShoot)
            {
                state = State.throwing;
            }
            else if (isClimbing)
            {
                state = State.climb;
            }
            else if (!canJump)
            {
                state = State.jump;
            }
            else if (Input.GetAxisRaw("Horizontal") != 0 && canJump)
            {
                state = State.run;
            }
            else
            {
                state = State.idle;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }//플레이어 상태체크

    IEnumerator PCAction()
    {
        while (!isDead)
        {
            switch (state)
            {
                case State.idle:
                    Anim.SetBool(hashRun,false);
                    Anim.SetBool(hashJump, false);
                    Anim.SetBool(hashThrowing, false);
                    break;
                case State.run:
                    Anim.SetBool(hashRun, true);
                    break;
                case State.jump:
                    Anim.SetBool(hashJump, true);
                    break;
                case State.throwing:
                    Anim.SetBool(hashThrowing, true);
                    break;
                case State.hurt:
                    Anim.SetTrigger(hashHurt);
                    break;
                case State.dead:
                    Anim.SetTrigger(hashDead);
                    isDead = true;
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }//플레이어 애니메이션

    void Update()
    {        
        if (canMove && !isDead)//움직일수있는지
        {
            if (facingRight && Input.GetAxisRaw("Horizontal") == -1)
            {
                flip();//좌우 반전
            }
            else if (!facingRight && Input.GetAxisRaw("Horizontal") == 1)
            {
                flip();//좌우 반전
            }

            if(Input.GetAxis("Horizontal") != 0)
            {
                PCMove();//좌우 움직임
            }

            RaycastHit2D hitinfoG = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, whatIsGround);//바닥체크
            if (hitinfoG.collider != null)
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }

            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, checkDistance, whatIsLadder);//사다리 체크
            if (hitinfo.collider != null)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    isClimbing = true;
                    canJump = false;
                }
            }
            else
            {
                if(Input.GetAxisRaw("Horizontal") != 0 && hitinfo.collider == null)
                {
                    isClimbing = false;
                }   
            }
            
            if(isClimbing && hitinfo.collider != null)//사다리를 타는중이면 중력 0
            {
                Rigidbody.velocity = Vector2.zero;
                Rigidbody.gravityScale = 0;
                PCClimb();
            }
            else//사다리에서 벗어나오면 중력 되돌림
            {
                Rigidbody.gravityScale = 1;
            }

            if (canJump && Input.GetKeyDown(KeyCode.LeftAlt))//점프가능한지? + 점프키 확인
            {
                Rigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && canShoot && ammo > 0 && !isClimbing)//스킬사용
            {
                StartCoroutine(ThrowPower());
            }            

            if (Input.GetKeyDown(KeyCode.LeftShift))//스킬변경
            {
                PCSkill();
            }
        }
        else if(isDead)
        {
            PCDied();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)//몬스터 혹은 피격시 HP감소와 밀려남
    {
        if (collision.gameObject.tag == "Enemy" && !invinc)//몬스터 피격시 HP 1감소
        {
            PCDamaged(collision.gameObject);
        }
    }

    void PCClimb()//사다리 올라가는 모션
    {
        vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime * climbSpeed;
        transform.Translate(0.0f, vertical, 0.0f);
    }

    void PCDamaged(GameObject gameObject)
    {
        HP -= 1;

        if (HP <= 0)//피격후 사망시 피격 넉백 없이 즉시사망
        {
            Rigidbody.velocity = new Vector2(0, 0);
            return;
        }
        else if (gameObject.transform.position.x >= this.transform.position.x)//몬스터한테 피격받은 방향확인
        {
            Rigidbody.velocity = new Vector2(0, 0);
            Rigidbody.AddForce(new Vector2(-100, 100));
            invinc = true;
            StartCoroutine(invincible());
        }
        else
        {
            Rigidbody.velocity = new Vector2(0, 0);
            Rigidbody.AddForce(new Vector2(100, 100));
            invinc = true;
            StartCoroutine(invincible());
        }
    }

    IEnumerator invincible()//피격후 무적시간
    {
        int countTime = 0;
        canMove = false;
        while (countTime < 10)
        {                        
            if(countTime == 7)
            {
                canMove = true;
            }
            if(countTime%2 == 0)
            {
                SR.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                SR.color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.1f);
            countTime++;
        }
        SR.color = new Color32(255, 255, 255, 255);

        invinc = false;
        StartCoroutine(PCAction());
    }

    void OnCollisionStay2D(Collision2D collision)//적이랑 계속붙어있으면 무적시간이 풀렸을때 데미지입게하기
    {
        if(collision.gameObject.tag == "Enemy" && !invinc)
        {
            PCDamaged(collision.gameObject);
        }
    }

    void flip()//캐릭 방향전환
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void PCMove()//좌우 움직임
    {
        horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        this.transform.Translate(horizontal, 0.0f, 0.0f);
    }

    void PCDied()//플레이어 사망시
    {
        Debug.Log("PC Died!");
    }

    IEnumerator ThrowPower()
    {
        canShoot = false;
        while (Input.GetKey(KeyCode.LeftControl))
        {
            Str += Time.deltaTime * 2.0f;
            Debug.Log(Str);
            if (Str > 1f)
            {
                Str = 1.0f;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(PCAttack(skillNumber));
    }//컨트롤키를 오래누르면 멀리나감

    IEnumerator PCAttack(int skilltype)//플레이어 공격시
    {

        yield return new WaitForSeconds(0.3f);//공격모션후 날라가는 딜레이
        if (!isDead && canMove)
        {
            ammo -= 1;
            GameObject potion = Instantiate(Skill[skilltype]) as GameObject;
            playerPos = this.transform.position;
            if (facingRight)
            {
                playerPos.x += 0.2f;
            }
            else
            {
                playerPos.x -= 0.2f;
            }

            playerPos.y += 1.0f;
            potion.transform.position = playerPos;
        }
        Str = 0.0f;
        canShoot = true;
    }

    void PCSkill()//플레이어 스킬변경
    {
        skillNumber++;
        if(skillNumber > stage)//스테이지 클리어에 따른 스킬 가능 증가(stage 변수)
        {
            skillNumber = 0;
        }
    }
}