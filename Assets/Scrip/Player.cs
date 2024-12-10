using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick; // ������ �� ��� ������ ���������
    public float moveSpeed = 5f; // �������� �������� ������
    public float gravity = -9.81f; // ���� ����������

    private CharacterController characterController; // ��� ���������� �������������
    private Animator animator; // ��� ���������� ���������
    private Vector3 velocity; // ��� ����� ������������� �������� (����������, ������)
    public bool isGrounded; // ��� ��������, �� ����� �� �����

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
        // ���������, �� ����� �� �����
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ������ ��������� � �����
        }

        // �������� �������� �� ���������
        float horizontal = -joystick.Horizontal;
        float vertical = -joystick.Vertical;

        // ����������� ��������
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // ����������� ������
        if (direction.magnitude >= 0.1f)
        {
            // ���������� ������ � ������� CharacterController
            characterController.Move(direction * moveSpeed * Time.deltaTime);

            // ������� ������ � ����������� ��������
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * 10f);

            // ��������� �������� ����
            animator.SetBool("IsWalk", true);
        }
        else
        {
            // ���������� �������� ����
            animator.SetBool("IsWalk", false);
        }

        // ��������� ����������
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