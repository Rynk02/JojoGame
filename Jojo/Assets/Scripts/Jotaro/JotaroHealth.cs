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
        GameObject star = GameObject.Find("StarP");
        star.GetComponent<Renderer>().enabled = false;
        jotaroAnim.SetBool("isDead", true);
        jotaro = GameObject.FindGameObjectWithTag("Player");
        JotaroMovement moveScript = jotaro.GetComponent<JotaroMovement>();
        moveScript.rb.velocity = transform.right * 0;
        jotaro.GetComponent<JotaroMovement>().enabled = false;
        Invoke("LoadDeathScene", 5);

    }

    void LoadDeathScene()
    {
        SceneManager.LoadScene("Dead");
    }
}
