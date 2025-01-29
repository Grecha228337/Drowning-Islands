using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeballArea : MonoBehaviour
{
    public ToolSelectSystem toolSelectSystem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Building"))
        {
            collision.gameObject.GetComponent<PurchasableBuilding>().Save();
            collision.gameObject.GetComponent<PurchasableBuilding>().DestroyBuilding();
            toolSelectSystem.SelectTool(ToolType.None);
            this.gameObject.SetActive(false);

        }
    }
}
