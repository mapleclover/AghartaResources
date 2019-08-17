using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class Generator : MonoBehaviour
{
    public Vector2[] GenPos;
    public Vector2[] WarnPos;
    public GameObject DevilHand;
    public GameObject WarningSign;
    public GameObject Magma;
    public GameObject DevilHead;

    void Start()
    {
        //StartCoroutine(HandTrans());
    }

    private void Update()//연습용 키입력
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(HandTrans());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(MagmaTrans());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HeadTrans();
        }
    }

    IEnumerator HandTrans()
    { 
        int x = Random.Range(0, 3);//손 랜덤생성위치
        GameObject Warn = Instantiate(WarningSign) as GameObject;
        Warn.transform.position = WarnPos[x];
        yield return new WaitForSeconds(2.0f);
        GameObject Hand = Instantiate(DevilHand) as GameObject;
        Hand.transform.position = GenPos[x];
    }

    IEnumerator MagmaTrans()
    {
        GameObject Warn = Instantiate(WarningSign) as GameObject;
        Warn.transform.position = WarnPos[3];
        yield return new WaitForSeconds(2.0f);
        GameObject magma = Instantiate(Magma) as GameObject;
        magma.transform.position = GenPos[3];
    }

    void HeadTrans()
    {
        GameObject Head = Instantiate(DevilHead) as GameObject;
        Head.transform.position = GenPos[4];
    }
}
