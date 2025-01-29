using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ShopItemInfo> itemInfos = new List<ShopItemInfo>();
    public GameObject ShopItemPrefab;
    public Transform parentForSlots;
    private List<GameObject> items = new List<GameObject>();
    public GameModeSwitcher gameModeSwitcher;

    private void Start()
    {
        for (int i = 0; i < itemInfos.Count; i++) 
        {
            GameObject tmp = Instantiate(ShopItemPrefab);
            tmp.transform.SetParent(parentForSlots);
            tmp.transform.localScale = Vector3.one;
            items.Add(tmp);

            ShopItem tmpItem = tmp.GetComponent<ShopItem>();
            tmpItem.UpdateInfo(itemInfos[i], this, i);

        }
    }

    public void BuyItem(int id)
    {
        if (itemInfos[id].cost <= GameManager.current.amountOfResources[itemInfos[id].typeCost])
        {
            Debug.Log("Куплено");
            GameManager.current.ChangeResource(itemInfos[id].amount, itemInfos[id].typeRes);
            itemInfos[id].howManyCanBuy -= 1;
            GameManager.current.ChangeResource(-itemInfos[id].cost, itemInfos[id].typeCost);
            if(itemInfos[id].howManyCanBuy == 0)
            {
                GameObject tmp = items[id];
                for (int i = id; i < items.Count; i++)
                {
                    items[i].GetComponent<ShopItem>().id--;
                }
                items.RemoveAt(id);
                itemInfos.RemoveAt(id);
                Destroy(tmp);
            }
            FindObjectOfType<AudioManager>().Play("Buy");
            return;
        }
        Debug.Log("Не хватает " + (itemInfos[id].cost - GameManager.current.amountOfResources[itemInfos[id].typeCost]));
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
        gameModeSwitcher.SwitchMode(GameMode.isShopping);
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
        gameModeSwitcher.SwitchMode(GameMode.isPlayer);
    }
}
