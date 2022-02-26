using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // reload�� ���� �ʿ��� ����
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject rain;
    public GameObject panel;
    public static GameManager I; // I�� �̱��� ȭ�� �ǹ� (GameManager�� �ϳ����� �Ѵ�.)
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
        InvokeRepeating("makeRain", 0, 0.5f);   // 0.5�ʸ��� makeRain ����
    }

    // Update is called once per frame
    void Update()
    {
        limit -= Time.deltaTime; // ���� �ð��� ����
        if (limit < 0)
        {
            Time.timeScale = 0.0f; // Time�� 0���� set
            panel.SetActive(true); // panel�� active ���·� �ٲ���
            limit = 0.0f; 
        }
        timeText.text = limit.ToString("N2"); // ���ڿ��� ��ȯ���ִ� �Լ� (N2�� �Ҽ��� ��° �ڸ����� ©�� ���ڿ��� �ٲٴ� ���� �ǹ�)
    }

    void makeRain()
    {
        Instantiate(rain); // Ʋ�� ��� �Լ� (rain ����)
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

    void initGame() // retry�� ���� ��� �ʱ�� ������
    {
        Time.timeScale = 1.0f; // �ð� �ʱ�ȭ
        totalScore = 0; // ���� �ʱ�ȸ
        limit = 30f; 
    }
}
