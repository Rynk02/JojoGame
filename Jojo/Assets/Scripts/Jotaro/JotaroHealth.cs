using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JotaroHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public Animator jotaroAnim;
    private GameObject jotaro;
    public HealthBar healthBar;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        jotaroAnim.SetTrigger("Hurt");
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        jotaroAnim.SetBool("isDead", true);
        jotaro = GameObject.FindGameObjectWithTag("Player");
        jotaro.GetComponent<JotaroMovement>().enabled = false;
        Invoke("LoadDeathScene", 4);

    }

    void LoadDeathScene()
    {
        SceneManager.LoadScene("Dead");
    }
}
