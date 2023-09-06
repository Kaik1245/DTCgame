using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeCBullet : MonoBehaviour
{
    public float Speed = 15;
    Rigidbody2D rb;
    public float TimeAlive = 5;
    float ActualTime = 0;
    public int DamageAmount = 40;
    public float Health = 1;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.AddRelativeForce(Vector2.right * Speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (ActualTime >= TimeAlive || Health <= 0)
        {
            Destroy(this.gameObject);
        }
        ActualTime += Time.deltaTime;
    }
    void FixedUpdate()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().PlayerHealth -= DamageAmount;
            other.GetComponent<Player>().SoundEffects.StartGetHit();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        else if (other.tag == "Bullet")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
