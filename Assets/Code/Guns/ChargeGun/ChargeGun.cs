using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class ChargeGun : GunType
{
    Player player;
    Camera GameCamera;
    public ChargeBullet bullet;
    public int MaxShotAmount = 16;
    public float CoolDownTime = 3;
    public float ReloadTime = 3;
    public int ActualShotAmount = 0;
    
    public float MaxRecoilAmount = 5;
    public float MinRecoilAmount = 2;
    // This variable represents the amount of time requiredto reach MaxRecoilAmount
    public float MaxHoldTime = 3;
    public float MinHoldTime = .7f;
    public float ActualHoldTime = 0;
    public float TimeIncrement;
    public bool IsReloading = false;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        GameCamera = FindAnyObjectByType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
            if(player.IsMovingControllerRightJoystick){
                transform.rotation = Quaternion.Euler(0, 0, RotateTowardsPosition(transform.position, player.MousePosInCameraSpace, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, RotateTowardsPosition(transform.position, GameCamera.ScreenToWorldPoint(player.MousePosInCameraSpace), 0));
            }

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
        if(player.IsHoldingShoot)
        {
            ActualHoldTime += TimeIncrement * Time.deltaTime;
            //print(ActualHoldTime);
        }
        else
        {
            if(ActualHoldTime > 0)
            {
            if(ActualHoldTime <= MinHoldTime)
            {
                if(player.IsMovingControllerRightJoystick)
                {
                    player.SetVelocity(Recoil(MinRecoilAmount, player.MousePosInCameraSpace, player.transform.position, 90));
                }
                else
                {
                    player.SetVelocity(Recoil(MinRecoilAmount, GameCamera.ScreenToWorldPoint(player.MousePosInCameraSpace), player.transform.position, 90));
                }

                if(ActualShotAmount <= MaxShotAmount)
                {
                    ShootBullet(bullet, transform.position, transform.eulerAngles.z);
                    ActualShotAmount += 1;
                }
                else
                {
                    StartCoroutine(ShotsCooldown());
                }
            }
            else
            {
                if(ActualHoldTime >= MaxHoldTime)
                {
                if(player.IsMovingControllerRightJoystick)
                {
                    player.SetVelocity(Recoil(MinRecoilAmount, player.MousePosInCameraSpace, player.transform.position, 90));
                }
                else
                {
                    player.SetVelocity(Recoil(MaxRecoilAmount, GameCamera.ScreenToWorldPoint(player.MousePosInCameraSpace), player.transform.position, 90));
                }

                    ShootBullet(bullet, transform.position, transform.eulerAngles.z);
                    ActualShotAmount += 1;
                }
                else
                {
                    if(player.IsMovingControllerRightJoystick)
                    {
                        player.SetVelocity(Recoil(MinRecoilAmount, player.MousePosInCameraSpace, player.transform.position, 90));
                    }
                    else
                    {
                        player.SetVelocity(Recoil((ActualHoldTime / (MaxHoldTime)) * MaxRecoilAmount, GameCamera.ScreenToWorldPoint(player.MousePosInCameraSpace), player.transform.position, 90));
                    }
                    ShootBullet(bullet, transform.position, transform.eulerAngles.z);
                    ActualShotAmount += 1;
                }
            }
            }
            ActualHoldTime = 0;
        }
    }
}
