using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 게임오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있음
public class GameManager : MonoBehaviour
{
    // 싱글턴을 할당할 전역 변수
    public static GameManager instance;

    public bool isGameover = false; // 게임오버 상태
    public Text scoreText; // 점수 출력 UI 텍스트
    public GameObject gameoverUI; // 게임오버시 활성 오브젝트
    public GameObject menuPanel;

    private int score = 0; // 게임점수

    // 게임 시작과 동시에 싱글턴을 구성
    private void Awake()
    {
        // 싱글턴 변수 instance가 비어 있는가?
        if(instance == null)
        {
            // instance가 비어 있다면(null) 그곳에 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있다면

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미
            // 싱글턴 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.Log("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 게임오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        // 게임오버가 아니라면
        if (!isGameover)
        {
            // 점수를 증가
            score += newScore;
            scoreText.text = "Score : "+score;
        }
    }

    // 플레이어 캐릭터 사망 시 게임오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
        menuPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
