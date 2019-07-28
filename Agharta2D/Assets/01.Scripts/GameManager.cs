using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject Fire;
    public float checkDistance;
    public LayerMask whatIsGround;
    private float x, y;

    void Awake()
    {
        instance = this;
    }

    public void CreateFire(Vector3 exppos)//폭발위치
    {
        RaycastHit2D hitinfoG = Physics2D.Raycast(exppos, Vector2.down, checkDistance, whatIsGround);
        if(hitinfoG.collider != null)
        {
            GameObject Go = Instantiate(Fire) as GameObject;
            x = exppos.x;
            y = hitinfoG.collider.gameObject.transform.position.y;
            Go.transform.position = new Vector2(x, y);
        }       
    }
}