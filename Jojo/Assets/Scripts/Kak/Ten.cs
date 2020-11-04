using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ten : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rb;
    public int damage = 1;
    public Animator tenAnim;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        JotaroHealth jotaro = hitInfo.GetComponent<JotaroHealth>();
        JotaroCombat jotaroCombat = hitInfo.GetComponent<JotaroCombat>();
        if (jotaro != null)
        {
            if (Input.GetKey(KeyCode.P) == false)
            {
                tenAnim.SetTrigger("Attack");
                jotaro.TakeDamage(damage);
                GameObject j = GameObject.FindGameObjectWithTag("Player");
                JotaroMovement moveScript = j.GetComponent<JotaroMovement>();
                moveScript.enabled = false;
                moveScript.rb.velocity = transform.right * 0;
                rb.velocity = transform.right * 0;
                Invoke("ResumeMovement", 1);
            }
            else
            {
                jotaroCombat.Block();
            }
        }
    }

    void ResumeMovement()
    {

        GameObject jotaro = GameObject.FindGameObjectWithTag("Player");
        jotaro.GetComponent<JotaroMovement>().enabled = true;
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
