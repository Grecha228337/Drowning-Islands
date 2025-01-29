using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameModeSwitcher : MonoBehaviour
{
    public PlayerController player;
    public ToolSelectSystem toolSelectSystem;
    public GameObject[] cams;
    public GameObject buildMenu;
    public GameMode gameMode;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.B))
        {
            if(gameMode == GameMode.isPlayer)
                SwitchMode(GameMode.isBuilding);
            else if(gameMode == GameMode.isBuilding)
                SwitchMode(GameMode.isPlayer);
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameMode == GameMode.isBuilding)
                SwitchMode(GameMode.isPlayer);
            else if (gameMode == GameMode.isShopping)
                SwitchMode(GameMode.isPlayer);
        }
    }
    public void SwitchMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.isPlayer:
                cams[0].SetActive(true);
                cams[1].SetActive(false);
                player.enabled = true;
                buildMenu.SetActive(false);
                GridBuildingSystem.current.EnableSystem(false);
                toolSelectSystem.enabled = true;
                gameMode = GameMode.isPlayer;
                Time.timeScale = 1f;
                break;
            case GameMode.isBuilding:
                cams[0].SetActive(false);
                cams[1].SetActive(true);
                toolSelectSystem.SelectTool(ToolType.None);
                toolSelectSystem.enabled = false;
                player.enabled = false;
                buildMenu.SetActive(true);
                GridBuildingSystem.current.EnableSystem(true);
                gameMode = GameMode.isBuilding;
                Time.timeScale = 0f;
                break;
            case GameMode.isShopping:
                cams[0].SetActive(true);
                cams[1].SetActive(false);
                toolSelectSystem.SelectTool(ToolType.None);
                toolSelectSystem.enabled = false;
                player.enabled = false;
                buildMenu.SetActive(false);
                GridBuildingSystem.current.EnableSystem(false);
                gameMode = GameMode.isShopping;
                Time.timeScale = 0f;
                break;
            default:
                break;
        }
    }
}

public enum GameMode
{
    isPlayer,
    isBuilding,
    isShopping
}