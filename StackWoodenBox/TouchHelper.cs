using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchHelper 
{
#if UNITY_EDITOR
    public static bool Touch2 => Input.GetMouseButtonDown(1);
    public static bool IsDown => Input.GetMouseButtonDown(0);
    public static bool IsUp => Input.GetMouseButtonUp(0);
    public static Vector2 TouchPosition => Input.mousePosition;
#else
    public static bool Touch2 => Input.touchCount == 2 && (Input.GetTouch(1).phase == TouchPhase.Began);
    public static bool IsDown => Input.GetTouch(0).phase == TouchPhase.Began;
    public static bool IsUp => Input.GetTouch(0).phase == TouchPhase.Ended;
    public static Vector2 TouchPosition => Input.GetTouch(0).position;
#endif
    public static bool Touch3 => Input.touchCount == 3 && (Input.GetTouch(2).phase == TouchPhase.Began);
    // 객체들을 이동시킬 때 두 개의 손가락을 사용할 것이기 때문에 Event가 겹치지 않게 하기 위해서 Touch3를 추가 구현.
}
