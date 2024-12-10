using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public Vector3 offset; // �������� ������ ������������ ������
    public float smoothSpeed = 5f; // �������� ����������� ��������

    private void LateUpdate()
    {
        // �������� ������� ������ � ������ ��������
        Vector3 desiredPosition = player.position + offset;

        // ������� �������� ������ � �������� �������
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // ����������, ��� ������ ��������� ���� ��������� �������
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
    }
}
