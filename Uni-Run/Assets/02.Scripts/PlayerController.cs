using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��� �� ����� ����� Ŭ��
    public AudioClip deathClip;
    // ���� ��
    public float jumpForce = 700f;

    // ���� ���� Ƚ��
    private int jumpCount = 0;
    // �ٴڿ� ��Ҵ��� ��Ÿ��
    private bool isGrounded = false;
    // �������
    private bool isDead = false;
    // ����� ������ٵ� ������Ʈ
    private Rigidbody2D playerRigidbody;
    // ����� �ִϸ����� ������Ʈ
    private Animator animator;
    // ����� ����� �ҽ� ������Ʈ
    private AudioSource playerAudio;

    void Start()
    {
        // �ʱ�ȭ
        // ���� ������Ʈ�κ��� ����� ������Ʈ���� ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ����� �Է��� �����ϰ� �����ϴ� ó��
        if (isDead)
        {
            // ��� �� ó���� �� �̻� �������� �ʰ� ����
            return;
        }

        // ���콺 ���� ��ư�� �������� && �ִ� ���� Ƚ��(2)�� �������� �ʾҴٸ�
        if(Input.GetMouseButtonDown(0)&&jumpCount < 2)
        {
            // ���� Ƚ�� ����
            jumpCount++;
            // ���� ������ �ӵ��� ���������� ����(0,0)�� ����
            playerRigidbody.velocity = Vector2.zero;
            // ������ٵ� �������� �� �ֱ�
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            // ����� �ҽ� ���
            playerAudio.Play();
        }
        else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // ���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y ���� ������(���� ��� ��)
            // ���� �ӵ��� �������� ����
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // �ִϸ������� Grounded�Ķ���͸� isGrounded ������ ����
        animator.SetBool("Grounded", isGrounded);
       
    }

    private void Die()
    {
        // ���ó��
        // �ִϸ������� Die Ʈ���� �Ķ���͸� ��
        animator.SetTrigger("Die");

        // ����� �ҽ��� �Ҵ�� ����� Ŭ���� deathClip���� ����
        playerAudio.clip = deathClip;
        // ��� ȿ���� ���
        playerAudio.Play();

        // �ӵ��� ����(0,0)�� ����
        playerRigidbody.velocity = Vector2.zero;
        // ��� ���¸� true�� ����
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ʈ���� �ݶ��̴��� ���� ��ֹ����� �浹�� ����
        if(collision.tag == "Dead" && !isDead)
        {
            // �浹�� ������ �±װ� Dead�̸� ���� ������� �ʾҴٸ�
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������� �����ϴ� ó��
        // � �ݶ��̴��� �������, �浹 ǥ���� ������ ���� ������
        if(collision.contacts[0].normal.y > 0.7f)
        {
            // isGrounded�� true�� �����ϰ�, ���� ���� Ƚ���� 0���� ����
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ��� ������� �����ϴ� ó��
        // � �ݶ��̴����� ������ ��� isGrounded�� false�� ����
        isGrounded = false;
    }
}
