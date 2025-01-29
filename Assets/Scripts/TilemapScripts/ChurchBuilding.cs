using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchBuilding : PurchasableBuilding
{
    [SerializeField] private int amountIncrease;
    public override bool Place()
    {
        if (GameManager.current.churchCount == 3)
        {
            return false;
        }
        base.Place();
        GameManager.current.ChangeSpawnersResAmount(amountIncrease, 1);
        return true;
    }
    public override void DestroyBuilding()
    {
        base.DestroyBuilding();
        GameManager.current.ChangeSpawnersResAmount(-amountIncrease, -1);
    }
}
