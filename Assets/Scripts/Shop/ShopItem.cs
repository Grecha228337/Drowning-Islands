using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("UI Objects")]
    public TextMeshProUGUI costText;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI description;
    public Image itemToBuyImage;
    public Image costImage;

    private Shop shop;
    public int id;

    public void UpdateInfo(ShopItemInfo itemInfos, Shop s, int i)
    {
        costText.text = itemInfos.cost.ToString();
        amountText.text = itemInfos.amount.ToString();
        description.text = itemInfos.description;
        costImage.sprite = itemInfos.costSprite;
        itemToBuyImage.sprite = itemInfos.itemToBuyImageSprite;

        id = i;
        shop = s;
    }
    public void BuyItem()
    {
        shop.BuyItem(id);
    }
}
[System.Serializable]
public class ShopItemInfo
{
    [Header("Info")]
    public int cost;
    public int amount;
    [TextArea(3, 6)]
    public string description;
    public ResourceType typeCost;
    public ResourceType typeRes;
    public int howManyCanBuy = 1;

    [Header("Sprites")]
    public Sprite itemToBuyImageSprite;
    public Sprite costSprite;
}
