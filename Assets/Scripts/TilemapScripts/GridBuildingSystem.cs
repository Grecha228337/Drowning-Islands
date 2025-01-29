using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;
    public Tilemap islandTilemap;

    public TileBase[] _tileBases;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private Building temp;
    private Vector3 prevPos;
    public BoundsInt prevArea;

    private TileType currentSolidGround;

    [HideInInspector] public bool canBuild = true;

    private void Awake()
    {
        current = this;
    }

    public void ChangeSolidGround(string solidGround)
    {
        if(solidGround == "Earth")
        {
            currentSolidGround = TileType.White;
        }
        else if(solidGround == "Water")
        {
            currentSolidGround = TileType.Blue;
        }
    }
    private void Start()
    {
        tileBases = new Dictionary<TileType, TileBase>();
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, _tileBases[0]);
        tileBases.Add(TileType.Green, _tileBases[1]);
        tileBases.Add(TileType.Red, _tileBases[2]);
        tileBases.Add(TileType.Blue, _tileBases[3]);
    }
    private void Update()
    {
        if(!temp)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject(0))
        {
            return;
        }

        if (!temp.Placed)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

            if (prevPos != cellPos)
            {
                temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos);
                prevPos = cellPos;
                FollowBuilding(currentSolidGround);
            }
        }

        if (Input.GetMouseButtonDown(0) && canBuild)
        {
            if (temp.CanBePlaced())
            {
                temp.Place();
                //temp = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            if(!temp.Placed)
            {
                ClearArea();
                Destroy(temp.gameObject);
            }
        }
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] array = new TileBase[size];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }

    public static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);

    }
    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }

    public void InitializeWithBuilding(GameObject building, TileType typeOfSolidGround)
    {
        if(temp != null && !temp.Placed)
        {
            ClearArea();
            Destroy(temp.gameObject);
        }
        temp = Instantiate(building, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).GetComponent<Building>();
        FollowBuilding(typeOfSolidGround);
    }

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding(TileType typeOfSolidGround)
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[typeOfSolidGround])
            {
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                FillTiles(tileArray, TileType.Red);
                break;
            }
        }
        tempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area, TileType typeOfSolidGround)
    {
        TileBase[] baseArray = GetTilesBlock (area, mainTilemap);
        foreach (var b in baseArray)
        {
            if(b != tileBases[typeOfSolidGround])
            {
                Debug.Log("Не могу поставить");
                return false;
            }
        }
        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, tempTilemap);
        SetTilesBlock(area, TileType.Green, mainTilemap);
    }
    public void DestroyArea(BoundsInt area, TileType typeOfSolidGround)
    {
        SetTilesBlock(area, typeOfSolidGround, mainTilemap);
    }

    public void EnableSystem(bool enable)
    {
        mainTilemap.gameObject.SetActive(enable);
        tempTilemap.gameObject.SetActive(enable);
        if(!enable && temp != null)
        {
            ClearArea();
            if(!temp.Placed)
                Destroy(temp.gameObject);
        }
    }
}
public enum TileType
{
    Empty,
    White,
    Green,
    Red,
    Blue
}