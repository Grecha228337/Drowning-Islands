using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameModeSwitcher gameModeSwitcher;
    public void Open()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        gameModeSwitcher.SwitchMode(GameMode.isShopping);

        GameManager.current.CheckForEveryUpgradeIcons();
    }
    public void Close()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        gameModeSwitcher.SwitchMode(GameMode.isPlayer);
    }
}

