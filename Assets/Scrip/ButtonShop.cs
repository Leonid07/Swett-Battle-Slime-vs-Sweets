using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    public void Start()
    {
        buttonShopSwitch.onClick.AddListener(() =>
        {
            BuyAndTake();
        });
    }

    public PanelMangerMainMenu menu;
    public Shop shop;

    [Header("Маханика самого магазина")]
    public TMP_Text buttonText;
    public int price;
    public Button buttonShopSwitch;
    public Image buttonImage;
    public TMP_Text textPriceBuy;

    public GameObject imageCoin;

    [Header("Куплено")]
    public string isBuy_1 = "Select";
    //public Sprite isBuySprite_1;

    [Header("Одето")]
    public string isBuy_2 = "It's used";
    //public Sprite isBuySprite_2;

    [Header("Не куплено")]
    public string isBuy_3;
    //public Sprite isBuySprite_3;

    public int isBuy = 0;
    public string idBuy;

    [Header("Какой скин будет выбран")]
    public int skeenID;

    // 0  не куплено
    // 1 куплено но не одето
    // 2 купленно и одето

    private void Awake()
    {
        idBuy = gameObject.name;
        isBuy_3 = price.ToString();
        Check();
    }

    private void BuyAndTake()
    {
        if (isBuy == 0)
        {
            if (menu.countGold >= price)
            {
                menu.countGold -= price;
                menu.Save_Gold();
                menu.Apply_Gold_To_Text();
                isBuy++;
                Check();

                shop.SaveButtonShop();

                return;
            }
        }
        if (isBuy == 1)
        {
            for (int i = 0; i < shop.buttonShops.Length; i++)
            {
                if (shop.buttonShops[i].isBuy == 0)
                {
                    continue;
                }
                if (shop.buttonShops[i].isBuy == 2)
                {
                    shop.buttonShops[i].isBuy = 1;
                    shop.buttonShops[i].Check();
                }
            }

            isBuy = 2;
            Check();
            shop.SaveButtonShop();
        }

    }

    public void Check()
    {
        switch (isBuy)
        {
            case 0:
                textPriceBuy.text = isBuy_3;
                textPriceBuy.gameObject.SetActive(false);
                buttonText.gameObject.SetActive(true);
                imageCoin.SetActive(true);
                //buttonImage.sprite = isBuySprite_3;
                break;
            case 1:
                textPriceBuy.text = isBuy_1;
                textPriceBuy.gameObject.SetActive(true);
                buttonText.gameObject.SetActive(false);
                imageCoin.SetActive(false);
                //buttonImage.sprite = isBuySprite_1;
                break;
            case 2:
                shop.countSkeenPlayer = skeenID;
                shop.SaveCountSkeenPlayer();

                textPriceBuy.text = isBuy_2;
                textPriceBuy.gameObject.SetActive(true);
                buttonText.gameObject.SetActive(false);
                imageCoin.SetActive(false);
                //buttonImage.sprite = isBuySprite_2;
                break;
        }
    }
}
