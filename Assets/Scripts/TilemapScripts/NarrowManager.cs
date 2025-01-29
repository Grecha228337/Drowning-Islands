using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class NarrowManager : MonoBehaviour
{
    public Tilemap islandTilemap;
    public Tilemap mainTilemap;
    public Island[] islands;

    public int cooldown = 2;
    public int min, seconds;
    public int secPassed;
    public TextMeshProUGUI timerText;
    public Animator narrowIsComingAnim;

    void Start()
    {
        foreach (var island in islands)
        {
            island.InitializeTilemaps(islandTilemap, mainTilemap);
        }
        islands[0].SetTiles();
        islands[0].PrepareToNarrow();
        StartCoroutine(Timer());

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        seconds--;
        if(seconds <= 0)
        {
            min--;
            seconds = 59;
            if(min < 0)
            {
                foreach (var island in islands)
                {
                    if (island.isActive)
                    {
                        island.NarrowIsland();
                        narrowIsComingAnim.SetTrigger("Disappear");
                        island.PrepareToNarrow();
                    }
                }
                min = cooldown - 1;
            }
        }
        timerText.text = "0" + min + ":" + (seconds > 9 ?  seconds : "0" + seconds);
        //Анимация о том, что скоро наводнение
        if(min == 0 && seconds == 15)
            narrowIsComingAnim.SetTrigger("Appear");

        secPassed++;
        StartCoroutine(Timer());
    }

    [ContextMenu("Narrow")]
    void NarrowForUnity()
    {
        islands[0].NarrowIsland();
    }
}
[System.Serializable]
public class Island
{
    public Vector2Int startPos;
    public int size;
    private Tilemap islandTilemap;
    private Tilemap mainTilemap;
    public List<InGameTile> islandTiles;
    public List<InGameTile> mainTiles;
    public bool isActive = false;

    public LayerMask buildingsLayer;
    public Color HighlightedColor;

    public void InitializeTilemaps(Tilemap island, Tilemap main)
    {
        islandTilemap = island; mainTilemap = main;
    }
    public void SetTiles()
    {
        for (int i = startPos.x; i <= size + startPos.x; i++)
        {
            for (int j = startPos.y; j >= -size + startPos.y; j--)
            {
                islandTiles.Add(new InGameTile(i, j, islandTilemap.GetTile(new Vector3Int(i, j))));
                if(!mainTilemap.GetTile(new Vector3Int(i, j)) == GridBuildingSystem.current._tileBases[1])
                    mainTilemap.SetTile(new Vector3Int(i, j), GridBuildingSystem.current._tileBases[0]);
                mainTiles.Add(new InGameTile(i, j, mainTilemap.GetTile(new Vector3Int(i, j))));
            }
        }
    }
    public void PrepareToNarrow()
    {
        List<InGameTile> tempIsland =  new List<InGameTile>(islandTiles);
        size -= 2;
        startPos -= new Vector2Int(-1, 1);
        islandTiles.Clear();

        SetTiles();

        tempIsland = tempIsland.Except(islandTiles).ToList();
        foreach (InGameTile item in tempIsland)
        {
            islandTilemap.SetColor(item.position, HighlightedColor);
        }
        size += 2;
        startPos += new Vector2Int(-1, 1);
        SetTiles();
    }
    public void NarrowIsland()
    {
        List<InGameTile> tempIsland =  new List<InGameTile>(islandTiles);
        List<InGameTile> tempMain = new List<InGameTile>(mainTiles);
        size -= 2;
        startPos -= new Vector2Int(-1, 1);
        islandTiles.Clear();
        mainTiles.Clear();
        SetTiles();

        tempIsland = tempIsland.Except(islandTiles).ToList();
        foreach (InGameTile item in tempIsland)
        {
            //Collider2D[] buildings = Physics2D.OverlapCircleAll(GridBuildingSystem.current.gridLayout.CellToLocalInterpolated(item.position), 0.15f,buildingsLayer);
            Collider2D[] buildings = Physics2D.OverlapBoxAll(GridBuildingSystem.current.gridLayout.CellToLocalInterpolated(item.position) + new Vector3(0.5f, 0.5f, 0), new Vector2(0.6f, 0.6f), 0, buildingsLayer);

            foreach (var building in buildings)
            {
                if (building != null)
                {
                    if (building.gameObject.layer == 8)
                    {
                        building.GetComponent<Restarter>().Restart();
                    }
                    else
                        building.GetComponent<Building>().DestroyBuilding();
                }
            }
            
            islandTilemap.SetTile(item.position, null);
        }
        tempMain = tempMain.Except(mainTiles).ToList();
        foreach (InGameTile item in tempMain)
        {
            mainTilemap.SetTile(item.position, GridBuildingSystem.current._tileBases[3]);
        }
    }
    
}
[System.Serializable]
public class InGameTile
{
    public Vector3Int position;
    public TileBase tile;

    public InGameTile(int posX, int posY, TileBase t)
    {
        position.x = posX;
        position.y = posY;
        tile = t;
    }

    public override bool Equals(object? obj)
    {
        if (obj is InGameTile tile) return position == tile.position;
        return false;
    }
    public override int GetHashCode() => position.GetHashCode();
}