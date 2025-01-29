using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttackArea : AttackArea
{
    public bool isUpgraded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Building"))
        {
            this.gameObject.SetActive(false);
            PurchasableBuilding tmp = collision.gameObject.GetComponent<PurchasableBuilding>();
            tmp.DestroyBuilding();
            if(isUpgraded)
            {
                for (int i = 0; i < tmp.currentBuildingInfo.needAmount.Length; i++)
                {
                    GameManager.current.ChangeResource(tmp.currentBuildingInfo.needAmount[i]/2, tmp.currentBuildingInfo.resTypes[i]);
                }
            }
        }
    }
}
