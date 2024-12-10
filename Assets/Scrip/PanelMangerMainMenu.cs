using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelMangerMainMenu : MonoBehaviour
{
    public Button buttonStartGame;

    [Header("Параметры для получения бонуса")]
    public Button buttonBonus;
    public TMP_Text textCounterBonus;
    public int countOnlyBonus;
    public string idCountBonus = "IDCountBonus";

    [Header("Параметры для сохранения золота")]
    public TMP_Text[] textGold;
    public int countGold;
    public string idGold = "GOLD_ID";

    [Header("Параметры какой текущий уровнь сложности")]
    public int counterLevel;
    public string idCounterLevel = "IDCOUNTERLEVEL";
    public TMP_Text textCounterLevel;

    private void Start()
    {
        buttonStartGame.onClick.AddListener(StartLevel);
        buttonBonus.onClick.AddListener(OpenBonus);

        buttonOption.onClick.AddListener(OpenlOption);
        buttonShop.onClick.AddListener(OpenShop);
        buttonBackOption.onClick.AddListener(CloseOption);
        buttonBackShop.onClick.AddListener(CloseShop);

        Load_Gold();
        Save_Gold();

        Load_Level();
        Save_Level();

        Load_CountOnly_Bonus();
        Save_CountOnly_Bonus();

        Load_Location_Count();
        Save_Location_Count();
    }

    [Header("Параметр сохранения")]
    public int locationCount;
    public string idLocationCount = "IDLocationCount";

    public void Save_Location_Count()
    {
        PlayerPrefs.SetInt(idLocationCount, locationCount);
        PlayerPrefs.Save();
    }
    public void Load_Location_Count()
    {
        if (PlayerPrefs.HasKey(idLocationCount))
        {
            locationCount = PlayerPrefs.GetInt(idLocationCount);
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Load_CountOnly_Bonus()
    {
        if (PlayerPrefs.HasKey(idCountBonus))
        {
            textCounterBonus.text = $"{PlayerPrefs.GetInt(idCountBonus)}/10";
            countOnlyBonus = PlayerPrefs.GetInt(idCountBonus);
        }
    }
    public void Save_CountOnly_Bonus()
    {
        PlayerPrefs.SetInt(idCountBonus, countOnlyBonus);
        PlayerPrefs.Save();
    }
    public void OpenBonus()
    {
        if (countOnlyBonus >= 10)
        {
            countOnlyBonus -= 10;
            countGold += 500;
            Save_Gold();
            Save_CountOnly_Bonus();
        }
    }
    public void Load_Gold()
    {
        if (PlayerPrefs.HasKey(idGold))
        {
            countGold = PlayerPrefs.GetInt(idGold);
            Apply_Gold_To_Text();
        }
    }
    public void Save_Gold()
    {
        PlayerPrefs.SetInt(idGold, countGold);
        PlayerPrefs.Save();
    }
    public void Apply_Gold_To_Text()
    {
        for (int i = 0; i < textGold.Length; i++)
        {
            textGold[i].text = countGold.ToString();
        }
    }
    public void Load_Level()
    {
        if (PlayerPrefs.HasKey(idCounterLevel))
        {
            counterLevel = PlayerPrefs.GetInt(idCounterLevel);
            Apply_Level_To_Text();
        }
    }
    public void Save_Level()
    {
        PlayerPrefs.SetInt(idCounterLevel, counterLevel);
        PlayerPrefs.Save();
    }
    public void Apply_Level_To_Text()
    {
        textCounterLevel.text = $"{counterLevel} LVL";
    }

    [Header("Кнопки на главном меню")]
    public Button buttonOption;
    public Button buttonShop;
    public Button buttonBackOption;
    public Button buttonBackShop;

    public GameObject panelOption;
    public GameObject panelShop;

    public void OpenlOption()
    {
        panelOption.SetActive(true);
    }
    public void OpenShop()
    {
        GetComponent<Shop>().LoadButtonShop();
        panelShop.SetActive(true);
    }
    public void CloseOption()
    {
        panelOption.SetActive(false);
    }
    public void CloseShop()
    {
        panelShop.SetActive(false);
    }
}