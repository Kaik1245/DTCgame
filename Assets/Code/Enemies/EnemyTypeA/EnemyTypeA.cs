using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeA : MonoBehaviour
{
    Transform PlayerPos;
    public float EnemyVelocity = 15;
    public Rigidbody2D rb;
    float OffsetX = .4f;
    public float health = 1f;
    public GameObject Gun;
    public EnemyTypeABullet bullet;
    public float FireRate;
    [Range(0, 100)] public int EnemySpawnProbability;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootTimer());

    }
    void Update()
    {
        if(health <= 0)
        {
            PlayerPos.GetComponent<Player>().SoundEffects.StartEnemyHit();
            Destroy(this.gameObject);
        }
        RotateTowardsPlayer();
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
    void RotateTowardsPlayer()
    {
        float dx = PlayerPos.position.x - transform.position.x;
        float dy = PlayerPos.position.y - transform.position.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        Gun.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    IEnumerator ShootTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(FireRate);
            PlayerPos.GetComponent<Player>().SoundEffects.StartEnemyShoot();
            shoot();
        }
    }
    void shoot()
    {
        var BulletInstance = Instantiate(bullet, Gun.transform.position, Quaternion.Euler(0, 0, Gun.transform.eulerAngles.z)).gameObject;
    } 
}
