using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    // 생성할 발판의 원본 프리팹
    public GameObject platformPrefab;
    // 생성할 발판 수
    public int count = 3;

    // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMin = 1.25f;
    // 다음 배치까지의 시간 간격 최대값
    public float timeBetSpawnMax = 2.25f;
    // 실 다음 배치까지의 시간 간격
    private float timeBetSpawn;

    // 배치할 위치의 최소 y값
    public float yMin = -3.5f;
    // 배치할 위치의 최대 y값
    public float yMax = 1.5f;
    // 배치할 위치의 x값
    private float xPos = 20f;

    // 미리 생성한 발판들
    private GameObject[] platforms;
    // 사용할 현재 순번의 발판
    private int currentIndex = 0;

    // 초반에 생성한 발판을 화면 밖에 숨겨둘 위치
    private Vector2 poolPosition = new Vector2(0, -25);
    // 마지막 배치 시점
    private float lastSpawnTime;

    void Start()
    {
        // 변수를 초기화하고 사용할 발판을 미리 생성
        // count만큼의 공간을 가지는 새로운
        // 발판 배열 생성.
        platforms = new GameObject[count];

        // count만큼 루프하면서 발판 생성
        for(int i = 0; i < count; i++)
        {
            // platformPrefab을 원본으로 새 발판을
            // poolPosition 위치에 복제 생성
            // 생성된 발판을 platform 배열에 할당
            platforms[i] = Instantiate(platformPrefab, 
                poolPosition, Quaternion.identity);
        }
        // 마지막 배치 시점, 다음번 배치 초기화
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    void Update()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치
        // 게임오버 상태에서 더 이상 동작하지 않게끔.
        if (GameManager.instance.isGameover) return;

        // 마지막 배치 시점에서 timeBetSpawn 이상 시간이
        // 흘렀다면...
    }
}
