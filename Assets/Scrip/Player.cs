using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick; // Ссылка на ваш скрипт джойстика
    public float moveSpeed = 5f; // Скорость движения игрока
    public float gravity = -9.81f; // Сила притяжения

    private CharacterController characterController; // Для управления передвижением
    private Animator animator; // Для управления анимацией
    private Vector3 velocity; // Для учета вертикального движения (гравитация, прыжки)
    public bool isGrounded; // Для проверки, на земле ли игрок

    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Material newMaterialAddBonus;

    public SpawnPoint spawnPoint;

    public PanelGameManager panelGameManager;

    private void Start()
    {
        newMaterialAddBonus = skinnedMeshRenderer.GetComponent<Renderer>().sharedMaterials[0];
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Проверяем, на земле ли игрок
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Слегка прижимаем к земле
        }

        // Получаем значения из джойстика
        float horizontal = -joystick.Horizontal;
        float vertical = -joystick.Vertical;

        // Направление движения
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Перемещение игрока
        if (direction.magnitude >= 0.1f)
        {
            // Перемещаем игрока с помощью CharacterController
            characterController.Move(direction * moveSpeed * Time.deltaTime);

            // Поворот игрока в направлении движения
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * 10f);

            // Включение анимации бега
            animator.SetBool("IsWalk", true);
        }
        else
        {
            // Отключение анимации бега
            animator.SetBool("IsWalk", false);
        }

        // Добавляем гравитацию
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bonus")
        {
            newMaterialAddBonus = collision.gameObject.GetComponent<Renderer>().sharedMaterial;
            skinnedMeshRenderer.sharedMaterial = newMaterialAddBonus;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<enemy>().skinnedMeshRenderer.sharedMaterials[0] == newMaterialAddBonus)
            {
                spawnPoint.MinusCountToCounter();
                collision.GetComponent<enemy>().DeadEnemy();
            }
            else
            {
                panelGameManager.panelLose.SetActive(true);
            }
        }
    }
}