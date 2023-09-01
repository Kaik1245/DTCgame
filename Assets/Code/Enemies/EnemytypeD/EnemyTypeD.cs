using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeD : MonoBehaviour
{
    Transform PlayerPos;
    public float EnemyVelocity = 15;
    public Rigidbody2D rb;
    float OffsetX = .4f;
    public float health = 1f;
    public float TimerUntilExplosion;
    public float JumpHeight;
    public float ExplosionRadius;
    public int MaxDamageAmount;
    public float BlastForce;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ExplosionTimer());
    }
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPos.position.x > transform.position.x + OffsetX)
        {
            rb.velocity = new Vector2(EnemyVelocity * Time.deltaTime, rb.velocity.y);
        }
        else if (PlayerPos.position.x < transform.position.x - OffsetX)
        {
            rb.velocity = new Vector2(-EnemyVelocity * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (transform.position.y >= PlayerPos.position.y)
        {
            Explode(ExplosionRadius, MaxDamageAmount, BlastForce);
            Destroy(gameObject);
        }    
    }
    IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(TimerUntilExplosion);
        rb.velocity += new Vector2(0, JumpHeight);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public void Explode(float ExplosionRadius, int DamageAmount, float BlastAmount)
    {
        var Colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (var HitCollider in Colliders)
        {
            if (HitCollider.tag == "Player")
            {
                Vector2 ClosestHitPosition = HitCollider.ClosestPoint(transform.position);
                float distance = Vector3.Distance(ClosestHitPosition, transform.position);
                float DamagePercent = Mathf.InverseLerp(ExplosionRadius, 0, distance);

                // Add explosionForce
                if (BlastAmount != 0)
                {
                    Vector2 DistanceVector = (Vector2)HitCollider.transform.position - (Vector2)transform.position;
                    float ExplosionForce = BlastAmount / DistanceVector.magnitude;
                    HitCollider.GetComponent<Player>().rb.AddForce(DistanceVector.normalized * ExplosionForce);
                    HitCollider.GetComponent<Player>().PlayerHealth -= (int)DamagePercent * DamageAmount;
                }
            }
        }
    }
}
