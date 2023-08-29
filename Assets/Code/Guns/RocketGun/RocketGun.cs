using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGun : GunType
{
    Player player;
    Camera GameCamera;
    public RocketBullet bullet;
    public int MaxShotAmount = 16;
    public float CoolDownTime = 3;
    public float ReloadTime = 3;
    private int ActualShotAmount = 0;
    public float RecoilAmount = 5;
    private bool IsReloading = false;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        GameCamera = FindAnyObjectByType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, RotateTowardsPosition(transform.position, GameCamera.ScreenToWorldPoint(Input.mousePosition), 0));

        if(!IsReloading)
        {
            Shoot();
        }
        ManualReloadCheck();
    }
    IEnumerator ShotsCooldown()
    {
        IsReloading = true;
        yield return new WaitForSeconds(CoolDownTime);
        ResetGun();
    }
    IEnumerator ReloadCoolDown()
    {
        IsReloading = true;
        yield return new WaitForSeconds(ReloadTime);
        ResetGun();
    }
    public void ResetGun()
    {
        ActualShotAmount = 0;
        IsReloading = false;
    }
    void ManualReloadCheck()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadCoolDown());
        }
    }
    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(ActualShotAmount < MaxShotAmount)
                {
                    player.SetVelocity(Recoil(RecoilAmount, GameCamera.ScreenToWorldPoint(Input.mousePosition), player.transform.position, 90));
                    ShootBullet(bullet, transform.position, transform.eulerAngles.z);
                    ActualShotAmount += 1;
                }
                else
                {
                    StartCoroutine(ShotsCooldown());
                }
        }
    }
}
