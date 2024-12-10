using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    public GameObject[] bonusPrefab; // ������ ������, ������� ����� ����������

    private GameObject currentBonus; // ������� ����� �� �����

    void Start()
    {
        SpawnBonusObject();
    }

    void SpawnBonusObject()
    {
        int rand = Random.Range(0, bonusPrefab.Length);

        currentBonus = Instantiate(bonusPrefab[rand], transform.position, Quaternion.Euler(0f, 0f, 45f));

        // ��������� �������� ����� 5 ������
        StartCoroutine(CheckIfBonusIsDestroyed());
    }

    IEnumerator CheckIfBonusIsDestroyed()
    {
        // ��� 5 ������
        yield return new WaitForSeconds(5f);

        // ���������, ��� �� ������ �����
        if (currentBonus == null)
        {
            // ���� ������ �����, ������� ��� ������
            SpawnBonusObject();
        }
        else
        {
            // ���� ������ ����������, �������� ��������� ��������
            StartCoroutine(CheckIfBonusIsDestroyed());
        }
    }
}
