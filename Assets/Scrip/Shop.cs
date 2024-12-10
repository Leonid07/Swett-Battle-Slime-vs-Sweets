using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public PanelMangerMainMenu menu;

    public ButtonShop[] buttonShops;

    public string idCountSkeenPlayer = "countSkeenPlayer";
    public int countSkeenPlayer = 0;

    private void Start()
    {
        LoadCountSkeenPlayer();
    }

    public void LoadButtonShop()
    {
        for (int i = 0; i < buttonShops.Length; i++)
        {
            if (PlayerPrefs.HasKey(buttonShops[i].idBuy))
            {
                buttonShops[i].isBuy = PlayerPrefs.GetInt(buttonShops[i].idBuy);
                buttonShops[i].Check();
            }
        }
    }

    public void SaveButtonShop()
    {
        for (int i = 0; i < buttonShops.Length; i++)
        {
            PlayerPrefs.SetInt(buttonShops[i].idBuy, buttonShops[i].isBuy);
        }
        PlayerPrefs.Save();
    }
    public void SaveCountSkeenPlayer()
    {
        PlayerPrefs.SetInt(idCountSkeenPlayer, countSkeenPlayer);
        PlayerPrefs.Save();
    }

    public void LoadCountSkeenPlayer()
    {
        if (PlayerPrefs.HasKey(idCountSkeenPlayer))
        {
            countSkeenPlayer = PlayerPrefs.GetInt(idCountSkeenPlayer);
        }
    }
}
