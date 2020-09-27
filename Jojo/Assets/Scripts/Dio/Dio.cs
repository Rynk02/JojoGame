using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dio : MonoBehaviour
{
    public int maxHealth = 20;
    int currentHealth;

    public Animator dioAnim;
    public GameObject knifePrefab;
    public Transform firePoint;

    Transform player;
    public Rigidbody2D rb;
    public float speed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void TakeDamage(int damage)
    {
        dioAnim.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            dioAnim.SetTrigger("Knife");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack();
        }
        dioAnim.SetTrigger("isRunning");
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    public void Attack()
    {
        dioAnim.SetTrigger("isRunning");
    }
    public void Shoot()
    {
        Instantiate(knifePrefab, firePoint.position, firePoint.rotation);
    }

    void Die()
    {
        dioAnim.SetBool("isDead", true);
        Invoke("LoadNextScene", 4);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
