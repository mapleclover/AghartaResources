using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class MonsterControl : MonoBehaviour
{
    public int MonsterHP;
    public int ptype;
    public int damage = 1;
    public int count;

    public bool EarthWeakness, IceWeakness, FireWeakness;

    void Update()
    {

        if(MonsterHP <= 0)
        {
            Died();
        }
    }

    void Died()
    {
        //죽는애니메이션
        Destroy(this.gameObject, 1.0f);//죽는 모션후 몬스터 삭제
    }

    public void Damaged()
    {
        MonsterHP -= damage;
    }
}
