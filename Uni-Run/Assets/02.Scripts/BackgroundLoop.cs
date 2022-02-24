using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    // ����� ���� ����
    private float width;

    private void Awake()
    {
        // ���� ���̸� �����ϴ� �Ÿ�
        // BoxCollider2D ������Ʈ�� Size �ʵ���
        // X���� ���� ���̷� ���.
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
    }

    void Update()
    {
        // ���� ��ġ�� �������� �������� width �̻� �̵����� �� ��ġ�� ���ġ
        if(transform.position.x <= -width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        // ��ġ�� ���ġ�ϴ� �޼���
        // ���� ��ġ���� ���������� ���� ���� *2 ��ŭ �̵�
        //Vector2 offset = new Vector2(width * 2f, 0);
        //transform.position = (Vector2)transform.position + offset;
        transform.position = transform.position + new Vector3(width * 2, 0f, 0f);
    }
}
