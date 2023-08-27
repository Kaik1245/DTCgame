using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : BulletType
{
    public float Speed = 15;
    Rigidbody2D rb;
    public float TimeAlive = 5;
    float ActualTime = 0;
    public float TimeToAdd = 0.05f;
    public float DamageAmount = 3.4f;
    public float ExplosionRadius = 1;

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
        if(other.tag != "Player")
        {
        var Colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach(var hitColliders in Colliders)
        {
                var ClosestHitPosition = hitColliders.ClosestPoint(transform.position);
                var distance = Vector3.Distance(ClosestHitPosition, transform.position);
                var DamagePercent = Mathf.InverseLerp(ExplosionRadius, 0, distance);
                print(DamagePercent + " " + hitColliders.tag);
        }
        }
    }
}
