using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    public GameObject[] bonusPrefab; // Префаб бонуса, который будет спавниться

    private GameObject currentBonus; // Текущий бонус на сцене

    void Start()
    {
        SpawnBonusObject();
    }

    void SpawnBonusObject()
    {
        int rand = Random.Range(0, bonusPrefab.Length);

        currentBonus = Instantiate(bonusPrefab[rand], transform.position, Quaternion.Euler(0f, 0f, 45f));

        // Запускаем проверку через 5 секунд
        StartCoroutine(CheckIfBonusIsDestroyed());
    }

    IEnumerator CheckIfBonusIsDestroyed()
    {
        // Ждём 5 секунд
        yield return new WaitForSeconds(5f);

        // Проверяем, был ли объект удалён
        if (currentBonus == null)
        {
            // Если объект удалён, спавним его заново
            SpawnBonusObject();
        }
        else
        {
            // Если объект существует, повторно запускаем проверку
            StartCoroutine(CheckIfBonusIsDestroyed());
        }
    }
}
