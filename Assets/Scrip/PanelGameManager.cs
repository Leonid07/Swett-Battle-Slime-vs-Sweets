using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelGameManager : MonoBehaviour
{
    [Header("Панель проигрыша")]
    public GameObject panelLose;
    public Button buttonRestart;
    public Button buttonHomeLose;

    [Header("Панель выйгрыша")]
    public GameObject panelWin;
    public Button buttonNext;
    public Button buttonHomeWin;

    public void Start()
    {
        buttonRestart.onClick.AddListener(ResterLevel);
        buttonHomeLose.onClick.AddListener(HomeLose);

        buttonNext.onClick.AddListener(NextLevel);
        buttonHomeWin.onClick.AddListener(HomeWin);
    }
    public void ResterLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void HomeLose()
    {
        SceneManager.LoadScene(0);
    }
    public void HomeWin()
    {
        SaveGold();
        Save_CountOnly_Bonus();
        SaveCounterLevel();
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SaveGold();
        Save_CountOnly_Bonus();
        SaveCounterLevel();
        SceneManager.LoadScene(1);
    }

    [Header("Сохранение значений")]
    [Header("Параметры для сохранения золота")]
    public string idGold = "GOLD_ID";

    [Header("Параметры какой текущий уровнь сложности")]
    public int counterLevel;
    public string idCounterLevel = "IDCOUNTERLEVEL";

    [Header("Параметры для получения бонуса")]
    public string idCountBonus = "IDCountBonus";

    public void Save_CountOnly_Bonus()
    {
        int count = PlayerPrefs.GetInt(idCountBonus);
        count++;

        PlayerPrefs.SetInt(idCountBonus, count);
        PlayerPrefs.Save();
    }

    public void LoadCounterLevel()
    {
        counterLevel = PlayerPrefs.GetInt(idCounterLevel);
    }

    public void SaveGold()
    {
        int count = PlayerPrefs.GetInt(idGold);
        count += 100;
        PlayerPrefs.SetInt(idGold, count);
    }
    public void SaveCounterLevel()
    {
        counterLevel = PlayerPrefs.GetInt(idCounterLevel, counterLevel);
        counterLevel++;

        PlayerPrefs.SetInt(idCounterLevel, counterLevel);
    }
}