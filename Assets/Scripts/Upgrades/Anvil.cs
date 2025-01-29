using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    private UpgradeMenu upgradeMenu;
    private GameModeSwitcher gameModeSwitcher;

    private void Start()
    {
        upgradeMenu = FindObjectOfType<UpgradeMenu>();
        gameModeSwitcher = FindObjectOfType<GameModeSwitcher>();
    }
    public void OnMouseDown()
    {
        if (gameModeSwitcher.toolSelectSystem.currentTool == ToolType.None && gameModeSwitcher.gameMode != GameMode.isBuilding)
            upgradeMenu.Open();
    }
}
