using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RandomResourseSpawner : MonoBehaviour
{
    public GameObject[] resourcePrefabs;
    public int[] chances;
    public NarrowManager narrowManager;

    public float cooldown;
    public int resSpawns = 3;
    public int islandId;

    private void Start()
    {
        StartCoroutine(Spawner());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnResource();
        }
    }
    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(cooldown);
        for (int i = 0; i < resSpawns; i++)
        {
            SpawnResource();
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(Spawner());
    }
    void SpawnResource()
    {
        if (!narrowManager.islands[islandId].isActive)
            return;
        List<InGameTile> tmp = new List<InGameTile>();
        int chance = Random.Range(0, 100) + 1;
        int randomResToSpawnID = 0;
        for (int index = 0; index < chances.Length; index++)
        {
            var ch = chances[index];
            if (chance <= ch)
            {
                randomResToSpawnID = index;
                break;

            }
        }
        BoundsInt areaTemp = resourcePrefabs[randomResToSpawnID].GetComponent<Building>().area;
        foreach (var inGameTile in narrowManager.islands[islandId].mainTiles)
        {
            areaTemp.position = inGameTile.position;
            if (GridBuildingSystem.current.CanTakeArea(areaTemp, TileType.White))
            {
                tmp.Add(inGameTile);
            }
        }
        if (tmp == null)
            return;
        Debug.Log(tmp.Count);
        Building b = Instantiate(resourcePrefabs[randomResToSpawnID], tmp[Random.Range(0, tmp.Count)].position, Quaternion.identity).GetComponent<Building>();

        b.Place();
    }
}
