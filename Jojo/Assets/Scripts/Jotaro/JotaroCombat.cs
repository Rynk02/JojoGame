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

    public float blockRate = 2f;
    float nextBlockTime = 0f;
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.O) && !Input.GetKey(KeyCode.P))
            {
                FindObjectOfType<AudioManager>().Play("Ora");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }
        if (Time.time >= nextBlockTime)
        {
            if (Input.GetKey(KeyCode.P) && !Input.GetKeyDown(KeyCode.O))
            {
                starAnim.SetTrigger("Block");
            }
        }
    }

    public void Block()
    {
        nextBlockTime = Time.time + 1f / blockRate;
    }
    void Attack()
    {
        starAnim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            switch (buildIndex)
            {
                case 1:
                    enemy.GetComponent<KakScript>().TakeDamage(attackDamage);
                    break;
                case 2:
                    enemy.GetComponent<Jean>().TakeDamage(attackDamage);
                    break;
                case 3:
                    enemy.GetComponent<Dio>().TakeDamage(attackDamage);
                    break;
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
