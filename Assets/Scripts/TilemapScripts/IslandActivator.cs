using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandActivator : MonoBehaviour
{
    public NarrowManager narrowManager;
    public GameObject spawner;
    public int id;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            narrowManager.islands[id].isActive = true;
            narrowManager.islands[id].SetTiles();
            narrowManager.islands[id].PrepareToNarrow();
            spawner.SetActive(true);
            Destroy(gameObject);
        }
    }
}
