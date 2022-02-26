using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenCharacerController : CharacterController // CharacterController 상속
{
    protected override void Rotate() // 오버라이딩
    {
        var direction = Destination.position - transform.position;
        direction.y = 0;
        transform.DORotateQuaternion(Quaternion.LookRotation(direction), 0.5f); // 각도와 시간 값을 입력 (0.5초 만에 회전을 하도록 설정!)
    }

    protected override void MoveTo(Vector3 target) // 오버라이딩
    {
        const float speed = 0.5f; // 1초에 0.5m 이동하도록 설정.
        //time = distance / speed
        transform.DOMove(target, Vector3.Distance(transform.position, target) / speed); // 이동할 위치, 시간(거리 / 속도)
    }
}
