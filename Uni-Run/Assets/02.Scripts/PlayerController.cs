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
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead)
        {
            // 사망 시 처리를 더 이상 진행하지 않고 종료
            return;
        }

        // 마우스 왼쪽 버튼을 눌었으며 && 최대 점프 횟수(2)에 도달하지 않았다면
        if(Input.GetMouseButtonDown(0)&&jumpCount < 2)
        {
            // 점프 횟수 증가
            jumpCount++;
            // 점프 직전에 속도를 순각적으로 제로(0,0)로 변경
            playerRigidbody.velocity = Vector2.zero;
            // 리지드바디에 위쪽으로 힘 주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // 오디오 소스 재생
            playerAudio.Play();
        }
        else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면(위로 상승 중)
            // 현재 속도를 절반으로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // 애니메이터의 Grounded파라미터를 isGrounded 값으로 갱신
        animator.SetBool("Grounded", isGrounded);
       
    }

    private void Die()
    {
        // 사망처리
        // 애니메이터의 Die 트리거 파라미터를 셋
        animator.SetTrigger("Die");

        // 오디오 소스에 할당된 오디오 클립을 deathClip으로 변경
        playerAudio.clip = deathClip;
        // 사망 효과음 재생
        playerAudio.Play();

        // 속도를 제로(0,0)로 변경
        playerRigidbody.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;

        // 게임 매니저의 게임오버 처리 실행
        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if(collision.tag == "Dead" && !isDead)
        {
            // 충돌한 상대방의 태그가 Dead이며 아직 사망하지 않았다면
            Die();
        }
        else if(collision.tag == "Thorn" && !isDead)
        {
            if (GameManager.instance.Crash() == true) Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿았음을 감지하는 처리
        // 어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있으면
        if(collision.contacts[0].normal.y > 0.7f)
        {
            // isGrounded를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리
        // 어떤 콜라이더에서 떼어진 경우 isGrounded를 false로 변경
        isGrounded = false;
    }
}
