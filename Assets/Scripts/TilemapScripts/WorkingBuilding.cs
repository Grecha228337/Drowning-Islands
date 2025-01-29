using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingBuilding : PurchasableBuilding
{
    [Header("Cooldown")]
    public float endCooldown = 5;
    public float cooldown = 5f;
    [Header("Giving")]
    public List<ResourceType> givingType;
    public int[] givingAmount;
    public GameObject[] givingResPrefabs;
    public Transform spawnPos;

    [Header("Needed")]
    public bool isNeedResToWork = true;
    public List<ResourceType> needToWorkType;
    public int[] neededAmount;
    public GameObject X; 

    [Header("Scale")]
    public GameObject scale;
    public float startScale = 1f;
    public float currentScale;

    private void UpdateScale()
    {
        currentScale = cooldown * startScale / endCooldown;
        scale.transform.localScale = new Vector3(currentScale, 1, 1);
    }
    private void Update()
    {
        if (!Placed) return;

        if(cooldown >= endCooldown)
        {
            bool flag = true;
            if (isNeedResToWork)
            {
                int i = 0;
                //Проверка на наличие
                foreach (ResourceType type in needToWorkType)
                {
                    if (!GameManager.current.CheckAmount(neededAmount[i], type))
                    {
                        flag = false;
                        Instantiate(X, spawnPos.position, Quaternion.identity);
                        break;
                    }
                    i++;
                }
            }

            if (flag)
            {
                int i = 0;
                //Выдача
                foreach (ResourceType type in givingType)
                {
                    GameManager.current.ChangeResource(givingAmount[i], type);
                    Instantiate(givingResPrefabs[i], spawnPos.position, Quaternion.identity);
                    i++;
                }

                if(isNeedResToWork)
                {
                    i = 0;
                    foreach (ResourceType type in needToWorkType)
                    {
                        GameManager.current.ChangeResource(-neededAmount[i], type);
                        i++;
                    }
                }
            }
            cooldown = 0;
        }
        else
        {
            cooldown += Time.deltaTime;
            UpdateScale();
        }
    }
}
