using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceController : MonoBehaviour
{
    public Camera mainCamera;
    public ARRaycastManager raycastManager; // Raycast 사용을 위해 추가
    public GameObject placementIndicator; // AR 평면 위에 가구를 배치할 때 어떤 크기로 어떤 각도로 배치되는지 볼 수 있도록 도와주는 UI

    public GameObject[] prefab; // 가구들을 여러개 받는다.
    private Dictionary<int, GameObject> _instancedPrefab = new Dictionary<int, GameObject>(); // 생성한 모든 오브젝트를 기록하기 위한 Dictionary /
                                                                                              // 새로운 것을 선택했을 때 기존의 선택된 오브젝트를 제거해줘야 하기 때문

    void Update()
    {
        var hits = new List<ARRaycastHit>(); // Raycast의 결과 값을 받아옴
        var center = mainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)); // 현재 스크린의 중간 값
        // View port 값을 Screen Point 값으로 전환 / View port 값은 0 ~ 1 사이로 크기를 표현해주는 것인데 0.5f, 0.5f로 입력해 줌으로써 중앙을 표현
        raycastManager.Raycast(center, hits, TrackableType.PlaneWithinBounds); // Raycast 함수로 center의 결과 값을 반환받아 hits에 저장
        placementIndicator.SetActive(hits.Count > 0); // hits의 저장된 결과 값이 0보다 크면 UI를 띄운다.
        if (hits.Count == 0) return; // hits의 결과 값이 0이면 아래 구문 실행 X 

        var cameraForward = mainCamera.transform.TransformDirection(Vector3.forward); // 방향 벡터를 얻어온다. Vector3.forward = (0, 0, 1)
        var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized; // 0 ~ 1 단위의 값으로 받아온다.
        // placementIndicator의 방향이 항상 카메라의 앞쪽 방향을 향하게 하기위해 cameraBearing 값 선언
        var pose = hits[0].pose; // hits의 pose 값을 저장
        pose.rotation = Quaternion.LookRotation(cameraBearing); // 항상 원하는 각도로 가이드 UI가 방향을 설정해 준다.
        placementIndicator.transform.SetPositionAndRotation(pose.position, pose.rotation); // placementIndicator의 transform 값을 원하는 위치와 각도로 수정!
        
        // 원하는 각도와 위치는 스크린 상의 중앙 값을 3D 좌표 상에 표시하는 것이고 화면 방향은 카메라로 부터 정면 위를 향하게 끔 설정

        if (TouchHelper.Touch3)
        {
            var index = Random.Range(0, prefab.Length); // 가구 종류 랜덤 설정
            var obj = Instantiate(prefab[index], pose.position, pose.rotation, transform); // 가구를 원하는 각도와 위치에 생성 
            obj.SetActive(true); // hide 되어 있는 가구를 보이게 설정
            _instancedPrefab[obj.GetInstanceID()] = obj; // GetInstanceID는 오브젝트마다 가지고 있는 고유한 ID (겹칠 일 X) / 해당 키에 객체 넣기
            RefreshSelection(obj);
        }

        if (Input.touchCount == 0) return;

        if (TouchHelper.IsDown) // 손가락이 눌렸을 때
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(TouchHelper.TouchPosition), out var raycastHits,
                mainCamera.farClipPlane))  // Raycast의 결과가 있을 때
            {
                if (raycastHits.transform.gameObject.tag.Equals("Player")) // 혹시라도 다른 오브젝트가 클릭이 될 수 있기 때문에 tag 값 검사.
                {
                    RefreshSelection(raycastHits.transform.gameObject); // RefreshSelection 함수 실행 (선택한 오브젝트를 매개변수로 넘겨 줌)
                }
            }
        }
    }

    private void RefreshSelection(GameObject select)
    {
        foreach (var obj in _instancedPrefab)
        {
            var furniture = obj.Value.GetComponent<Furniture>(); // 해당 오브젝트의 Furniture class를 가져온다. 
            if (furniture) // Furniture가 있다면
            {
                furniture.IsSelected = obj.Key == select.GetInstanceID(); // 반복문을 도는 오브젝트의 key값과 매개변수로 받은(선택된) 객체의 고유 ID값이 일치하면
                                                                          // 해당 오브젝트의 Furniture의 IsSelected를 "True"로 설정!
            }
        }
    }
    
}
