using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasableBuilding : Building
{
    public BuildingIcon currentBuildingInfo;
    public string buildingInfoNameOnScene;
    public string buildSound = "Build";
    public void Start()
    {
        currentBuildingInfo = GameObject.Find(buildingInfoNameOnScene).GetComponent<BuildingIcon>();
    }

    public override bool Place()
    {
        /*
        if(currentBuildingInfo.savedBuildings > 0)
        {
            currentBuildingInfo.savedBuildings--;
            FindObjectOfType<AudioManager>().Play("Build");
            base.Place();
            return true;
        }
        for (int i = 0; i < 3; i++)
        {
            if (!GameManager.current.CheckAmount(currentBuildingInfo.needAmount[i], currentBuildingInfo.resTypes[i]))
            {
                Debug.Log("Не хватает ресурсов");
                return false;
            }
        }*/
        if (currentBuildingInfo.savedBuildings > 0)
        {
            currentBuildingInfo.savedBuildings--;
            FindObjectOfType<AudioManager>().Play(buildSound);
            //currentBuildingInfo.CheckNeededAmount();
            base.Place();

            GameManager.current.CheckForEveryBuildingIcons();
            if (currentBuildingInfo.CheckNeededAmount())
                GridBuildingSystem.current.InitializeWithBuilding(this.gameObject, whatIsSolidType);
            return true;
        }
        for (int i = 0; i < 3; i++)
        {
            GameManager.current.ChangeResource(-currentBuildingInfo.needAmount[i], currentBuildingInfo.resTypes[i]);
        }
        FindObjectOfType<AudioManager>().Play(buildSound);
        GameManager.current.buildingCreated++;
        base.Place();

        GameManager.current.CheckForEveryBuildingIcons();
        if (currentBuildingInfo.CheckNeededAmount())
            GridBuildingSystem.current.InitializeWithBuilding(this.gameObject, whatIsSolidType);
        return true;
    }

    public void Save()
    {
        GameManager.current.ChangeResource(-1, ResourceType.Pokeball);
        currentBuildingInfo.savedBuildings++;
    }
    public override void DestroyBuilding()
    {
        base.DestroyBuilding();
        CameraShake.current.Shake();
        FindObjectOfType<AudioManager>().Play("Destroy");
    }
}
