using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTypeB : MonoBehaviour
{
    Transform PlayerPos;
    public float EnemyVelocity = 15;
    public Rigidbody2D rb;
    public float health = 1f;
    public int damage;
    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>().gameObject;
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
        float dx = PlayerPos.position.x - transform.position.x;
        float dy = PlayerPos.position.y - transform.position.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Vector2 MoveDirection = (PlayerPos.position - transform.position).normalized;
        rb.velocity = MoveDirection * EnemyVelocity * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.GetComponent<Player>().PlayerHealth -= damage;
            gameManager.GetComponent<GameManager>().loose = true;
        }
    }
}
