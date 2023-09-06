using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

// Help me 
// TO DO: Fix shooting cool down
public class Player : MonoBehaviour
{
    // Jump
    [SerializeField] private float JumpHeight = 3f;
    [SerializeField] private float DownwardMovementMultiplier = 3f;
    [SerializeField] private float UpwardMovementMultiplier = 1.7f;
    public int AllowedAmountToJump = 2;
    int JumpAmount;

    // Movement
    [SerializeField] private float MaxSpeed = 4f;
    [SerializeField] private float MaxAcceleration = 35f;
    [SerializeField] private float MaxAirAcceleration = 20f;

    public Rigidbody2D rb;
    public float RaycastDistance = 1;
    public float GroundFriction = 0.1f;
    private SpriteRenderer sprite;

    // jump
    private float DefaultGravityScale, JumpSpeed;
    private bool DesiredJump, OnGround;

    // Movement
    private Vector2 DesiredVelocity;
    private float MaxSpeedChange, acceleration;

    public GunType ChosenGun;
    private GameObject GunInstance;
    public LayerMask GroundedLayers;
    private GameObject WeaponsManager;
    public int PlayerHealth;
    public GameObject gameManager;
    public int MaxHealth;
    public bool HasShot = false;
    public SoundEffectsManager SoundEffects;

    // Particle system
    public ParticleSystem dust;

    public Animator PlayerAimator;

    void Awake()
    {
        PlayerHealth = MaxHealth;
        WeaponsManager = FindObjectOfType<WeaponSelectionManager>().gameObject;
        ChosenGun = WeaponsManager.GetComponent<WeaponSelectionManager>().ChosenGun;
        if (WeaponsManager.GetComponent<WeaponSelectionManager>().Explodes)
        {
            if (ChosenGun is Riflegun)
            {
                ChosenGun.GetComponent<Riflegun>().BulletExplosion = true;
            }
            else if (ChosenGun is NormalGun)
            {
                ChosenGun.GetComponent<NormalGun>().BulletExplosion = true;
            }
            else if (ChosenGun is Shotgun)
            {
                ChosenGun.GetComponent<Shotgun>().BulletExplosion = true;
            }
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        DefaultGravityScale = 1;
        sprite = gameObject.GetComponent<SpriteRenderer>();

        if (ChosenGun != null)
        {
            GunInstance = Instantiate(ChosenGun, transform).gameObject;
            GunInstance.SetActive(true);
        }
        JumpAmount = 0;
    }
    void Start()
    {
        SoundEffects = gameManager.GetComponent<GameManager>().SoundManager;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth <= 0)
        {
            gameManager.GetComponent<GameManager>().loose = true;
            Destroy(this.gameObject);
        }
        Grounded();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DesiredJump = true;
        }
        else
        {
            DesiredJump = false;
        }
        DesiredVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * (MaxSpeed - GroundFriction), 0f);
        if (DesiredVelocity.x > 0 && sprite.flipX == true)
        {
            sprite.flipX = false;
            if (OnGround)
            {
                CreateDust();
            }
        }
        else if (DesiredVelocity.x < 0 && sprite.flipX == false)
        {
            sprite.flipX = true;
            if(OnGround)
            {
                CreateDust();
            }
        }
        if (JumpAmount != 0 && JumpAmount >= AllowedAmountToJump)
        {
            if (OnGround)
            {
                JumpAmount = 0;
            }
        }
        Jump();
        PlayerAimator.SetFloat("Speed", Mathf.Abs(DesiredVelocity.x));
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity += velocity;
    }
    void Move()
    {
        if (OnGround)
        {
            acceleration = MaxAcceleration;
        }
        else
        {
            acceleration = MaxAirAcceleration;
        }

        MaxSpeedChange = acceleration * Time.deltaTime;

        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, DesiredVelocity.x, MaxSpeedChange), rb.velocity.y);
    }
    void CreateDust()
    {
        dust.Play();
    }
    void Jump()
    {
        if (DesiredJump)
        {
            DesiredJump = false;
            JumpAction();
        }
        // if I am jumping
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = UpwardMovementMultiplier;
        }
        // if I am falling
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = DownwardMovementMultiplier;
        }
        // if not falling or jumping
        else if (rb.velocity.y == 0)
        {
            rb.gravityScale = DefaultGravityScale;
        }
    }
    void Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RaycastDistance, GroundedLayers);
        if (hit.collider != null)
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "DeathGround")
        {
            PlayerHealth = 0;
            
        }
    }
    void JumpAction()
    {
        CreateDust();
        if (OnGround)
        {
            SoundEffects.StartJump();
            JumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * JumpHeight);

            if (rb.velocity.y > 0f)
            {
                JumpSpeed = Mathf.Max(JumpSpeed - rb.velocity.y, 0f);
            }
            else if (rb.velocity.y < 0f)
            {
                JumpSpeed += Mathf.Abs(rb.velocity.y);
            }
            rb.velocity += new Vector2(0, JumpSpeed);
        }
        else if(JumpAmount < AllowedAmountToJump)
        {
            SoundEffects.StartJump();
            JumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * JumpHeight);

            if (rb.velocity.y > 0f)
            {
                JumpSpeed = Mathf.Max(JumpSpeed - rb.velocity.y, 0f);
            }
            else if (rb.velocity.y < 0f)
            {
                JumpSpeed += Mathf.Abs(rb.velocity.y);
            }
            rb.velocity += new Vector2(0, JumpSpeed);
            JumpAmount++;
        }
    }
}