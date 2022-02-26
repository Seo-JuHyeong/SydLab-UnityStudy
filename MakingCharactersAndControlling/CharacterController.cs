using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CharacterController : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private Animator _anim; // Animator를 받아와서 저장하기 위한 변수
    protected Transform Destination; // 이동시킬 지점
    private Vector3 _lastPosition;
    private float _restTime; // Rest Animation을 제어하기 위한 변수

    void Start()
    {
        _anim = GetComponent<Animator>(); // GetComponent로 animator를 받아옴
        _anim.speed = 1.5f; // Animator의 속도를 조정
        Destination = GameObject.FindWithTag("Player").transform; // Tag값으로 이동시킬 지점을 찾는다.
        _raycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>(); // Tag가 아닌 GameObject이름을 바로 받아와서 찾음
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            _restTime += Time.deltaTime; // deltaTime은 이전 Update가 호출된 시점부터 현재 Update가 호출된 시점까지의 시간 차
            if (_restTime < 3) return; // 휴식 시간을 3초 설정 (3초가 되지 않았다면 return)
            _restTime = 0; // 3초가 지났다면 _resttime = 0 
            _anim.SetBool("Rest", true); // Rest Animation 동작 시작
            return;
        }
        _restTime = 0;
        _anim.SetBool("Rest", false); // touchCount가 있을 경우에는 Rest Animation이 동작하지 않도록 함.

        var hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(TouchHelper.TouchPosition, hits, TrackableType.Planes);
        if (hits.Count == 0) return;
        Destination.transform.position = hits[0].pose.position;
        Rotate(); // 회전하는 함수
        MoveTo(hits[0].pose.position); // 이동하는 함수
    }

    void LateUpdate()
    {
        var delta = Vector3.Distance(transform.position, _lastPosition);
        _lastPosition = transform.position;
        _anim.SetFloat("Speed", delta * 100); // Speed 변수 값 Set
    }

    protected virtual void Rotate()
    {
        var direction = Destination.position - transform.position; // 도착 지점에서 현재 캐릭터의 위치를 빼면 direction의 방향벡터 값이 나온다. (벡터의 연산)
        direction.y = 0; // 수평 평면에서 이동하기 때문에 Y값은 0으로 설정
        transform.rotation = Quaternion.LookRotation(direction); // 현재 캐릭터의 방향을 direction의 방향으로 설정
    }

    protected virtual void MoveTo(Vector3 target) 
    {
        transform.position =
            Vector3.Lerp(transform.position, target, Time.deltaTime); // 자연스러운 움직임을 위해 Lerp 사용
    }
}