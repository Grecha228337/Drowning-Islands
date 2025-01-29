
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolSelectSystem : MonoBehaviour
{
    private GameObject followingGameObject;
    public ToolActivator[] tools;
    public ToolType currentTool = ToolType.None;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectTool(ToolType.Axe);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectTool(ToolType.Pickaxe);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectTool(ToolType.Hammer);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectTool(ToolType.Pokeball);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SelectTool(ToolType.None);
        }
    }
    
    private void FixedUpdate()
    {
        if (followingGameObject != null)
        {
            followingGameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            followingGameObject.transform.position += new Vector3(0, 0, 10);
        }
    }
    public void SelectTool(ToolType type)
    {
        switch (type)
        {
            case ToolType.None:
                foreach (var tool in tools)
                {
                    tool.Activate(false);
                }
                Cursor.visible = true;
                currentTool = ToolType.None;
                followingGameObject = null;
                break;
            case ToolType.Axe:
                tools[0].Activate(true);
                tools[1].Activate(false);
                tools[2].Activate(false);
                tools[3].Activate(false);

                Cursor.visible = false;
                currentTool = ToolType.Axe;
                followingGameObject = tools[0].tool;
                break;
            case ToolType.Pickaxe:
                tools[0].Activate(false);
                tools[1].Activate(true);
                tools[2].Activate(false);
                tools[3].Activate(false);

                Cursor.visible = false;
                currentTool = ToolType.Pickaxe;
                followingGameObject = tools[1].tool;
                break;
            case ToolType.Hammer:
                tools[0].Activate(false);
                tools[1].Activate(false);
                tools[2].Activate(true);
                tools[3].Activate(false);

                Cursor.visible = false;
                currentTool = ToolType.Hammer;
                followingGameObject = tools[2].tool;
                break;
            case ToolType.Pokeball:
                if (!GameManager.current.CheckAmount(1, ResourceType.Pokeball))
                    break;
                tools[0].Activate(false);
                tools[1].Activate(false);
                tools[2].Activate(false);
                tools[3].Activate(true);

                Cursor.visible = false;
                currentTool = ToolType.Pokeball;
                followingGameObject = tools[3].tool;
                break;
        }
    }

}
[System.Serializable]
public class ToolActivator
{
    public GameObject tool;
    public RectTransform toolUI;
    public GameObject toolOnPlayer;

    public void Activate(bool toActivate)
    {
        tool.SetActive(toActivate);
        toolOnPlayer.SetActive(toActivate);
        if(toActivate)
            toolUI.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        else
            toolUI.localScale = new Vector3(1f, 1f, 1f);
    }
}
public enum ToolType
{
    None,
    Axe,
    Pickaxe,
    Hammer,
    Pokeball
}
