using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public ToolSelectSystem tSS;
    public GameObject attackArea;
    private AttackArea attackAreaScript;
    bool isAttacking;
    public float timeBtwAttack;
    Animator anim;
    public bool canAttack;
    public GameObject x;

    private void Start()
    {
        anim = GetComponent<Animator>();
        attackAreaScript = attackArea.GetComponent<AttackArea>();
    }
    public void UpgadeAttack(float dmg)
    {
        attackAreaScript.UpdateAttack(dmg);
    }
    public void UpgadeSpeed()
    {
        anim.speed = 1.4f;
        timeBtwAttack = 0.1f;
    }
    private void OnDisable()
    {
        isAttacking = false;
    }
    private void Update()
    {
        if(!isAttacking && Input.GetMouseButton(0) && canAttack)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
        }
    }
    public void Attack()
    {
        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        attackArea.SetActive(true);
        float time = timeBtwAttack;
        yield return new WaitForSeconds(time);
        attackArea.SetActive(false);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Range")
        {
            x.SetActive(false);
            canAttack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Range")
        {
            x.SetActive(true);
            canAttack = false;
        }
    }
}
