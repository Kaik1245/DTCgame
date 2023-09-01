using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : GunType
{
    Player player;
    Camera GameCamera;
    public NormalBullet bullet;
    public float FireRate = 3;
    private bool IsReloading = false;
    public bool BulletExplosion;

    void Start()
    {
        bullet.ExplodesInCollision = true;
        player = FindAnyObjectByType<Player>();
        GameCamera = FindAnyObjectByType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, RotateTowardsPosition(transform.position, GameCamera.ScreenToWorldPoint(Input.mousePosition), 0));

        if (Input.GetMouseButtonDown(0) && !IsReloading)
        {
            ShootBullet(bullet, transform.position, transform.eulerAngles.z, 0);
            StartCoroutine(ShotsCooldown());
        }
    }
    IEnumerator ShotsCooldown()
    {
        IsReloading = true;
        yield return new WaitForSeconds(FireRate);
        IsReloading = false;
    }
}