using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // reload를 위해 필요한 선언
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject rain;
    public GameObject panel;
    public static GameManager I; // I는 싱글톤 화를 의미 (GameManager는 하나여야 한다.)
    public Text scoreText;
    public Text timeText;
    int totalScore;
    float limit = 30f;

    void Awake()
    {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        initGame();
        InvokeRepeating("makeRain", 0, 0.5f);   // 0.5초마다 makeRain 실행
    }

    // Update is called once per frame
    void Update()
    {
        limit -= Time.deltaTime; // 현재 시간을 빼줌
        if (limit < 0)
        {
            Time.timeScale = 0.0f; // Time을 0으로 set
            panel.SetActive(true); // panel을 active 상태로 바꿔줌
            limit = 0.0f; 
        }
        timeText.text = limit.ToString("N2"); // 문자열로 변환해주는 함수 (N2는 소숫점 둘째 자리까지 짤라서 문자열로 바꾸는 것을 의미)
    }

    void makeRain()
    {
        Instantiate(rain); // 틀을 찍는 함수 (rain 실행)
    }

    public void addScore(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    public void retry()
    {
        SceneManager.LoadScene("MainScene"); // MainScene reload
    }

    void initGame() // retry시 값을 모두 초기로 돌려줌
    {
        Time.timeScale = 1.0f; // 시간 초기화
        totalScore = 0; // 점수 초기회
        limit = 30f; 
    }
}
