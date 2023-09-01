using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunType
{
    Player player;
    Camera GameCamera;
    public ShotgunBullet bullet;
    public float FireRate = 3;
    private bool IsReloading = false;
    public float MaxBulletAngle;
    public float BulletAngleSpacing;
    public float RecoilAmount;
    public bool BulletExplosion;


    void Start()
    {
        player = FindAnyObjectByType<Player>();
        GameCamera = FindAnyObjectByType<Camera>();
        bullet.ExplodesInCollision = true;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, RotateTowardsPosition(transform.position, GameCamera.ScreenToWorldPoint(Input.mousePosition), 0));

        if (Input.GetMouseButtonDown(0) && !IsReloading)
        { 
            for(float i = -MaxBulletAngle; i < MaxBulletAngle; i += BulletAngleSpacing)
            {
                ShootBullet(bullet, transform.position, transform.eulerAngles.z - i, 0);
            }
            player.SetVelocity(Recoil(RecoilAmount, GameCamera.ScreenToWorldPoint(Input.mousePosition), player.transform.position, 90));
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
