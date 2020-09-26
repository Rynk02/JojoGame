using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JotaroCombat : MonoBehaviour
{

    public Animator starAnim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask EnemyLayers;

    public int attackDamage = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Input.GetKey(KeyCode.P) == false)
        {
            Attack();
        }
        if (Input.GetKey(KeyCode.P))
        {
            starAnim.SetTrigger("Block");
        }
    }

    void Attack()
    {
        starAnim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<KakScript>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);    
    }

}
