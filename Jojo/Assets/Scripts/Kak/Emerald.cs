using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emerald : MonoBehaviour
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
            jotaro.TakeDamage(damage);
        }
        Destroy(gameObject);    
    }
}
