using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Upgrade upgrade;
    public Button b;
    public GameObject sold;

    [Header("UI/Objects")]
    public GameObject infoBoardObj;
    public InfoBoard infoBoard;

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoBoardObj.SetActive(true);
        //infoBoardObj.GetComponent<RectTransform>().position = transform.position;
        infoBoard.textName.text = upgrade._name;
        infoBoard.textDescription.text = upgrade.description;
        for (int i = 0; i < upgrade.needAmount.Length; i++)
        {
            infoBoard.resourceIcons[i].sprite = infoBoard.resourceIconsByType[upgrade.resTypes[i]];
            infoBoard.textNeededRes[i].text = upgrade.needAmount[i].ToString();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoBoardObj.SetActive(false);
    }
    public void UpgradeSmth()
    {
        b.interactable = false;
        upgrade.isActive = true;
        sold.SetActive(true);
        GameManager.current.UpgradeTool(upgrade.toolType, upgrade.upgradeType);
        for (int i = 0; i < 2; i++)
        {
            GameManager.current.ChangeResource(-upgrade.needAmount[i], upgrade.resTypes[i]);
        }
        FindObjectOfType<AudioManager>().Play("Anvil");
        GameManager.current.CheckForEveryUpgradeIcons();
    }

    public void CheckAmount()
    {
        if (upgrade.isActive)
            return;
        for (int i = 0; i < upgrade.needAmount.Length; i++)
        {
            if (!GameManager.current.CheckAmount(upgrade.needAmount[i], upgrade.resTypes[i]))
            {
                b.interactable = false;
                return;
            }
        }
        b.interactable = true;
    }
}

[System.Serializable]
public class Upgrade
{
    [Header("Info")]
    public string _name;
    [TextArea(3, 6)]
    public string description;
    public ResourceType[] resTypes;
    public int[] needAmount;

    public ToolType toolType;
    public UpgradeType upgradeType;
    public bool isActive = false;
}

public enum UpgradeType
{
    Attack,
    Speed,
    AmountIncrease,
    ReturnResource
}
