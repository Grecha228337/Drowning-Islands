using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Building
{
    public ResourceType resType;
    public float maxHp;
    public float hp;
    private Animator anim;

    public GameObject floatingResPrefab;
    public Transform spawnPos;
    public string audioSoundName;

    [Header("Scale")]
    public GameObject scaleObj;
    private GameObject scale;
    public float startScale = 1f;
    public float currentScale;

    private void UpdateScale()
    {
        currentScale = hp * startScale / maxHp;
        scale.transform.localScale = new Vector3(currentScale, 1, 1);
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        scale = scaleObj.transform.GetChild(0).gameObject;
    }
    public override void DestroyBuilding()
    {
        base.DestroyBuilding();
        if (hp > 0) return;

        Instantiate(floatingResPrefab, spawnPos.position, Quaternion.identity);
        if(resType == ResourceType.Wood)
            GameManager.current.ChangeResource(Random.Range(GameManager.current.woodDrop.x, GameManager.current.woodDrop.y), resType);
        else if (resType == ResourceType.Rock)
        {
            if (GameManager.current.isPickaxeUpgraded)
            {
                if (Random.Range(1, 101) <= 8)
                {
                    GameManager.current.ChangeResource(1, ResourceType.Ore);
                    return;
                }
            }
            GameManager.current.ChangeResource(Random.Range(GameManager.current.rockDrop.x, GameManager.current.rockDrop.y), resType);
        }    
        else if (resType == ResourceType.Ore)
            GameManager.current.ChangeResource(1, resType);
    }
    public void TakeDamage(float damage)
    {
        scaleObj.SetActive(true);
        hp-=damage;
        anim.SetTrigger("Attacked");
        UpdateScale();
        CameraShake.current.Shake();
        FindObjectOfType<AudioManager>().Play(audioSoundName);
        if (hp <= 0)
        {
            DestroyBuilding();
            return;
        }
    }
}
