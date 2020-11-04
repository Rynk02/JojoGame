using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jean : MonoBehaviour
{
    Transform player;
    public Rigidbody2D rb;
    public Animator jeanAnim;
    public Animator silverAnim;
    public float speed = 2.5f;
    public float attackRange = 3f;

    public bool isFlipped = false;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int maxHealth = 10;
    int currentHealth;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("SilverC");
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        jeanAnim.SetBool("isRunning", true);
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if(Vector2.Distance(player.position, rb.position) <= attackRange && Time.time >= nextAttackTime)
        {
            if(currentHealth <= 5)
            {
          
                if(Random.Range(0,2) == 1)
                {
                    silverAnim.SetTrigger("AttackOne");
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                else
                {
                    silverAnim.SetTrigger("AttackTwo");
                    Attack2();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            else
            {
                silverAnim.SetTrigger("AttackOne");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }
    }

    void Attack()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        JotaroHealth jotaro = go.GetComponent<JotaroHealth>();
        JotaroCombat jotaroCombat = go.GetComponent<JotaroCombat>();
        if (Input.GetKey(KeyCode.P) == false)
        {
            jotaro.TakeDamage(1);
        }
        else
        {
            jotaroCombat.Block();
        }
        
    }

    void Attack2()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        JotaroHealth jotaro = go.GetComponent<JotaroHealth>();
        JotaroCombat jotaroCombat = go.GetComponent<JotaroCombat>();
        if (Input.GetKey(KeyCode.P) == false)
        {
            jotaro.TakeDamage(2);
        }
        else
        {
            jotaroCombat.Block();
        }
    }
    public void TakeDamage(int damage)
    {
        jeanAnim.SetTrigger("Hurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject silver = GameObject.Find("Silver");
        silver.SetActive(false);
        jeanAnim.SetBool("isDead", true);
        this.enabled = false;
        Invoke("LoadNextScene", 4);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}

