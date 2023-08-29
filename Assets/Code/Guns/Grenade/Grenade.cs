using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : GunType
{
    public float MinDragDistance;
    public float MaxDragDistance;
    Camera GameCamera;
    Vector2 Distance;
    Vector2 StartPos;
    float DistanceSquared;
    public float ThrowForceMax;
    public float ThrowForceMin;
    Rigidbody2D rb;
    public float TimerUntilExplosion;
    public float ExplosionRadius;
    public bool GrenadeThrown;
    Player player;
    bool IsReloading;

    // Start is called before the first frame update
    void Start()
    {
        GameCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody2D>();
        GrenadeThrown = false;
        player = FindObjectOfType<Player>();
        Physics2D.IgnoreLayerCollision(6, 7, true);
        IsReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsReloading)
        {
            if(Input.GetMouseButtonDown(0))
        {
            if(!GrenadeThrown)
            {
                transform.position = player.transform.position;
                rb.velocity = Vector2.zero;
                GetComponent<SpriteRenderer>().enabled = true;
            }
            StartPos = GameCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButton(0))
        {
            if(!GrenadeThrown)
            {
                transform.position = player.transform.position;
                rb.velocity = Vector2.zero;
                GetComponent<SpriteRenderer>().enabled = true;
            }
            Distance = (Vector2)GameCamera.ScreenToWorldPoint(Input.mousePosition) - StartPos;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(!GrenadeThrown)
            {
            DistanceSquared = Mathf.Sqrt(Mathf.Pow(Distance.x, 2) + Mathf.Pow(Distance.y, 2));
            if(DistanceSquared < MinDragDistance)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else if(DistanceSquared >= MaxDragDistance)
            {
                GrenadeThrown = true;
                rb.velocity += Recoil(ThrowForceMax, GameCamera.ScreenToWorldPoint(Input.mousePosition), StartPos, 90);
                StartCoroutine(ExplosionTimer());
            }
            else
            {
                GrenadeThrown = true;
                float CalcForce = DistanceSquared / MaxDragDistance * ThrowForceMax;
                rb.velocity += Recoil(CalcForce, GameCamera.ScreenToWorldPoint(Input.mousePosition), StartPos, 90);
                StartCoroutine(ExplosionTimer());
            }
            }
        }
        else{
            if(!GrenadeThrown)
            {
                transform.position = player.transform.position;
                rb.velocity = Vector2.zero;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        }
    }
    public void ResetGun()
    {
        GrenadeThrown = false;
        transform.position = player.transform.position;
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
    }
    IEnumerator ReloadGrenade()
    {
        IsReloading = true;
        yield return new WaitForSeconds(3);
        IsReloading = false;
    }
    void Explode()
    {
        var Colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach(var hitColliders in Colliders)
        {
            if(hitColliders.tag != "Grenade")
            {
                var ClosestHitPosition = hitColliders.ClosestPoint(transform.position);
                var distance = Vector3.Distance(ClosestHitPosition, transform.position);
                var DamagePercent = Mathf.InverseLerp(ExplosionRadius, 0, distance);
                print(DamagePercent + " " + hitColliders.tag);
            }
        }
        ResetGun();
        StartCoroutine(ReloadGrenade());
    }
    IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(TimerUntilExplosion);
        Explode();
    }
}
