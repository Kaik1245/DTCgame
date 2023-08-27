using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

// Help me 
// TO DO: Fix shooting cool down
public class Player : MonoBehaviour
{
    // Jump
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _downwardMovementMultiplier = 3f;
    [SerializeField] private float _upwardMovementMultiplier = 1.7f;

    // Movement
    [SerializeField] private float _maxSpeed = 4f;
    [SerializeField] private float _maxAcceleration = 35f;
    [SerializeField] private float _maxAirAcceleration = 20f;

    private Rigidbody2D rb;
    public float RaycastDistance = 1;
    public float GroundFriction = 0.1f;
    private SpriteRenderer sprite;

    // jump
    private float DefaultGravityScale, JumpSpeed;
    private bool DesiredJump, OnGround;

    // Movement
    private Vector2 _desiredVelocity;
    private float _maxSpeedChange, _acceleration;

    public GunType gun1;
    public GunType gun2;
    private GameObject Gun1Instance;
    private GameObject Gun2Instance;
    public LayerMask GroundedLayers;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        DefaultGravityScale = 1;
        sprite = gameObject.GetComponent<SpriteRenderer>();

        Gun1Instance = Instantiate(gun1, transform).gameObject;
        Gun2Instance = Instantiate(gun2, transform).gameObject;
        Gun1Instance.SetActive(true);
        Gun2Instance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Grounded();
        if(gun1 is LaserGun && Gun1Instance.activeInHierarchy)
            {
                if(Gun1Instance.GetComponent<LaserGun>().ActualTimeStillBeforeShooting == 0)
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        DesiredJump = true;
                        Jump();
                    }
                    else
                    {
                        DesiredJump = false;
                    }
                    _desiredVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * (_maxSpeed - GroundFriction), 0f);
                }
            }
            else if(gun2 is LaserGun && Gun2Instance.activeInHierarchy)
            {
                if(Gun2Instance.GetComponent<LaserGun>().ActualTimeStillBeforeShooting == 0)
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        DesiredJump = true;
                        Jump();
                    }
                    else
                    {
                        DesiredJump = false;
                    }
                    _desiredVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * (_maxSpeed - GroundFriction), 0f);
                }
            }
            else{

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    DesiredJump = true;
                    Jump();
                }
                else
                {
                    DesiredJump = false;
                }
                _desiredVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * (_maxSpeed - GroundFriction), 0f);
            }
        if(_desiredVelocity.x > 0)
        {
            sprite.flipX = true;
        }
        else if(_desiredVelocity.x < 0)
        {
            sprite.flipX = false;
        }
        ChangeGun();
    }
    void ChangeGun()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(Gun1Instance.activeSelf == true)
            {
                if(gun1 is ChargeGun)
                {
                    Gun1Instance.GetComponent<ChargeGun>().ResetGun();
                }
                else if(gun1 is NormalGun)
                {
                    Gun1Instance.GetComponent<NormalGun>().ResetGun();
                }
                else if(gun1 is LaserGun)
                {
                    Gun1Instance.GetComponent<LaserGun>().ResetGun();
                }
                else if(gun1 is RocketGun)
                {
                    Gun1Instance.GetComponent<RocketGun>().ResetGun();
                }
                else if(gun1 is Grenade)
                {
                    Gun1Instance.GetComponent<Grenade>().ResetGun();
                }
                Gun2Instance.SetActive(true);
                Gun1Instance.SetActive(false);

            }
            else if(Gun2Instance.activeSelf == true)
            {
                if(gun2 is ChargeGun)
                {
                    Gun2Instance.GetComponent<ChargeGun>().ResetGun();
                }
                else if(gun2 is NormalGun)
                {
                    Gun2Instance.GetComponent<NormalGun>().ResetGun();
                }
                else if(gun2 is LaserGun)
                {
                    Gun2Instance.GetComponent<LaserGun>().ResetGun();
                }
                else if(gun2 is RocketGun)
                {
                    Gun2Instance.GetComponent<RocketGun>().ResetGun();
                }
                else if(gun2 is Grenade)
                {
                    Gun2Instance.GetComponent<Grenade>().ResetGun();
                }

                Gun2Instance.SetActive(false);
                Gun1Instance.SetActive(true);
            }
        }
    }
    private void FixedUpdate() {
            if(gun1 is LaserGun && Gun1Instance.activeInHierarchy)
            {
                if(Gun1Instance.GetComponent<LaserGun>().ActualTimeStillBeforeShooting == 0)
                {
                    Move();
                }
            }
            else if(gun2 is LaserGun && Gun2Instance.activeInHierarchy)
            {
                if(Gun2Instance.GetComponent<LaserGun>().ActualTimeStillBeforeShooting == 0)
                {
                    Move();
                }
            }
            else{
                Move();
            }
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity += velocity;
    }
    void Move()
    {
            if(OnGround)
            {
                _acceleration = _maxAcceleration;
            }
            else
            {
                _acceleration = _maxAirAcceleration;
            }

            _maxSpeedChange = _acceleration * Time.deltaTime;

            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, _desiredVelocity.x, _maxSpeedChange), rb.velocity.y);
    }
    void Jump()
    {
        if(DesiredJump)
        {
            DesiredJump = false;
            JumpAction();
        }
        // if I am jumping
        if(rb.velocity.y > 0)
        {
            rb.gravityScale = _upwardMovementMultiplier;
        }
        // if I am falling
        else if(rb.velocity.y < 0)
        {
            rb.gravityScale = _downwardMovementMultiplier;
        }
        // if not falling or jumping
        else if(rb.velocity.y == 0)
        {
            rb.gravityScale = DefaultGravityScale;
        }
    }
    void Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RaycastDistance, GroundedLayers);
        if(hit.collider != null)
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
    }
    void JumpAction()
    {
        if (OnGround)
            {
                JumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
                
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
    }
}