using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 사망 시 재생할 오디오 클립
    public AudioClip deathClip;
    // 점프 힘
    public float jumpForce = 700f;

    // 누적 점프 횟수
    private int jumpCount = 0;
    // 바닥에 닿았는지 나타냄
    private bool isGrounded = false;
    // 사망상태
    private bool isDead = false;
    // 사용할 리지드바디 컴포넌트
    private Rigidbody2D playerRigidbody;
    // 사용할 애니메이터 컴포넌트
    private Animator animator;
    // 사용할 오디오 소스 컴포넌트
    private AudioSource playerAudio;

    void Start()
    {
        // 초기화
    }

    void Update()
    {
        // 사용자 입력을 감지하고 점프하는 처리
    }

    private void Die()
    {
        // 사망처리
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았음을 감지하는 처리
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리
    }
}
