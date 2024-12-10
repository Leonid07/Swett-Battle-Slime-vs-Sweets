using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] enemy;

    public int countEnemy;

    public TMP_Text textCounter;
    public int counterEnemy;

    public PanelGameManager gameManager;

    private void Start()
    {
        gameManager.LoadCounterLevel();
        counterEnemy = gameManager.counterLevel * 2;
        countEnemy = counterEnemy;
        StartCoroutine(Spawnenumy());
    }

    public IEnumerator Spawnenumy()
    {
        int count = counterEnemy;

        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, enemy.Length);
            Instantiate(enemy[rand], transform.position, Quaternion.identity);

            textCounter.text = $"Enemies {counterEnemy}/{countEnemy}";
        }
        yield return new WaitForSeconds(0.2f);
    }

    public void MinusCountToCounter()
    {
        counterEnemy--;
        textCounter.text = $"Enemies {counterEnemy}/{countEnemy}";

        if (counterEnemy == 0)
        {
            gameManager.panelWin.SetActive(true);
        }
    }
}
