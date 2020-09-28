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
    public Animator greenAnim;

    //Emerald Splash
    public Transform firePoint;
    public GameObject emeraldPrefab;
    //Tentacle
    public Transform firePoint2;
    public GameObject tenPrefab;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("EmeraldSplash");
        currentHealth = maxHealth;
        StartCoroutine("AutoAttack");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AttackOne();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            AttackTwo();
        }
    }


    IEnumerator AutoAttack()
    {
        while (currentHealth > 5)
        {
            yield return new WaitForSeconds(2);
            AttackOne();
        }
        while (currentHealth <= 5 && currentHealth > 0)
        {
            AttackOne();
            yield return new WaitForSeconds(1);
            AttackTwo();
            yield return new WaitForSeconds(1);
        }
    }

    void AttackOne()
    {
        greenAnim.SetTrigger("Emerald");
        Instantiate(emeraldPrefab, firePoint.position, firePoint.rotation);
    }

    void AttackTwo()
    {
        greenAnim.SetTrigger("Ten");
        Instantiate(tenPrefab, firePoint2.position, firePoint2.rotation);
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
        GameObject green = GameObject.Find("Green");
        green.SetActive(false);
        kakAnim.SetBool("isDead", true);
        this.enabled = false;
        Invoke("LoadNextScene", 4);
        
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
