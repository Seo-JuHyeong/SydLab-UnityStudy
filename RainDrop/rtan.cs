using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtan : MonoBehaviour
{
    float direction = 0.0225f;
    float toward = 1.0f;
    // Start is called before the first frame update
    void Start() // �������� ���� �Ͼ�� ��
    {
        
    }

    // Update is called once per frame
    void Update() // �� ������ ���� �Ͼ�� ��
    {   
        if (Input.GetMouseButtonDown(0)) //���콺 Ŭ���� ���� �ٲٱ�
        {
            toward *= -1;
            direction *= -1;
        }
        if (transform.position.x > 2.8f)
        {
            direction = -0.0225f;
            toward = -1.0f;
        }
        if (transform.position.x < -2.8f)
        {
            direction = 0.0225f;
            toward = 1.0f;
        }
        transform.localScale = new Vector3(toward, 1, 1);
        transform.position += new Vector3(direction, 0, 0);
    }
}
