using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBonus : MonoBehaviour
{
    public float rotationSpeed = 100f; // �������� �������� �������
    private void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
