using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public TMP_Text hourlyBonusText;

    public Button hourlyBonusButton;

    public Color diactive;
    public Color active;

    private const string HourlyBonusTimeKey = "hourly_bonus_time";

    public int HourlyBonusCooldownInSeconds = 1200;

    public PanelMangerMainMenu mainMenu;

    public GameObject panelBonus;

    private void Start()
    {
        hourlyBonusButton.onClick.AddListener(ClaimHourlyBonus);
        StartCoroutine(UpdateBonusTextsRoutine());
    }

    private IEnumerator UpdateBonusTextsRoutine()
    {
        while (true)
        {
            UpdateBonusTexts();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void UpdateBonusTexts()
    {
        string hourlyBonusTimeStr = PlayerPrefs.GetString(HourlyBonusTimeKey, "0");

        long hourlyBonusTime = long.Parse(hourlyBonusTimeStr);

        long currentTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        long hourlyCooldown = hourlyBonusTime + HourlyBonusCooldownInSeconds - currentTimestamp;

        hourlyBonusText.text = FormatTimeHourly(hourlyCooldown);

        hourlyBonusButton.interactable = hourlyCooldown <= 0;
    }
    private string FormatTimeHourly(long seconds)
    {
        if (seconds <= 0)
        {
            hourlyBonusButton.GetComponent<Image>().color = active;
            hourlyBonusButton.enabled = true;
            return "";
        }
        hourlyBonusButton.GetComponent<Image>().color = diactive;
        hourlyBonusButton.enabled = false;
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }


    private void ClaimHourlyBonus()
    {
        long currentTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        mainMenu.countGold += 500;
        mainMenu.Save_Gold();
        mainMenu.Apply_Gold_To_Text();

        panelBonus.SetActive(true);

        StartCoroutine(timer());

        PlayerPrefs.SetString(HourlyBonusTimeKey, currentTimestamp.ToString());
        PlayerPrefs.Save();

        Debug.Log("Hourly Bonus Claimed!");
        Debug.Log($"New Hourly Bonus Time: {currentTimestamp}");
    }
    public IEnumerator timer()
    {
        yield return new WaitForSeconds(2.5f);
        panelBonus.SetActive(false);
    }
}
