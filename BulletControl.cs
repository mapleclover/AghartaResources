using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
//총알 생성 스크립트
public class BulletControl : MonoBehaviour
{
    public GameObject Bullet;
    Vector3 targetPos;
    GameObject target;
    bool canFire=true;
    float fire;
    float nextfire;
    float Distance;
    

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Start()
    {
        
    }

    void FixedUpdate()
    {
       Vector3 targetPos = target.transform.position;
        Distance = Vector3.Distance(target.transform.position, transform.position);
        if (Mathf.Abs(Distance) < 5 &&canFire)
        {
            StartCoroutine("Check");
        }
        Destroy(GameObject.Find(Bullet.name + "(Clone)"), 2f);

    }
    IEnumerator Check()
    {
        canFire = false;
        while(Mathf.Abs(Distance)<3)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(Bullet, transform.position, transform.rotation) ;
            yield return new WaitForSeconds(2f);
        }
        canFire = true;
        
    }
   


}

