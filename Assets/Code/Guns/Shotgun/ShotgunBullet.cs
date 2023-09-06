using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : BulletType
{
    public float Speed = 15;
    Rigidbody2D rb;
    public float TimeAlive = 5;
    float ActualTime = 0;
    public float TimeToAdd = 0.05f;
    public float DamageAmount = 3.4f;
    public float ExplosionRadius;
    public float ExplosionBlastAmount;
    public float ExplosionDamageAmount;
    public float SpeedRandomizer;
    private SoundEffectsManager SoundManager;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void InitiateVariables()
    {

    }
    void Start()
    {
        float RandomSpeed = Random.Range(Speed - SpeedRandomizer, Speed);
        rb.AddRelativeForce(Vector2.right * RandomSpeed);
        SoundManager = FindAnyObjectByType<SoundEffectsManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (ActualTime >= TimeAlive)
        {
            Destroy(this.gameObject);
        }
        ActualTime += TimeToAdd;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            SoundManager.StartBulletHit();
            if (other.tag == "EnemyTypeA")
            {
                EnemyTypeA enemy = other.GetComponent<EnemyTypeA>();
                enemy.health -= DamageAmount;
                Destroy(this.gameObject);
            }
            else if (other.tag == "Ground")
            {
                Destroy(this.gameObject);
            }
            else if (other.tag == "EnemyTypeB")
            {
                EnemyTypeB enemy = other.GetComponent<EnemyTypeB>();
                enemy.health -= DamageAmount;
                Destroy(this.gameObject);
            }
            else if (other.tag == "EnemyTypeC")
            {
                EnemyTypeC enemy = other.GetComponent<EnemyTypeC>();
                enemy.health -= DamageAmount;
                Destroy(this.gameObject);
            }
        }
    }
}
