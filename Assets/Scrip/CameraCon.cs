using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public Vector3 offset; // Смещение камеры относительно игрока
    public float smoothSpeed = 5f; // Скорость сглаживания движения

    private void LateUpdate()
    {
        // Желаемая позиция камеры с учетом смещения
        Vector3 desiredPosition = player.position + offset;

        // Плавное движение камеры к желаемой позиции
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Убеждаемся, что камера сохраняет свой начальный поворот
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
    }
}
