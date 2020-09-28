using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JotaroCombat : MonoBehaviour
{

    public Animator starAnim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask EnemyLayers;

    public int attackDamage = 1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.O) && Input.GetKey(KeyCode.P) == false)
            {
                FindObjectOfType<AudioManager>().Play("Ora");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKey(KeyCode.P))
            {
                starAnim.SetTrigger("Block");
            }
        }
    }

    void Attack()
    {
        starAnim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                enemy.GetComponent<KakScript>().TakeDamage(attackDamage);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                enemy.GetComponent<Jean>().TakeDamage(attackDamage);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                enemy.GetComponent<Dio>().TakeDamage(attackDamage);
            }
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
