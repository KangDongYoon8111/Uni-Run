using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // 장애물 오브젝트들 담을 변수를 지정
    public GameObject[] obstacles;
    // 플레이어 캐릭터가 밟았는지
    private bool stepped = false;

    // 컴포넌트가 활성화될 때마다 매번 실행되는 메서드
    private async void OnEnable()
    {
        // 발판을 리셋하는 처리
        stepped = false;

        // 장애물의 수만큼 루프
        for (int i = 0; i < obstacles.Length; i++)
        {
            // 현재 순번의 장애물을 1/3의 확률로 활성화
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 플레이어 캐릭터가 자신을 밞았을 때
        // 점수를 추가하는 처리
        // 충돌한 상대방의 태그가 Player이고,
        // 이전에 플레이어 캐릭터가 밟지 않았다면,
        if(other.collider.tag == "Player"&&!stepped){
            // 점수를 추가하고 밟힘 상태를 참으로 변경
            stepped = true;
            GameManager.instance.AddScore(1);
        }

    }
}
