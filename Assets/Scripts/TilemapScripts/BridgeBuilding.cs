using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BridgeBuilding : PurchasableBuilding
{
    public TileBase bridgeTileBase;
    public override bool Place()
    {
        base.Place();
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        GridBuildingSystem.current.islandTilemap.SetTile(areaTemp.position, bridgeTileBase);
        
        DestroyBuilding();
        GridBuildingSystem.current.mainTilemap.SetTile(areaTemp.position, null);
        return true;
    }
}
