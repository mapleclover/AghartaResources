using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//유은호
public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed;
    public Vector3 offset;

    void LateUpdate()
    {
        if (!target.GetComponent<PlayerControl>().isDead)//캐릭터가 생존중이라면 카메라가 따라감
        {
            CameraMove();
        }
    }

    void CameraMove()
    {
        Vector3 desiredPosition = target.transform.position + offset;//오프셋만큼 캐릭터와 떨어짐
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);//천천히 따라가는 카메라
        transform.position = smoothedPosition;
    }
}