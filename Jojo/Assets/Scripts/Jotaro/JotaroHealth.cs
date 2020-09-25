using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JotaroHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public Animator jotaro;

    public HealthBar healthBar;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        jotaro.SetTrigger("Hurt");
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
