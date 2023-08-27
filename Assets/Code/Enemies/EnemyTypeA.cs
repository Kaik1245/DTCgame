using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeA : MonoBehaviour
{
    Transform PlayerPos;
    public float EnemyVelocity = 15;
    Rigidbody2D rb;
    float OffsetX = .4f;
    public float health = 1f; 
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerPos.position.x > transform.position.x + OffsetX)
        {
            rb.velocity = new Vector2(EnemyVelocity * Time.deltaTime, rb.velocity.y);
        }
        else if(PlayerPos.position.x < transform.position.x - OffsetX)
        {
            rb.velocity = new Vector2(-EnemyVelocity * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
