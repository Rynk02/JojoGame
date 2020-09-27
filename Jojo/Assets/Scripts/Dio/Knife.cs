using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 1;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        JotaroHealth jotaro = hitInfo.GetComponent<JotaroHealth>();
        if (jotaro != null)
        {
            if (Input.GetKey(KeyCode.P) == false)
            {
                jotaro.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
