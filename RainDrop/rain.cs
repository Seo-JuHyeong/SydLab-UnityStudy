using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    int type;
    float size;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-2.7f, 2.7f);
        float y = Random.Range(3f, 5f);
        transform.position = new Vector3(x, y, 0);

        type = Random.Range(1, 5);
        if (type == 1)
        {
            size = 1.2f;
            score = 3;
            GetComponent<SpriteRenderer>().color = new Color(100 / 255f, 100 / 255f, 255 / 255f, 255 / 255f);
        }
        else if (type == 2)
        {
            size = 1.0f;
            score = 2;
            GetComponent<SpriteRenderer>().color = new Color(130 / 255f, 130 / 255f, 255 / 255f, 255 / 255f);
        }
        else if (type == 3)
        {
            size = 0.8f;
            score = 1;
            GetComponent<SpriteRenderer>().color = new Color(150 / 255f, 150 / 255f, 255 / 255f, 255 / 255f);
        }
        else
        {
            size = 0.8f;
            score = -5;
            GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 100 / 255f, 100 / 255f, 255 / 255f);
        }
        transform.localScale = new Vector3(size, size, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll) //collison을 적용한 두 물체가 충돌했을 때 실행되는 함수 이때, 매개변수의 coll은 상대방
    {
        if (coll.gameObject.tag == "ground")
        {
            Destroy(gameObject); // 자기자신을 삭제하는 함수 (rain)
        }

        if (coll.gameObject.tag == "rtan")
        {
            Destroy(gameObject); // 삭제하는 함수
            GameManager.I.addScore(score); // I는 싱글톤 화를 의미
        }
    }
}
