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

    public float attackRange = 1f;
    bool attack = false;
    public Transform startPos;
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("AutoAttack");
        FindObjectOfType<AudioManager>().Play("DioIntro");
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            Vector2 target = new Vector2(startPos.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (Vector2.Distance(startPos.position, rb.position) <= 0.1)
            {
                transform.Rotate(0f, 180f, 0f);
                start = false;
            }
        }
        if (attack)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                dioAnim.SetTrigger("Attack");
                attack = false;
                GameObject go = GameObject.FindGameObjectWithTag("Player");
                JotaroHealth jotaro = go.GetComponent<JotaroHealth>();
                if (Input.GetKey(KeyCode.P) == false)
                {
                    jotaro.TakeDamage(2);
                }
            }
        }
    }

    IEnumerator AutoAttack()
    {
        yield return new WaitForSeconds(4);
        while (currentHealth > 10)
        {
            yield return new WaitForSeconds(2);
            dioAnim.SetTrigger("Knife");
        }
        while (currentHealth <= 10 && currentHealth > 1)
        {
            if (Random.Range(0, 2) == 1)
            {
                Attack();
                yield return new WaitForSeconds(2);
                backToStart();
                yield return new WaitForSeconds(2);
            }
            else
            {
                dioAnim.SetTrigger("Knife");
                yield return new WaitForSeconds(2);
            }
        }
        if (currentHealth == 1)
        {
            Debug.Log("1Hp");
        }
    }

    void backToStart()
    {
        transform.Rotate(0f, 180f, 0f);
        dioAnim.SetTrigger("isRunning");
        start = true;
    }
    public void Attack()
    {
        dioAnim.SetTrigger("isRunning");
        attack = true;
    }
    public void Shoot()
    {
        Instantiate(knifePrefab, firePoint.position, firePoint.rotation);
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
