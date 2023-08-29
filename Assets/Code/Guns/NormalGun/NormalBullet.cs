using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletType
{
    public float Speed = 15;
    Rigidbody2D rb;
    public float TimeAlive = 5;
    float ActualTime = 0;
    public float TimeToAdd = 0.05f;
    public float DamageAmount = 3.4f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void InitiateVariables()
    {
        
    }
    void Start()
    {
        rb.AddRelativeForce(Vector2.right * Speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(ActualTime >= TimeAlive)
        {
            Destroy(this.gameObject);
        }
        ActualTime += TimeToAdd;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "EnemyTypeA")
        {
            EnemyTypeA enemy = other.GetComponent<EnemyTypeA>();
            enemy.health -= DamageAmount;
            Destroy(this.gameObject);
        }
    }
}
