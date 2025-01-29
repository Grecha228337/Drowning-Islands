using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Win : Building
{
    public BuildingIcon currentBuildingInfo;
    public string buildingInfoNameOnScene;
    public void Start()
    {
        currentBuildingInfo = GameObject.Find(buildingInfoNameOnScene).GetComponent<BuildingIcon>();
    }

    public override bool Place()
    {
        if (currentBuildingInfo.savedBuildings > 0)
        {
            currentBuildingInfo.savedBuildings--;
            FindObjectOfType<AudioManager>().Play("Build");
            base.Place();
            StartCoroutine(WinScene());
            return true;
        }
        for (int i = 0; i < 3; i++)
        {
            if (!GameManager.current.CheckAmount(currentBuildingInfo.needAmount[i], currentBuildingInfo.resTypes[i]))
            {
                Debug.Log("Не хватает ресурсов");
                return false;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            GameManager.current.ChangeResource(-currentBuildingInfo.needAmount[i], currentBuildingInfo.resTypes[i]);
        }
        FindObjectOfType<AudioManager>().Play("Build");
        base.Place();
        StartCoroutine(WinScene());
        return true;
    }
    IEnumerator WinScene()
    {
        yield return new WaitForSeconds(3f);
        GameManager.current.Win();
    }

}
