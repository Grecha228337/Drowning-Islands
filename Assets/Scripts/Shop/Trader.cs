using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    public Shop shop;
    public ToolSelectSystem toolSelectSystem;

    public void OnMouseDown()
    {
        if(toolSelectSystem.currentTool == ToolType.None)
            shop.OpenShop();
    }

}
