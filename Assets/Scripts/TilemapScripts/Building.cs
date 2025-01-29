using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed {get; private set; }
    public BoundsInt area;
    public TileType whatIsSolidType = TileType.White;

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(GridBuildingSystem.current.CanTakeArea(areaTemp, whatIsSolidType))
        {
            return true;
        }
        return false;
    }
    public virtual bool Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
        return true;
    }

    public virtual void DestroyBuilding()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        GridBuildingSystem.current.DestroyArea(areaTemp, whatIsSolidType);
        Destroy(this.gameObject);
    }
}
