using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KakScript : MonoBehaviour
{
    //Health
    public int maxHealth = 10;
    int currentHealth;
    public Animator kakAnim;

    //Emerald Splash
    public Transform firePoint;
    public GameObject bulletPrefab;
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    public void TakeDamage(int damage)
    {
        kakAnim.SetTrigger("Hurt");
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        kakAnim.SetBool("isDead", true);
        Debug.Log("Kak Dead");
        this.enabled = false;
        Invoke("LoadNextScene", 4);
        
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
