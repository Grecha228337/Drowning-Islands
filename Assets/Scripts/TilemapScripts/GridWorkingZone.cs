using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridWorkingZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GridBuildingSystem.current.canBuild = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GridBuildingSystem.current.canBuild = true;
    }
}
