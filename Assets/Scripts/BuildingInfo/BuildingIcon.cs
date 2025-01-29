using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BuildingIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Info")]
    public string _name;
    [TextArea(3,6)]
    public string description;
    public ResourceType[] resTypes;
    public int[] needAmount;
    public TileType whatIsSolidType;

    [Header("Saved")]
    public int savedBuildings;

    [Header("UI/Objects")]
    public GameObject infoBoardObj;
    public RectTransform infoBoardSpawnPos;
    public InfoBoard infoBoard;
    public GameObject buildingPrefab;
    public GameObject saved;
    public TextMeshProUGUI savedText;


   
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        infoBoardObj.SetActive(true);
        infoBoardObj.GetComponent<RectTransform>().position = infoBoardSpawnPos.position;
        infoBoard.textName.text = _name;
        infoBoard.textDescription.text = description;
        for (int i = 0; i < needAmount.Length; i++)
        {
            infoBoard.resourceIcons[i].sprite = infoBoard.resourceIconsByType[resTypes[i]];
            infoBoard.textNeededRes[i].text = needAmount[i].ToString();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        infoBoardObj.SetActive(false);
    }
    private void OnEnable()
    {
        CheckNeededAmount();
    }
    public void Buy()
    {
        GridBuildingSystem.current.InitializeWithBuilding(buildingPrefab, whatIsSolidType);
    }
    
    public virtual bool CheckNeededAmount()
    {
        if (savedBuildings > 0)
        {
            GetComponent<Button>().interactable = true;
            saved.SetActive(true);
            savedText.text = savedBuildings.ToString();
            return true;
        }
        for (int i = 0; i < 3; i++)
        {
            if (!GameManager.current.CheckAmount(needAmount[i], resTypes[i]))
            {
                Debug.Log("Не хватает ресурсов");
                GetComponent<Button>().interactable = false;
                saved.SetActive(false);
                return false;
            }
        }
        saved.SetActive(false);
        GetComponent<Button>().interactable = true;
        return true;
    }
}
