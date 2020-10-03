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
    public GameObject star;
    public JotaroMovement moveScript;
    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Die()
    {
        star.GetComponent<Renderer>().enabled = false;
        jotaroAnim.SetBool("isDead", true);
        moveScript.rb.velocity = transform.right * 0;
        moveScript.enabled = false;
        Invoke("LoadDeathScene", 5);

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

    void LoadDeathScene()
    {
        SceneManager.LoadScene("Dead");
    }
}
