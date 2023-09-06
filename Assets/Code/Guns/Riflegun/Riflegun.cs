using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riflegun : GunType
{
    Player player;
    Camera GameCamera;
    public RifleBullet bullet;
    public float FireRate = 3;
    private bool IsReloading = false;
    public bool BulletExplosion;
    private GameManager manager;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        GameCamera = FindAnyObjectByType<Camera>();
        manager = FindAnyObjectByType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, RotateTowardsPosition(transform.position, GameCamera.ScreenToWorldPoint(Input.mousePosition), 0));

        if (Input.GetMouseButtonDown(0) && !IsReloading)
        {
            ShootBullet(bullet, transform.position, transform.eulerAngles.z, 0);
            manager.SoundManager.StartWeaponShoot();
            player.HasShot = true;
            StartCoroutine(ShotsCooldown());
        }
    }
    IEnumerator ShotsCooldown()
    {
        IsReloading = true;
        yield return new WaitForSeconds(FireRate);
        IsReloading = false;
        player.HasShot = false;
    }
}
