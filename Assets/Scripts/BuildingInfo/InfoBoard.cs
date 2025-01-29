using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBoard : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDescription;
    public TextMeshProUGUI[] textNeededRes;
    public Image[] resourceIcons; //Объекты картинок
    public Sprite[] resourceSprites; //Все типы спрайтов

    public Dictionary<ResourceType, Sprite> resourceIconsByType = new Dictionary<ResourceType, Sprite>();

    private void Start()
    {
        for (int i = 0; i < resourceSprites.Length; i++)
        {
            resourceIconsByType.Add((ResourceType)i, resourceSprites[i]);
        }
    }
}
