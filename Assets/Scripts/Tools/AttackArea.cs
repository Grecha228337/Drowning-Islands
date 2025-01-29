using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public float damage;
    public string resourceTag;

    public void UpdateAttack(float dmg)
    {
        damage = dmg;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(resourceTag))
        {
            this.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Resource>().TakeDamage(damage);
        }
    }
}
