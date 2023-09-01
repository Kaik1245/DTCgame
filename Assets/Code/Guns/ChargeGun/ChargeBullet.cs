using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

public class ChargeBullet : BulletType
{
    public float MaxVelocity = 15;
    Rigidbody2D rb;
    public float TimeAlive = 5;
    float ActualTime = 0;
    public float TimeToAdd = 0.05f;
    public float MaxDamageAmount = 3.4f;
    public float ActualDamageAmount = 0;
    public float ExplosionRadius;
    public float ExplosionBlastAmount;
    public float ExplosionDamageAmount;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void InitiateVariables()
    {
        
    }
    void Start()
    {
        rb.AddRelativeForce(Vector2.right * MaxVelocity);
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
    public void SetVelocity(Vector2 velocity)
    {
         rb.velocity += velocity;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "EnemyTypeA")
        {
            EnemyTypeA enemy = other.GetComponent<EnemyTypeA>();
            if(ExplodesInCollision)
            {
                enemy.health -= ExplosionDamageAmount;
            }
            else{
                enemy.health -= ActualDamageAmount;
            }
            Destroy(this.gameObject);
        }
        else if(other.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        else if (other.tag != "EnemyTypeA" && other.tag != "Bullet" && ExplodesInCollision)
        {
            Explode(ExplosionRadius, ExplosionDamageAmount, ExplosionBlastAmount);
        }
    }
}
