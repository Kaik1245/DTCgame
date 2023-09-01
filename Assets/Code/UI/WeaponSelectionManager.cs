using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponSelectionManager : MonoBehaviour
{
    public GunType[] GunTypes;
    public GunType ChosenGun;
    public bool Explodes = false;
    public Player player;
    public int RifleGunHealthDecreaseAmount;
    public int ShotgunHealthDecreaseAmount;
    public int LaserGunHealthDecreaseAmount;
    public Image HealthSlider;
    public int PlayerChosenHealth;


    private void Awake()
    {
        Explodes = false;
        DontDestroyOnLoad(this.gameObject);
    }
    public void SelectShotGun()
    {
        if(ChosenGun == null)
        {
            foreach (var gun in GunTypes)
            {
                if (gun is Shotgun)
                {
                    ChosenGun = gun;
                    float DecreasedHealth = player.MaxHealth - ShotgunHealthDecreaseAmount;
                    HealthSlider.fillAmount = DecreasedHealth  / player.MaxHealth;
                    PlayerChosenHealth = (int)DecreasedHealth;
                }
            }
        }
    }
    public void SelectRifle()
    {
        if (ChosenGun == null)
        {
            foreach (var gun in GunTypes)
            {
                if (gun is Riflegun)
                {
                    ChosenGun = gun;
                    float DecreasedHealth = player.MaxHealth - RifleGunHealthDecreaseAmount;
                    HealthSlider.fillAmount = DecreasedHealth / player.MaxHealth;
                    PlayerChosenHealth = (int)DecreasedHealth;
                }
            }
        }
    }
    public void SelectLaserGun()
    {
        if (ChosenGun == null)
        {
            foreach (var gun in GunTypes)
            {
                if (gun is LaserGun)
                {
                    ChosenGun = gun;
                    float DecreasedHealth = player.MaxHealth - LaserGunHealthDecreaseAmount;
                    HealthSlider.fillAmount = DecreasedHealth / player.MaxHealth;
                    PlayerChosenHealth = (int)DecreasedHealth;
                }
            }
        }
    }
    public void SelectExplosion()
    {
        foreach (var gun in GunTypes)
        {
            if (gun is LaserGun)
            {
                Explodes = false;
            }
            else
            {
                Explodes = true;
            }
        }
    }
    public void StartGame()
    {
        if(ChosenGun == null)
        {
            foreach (var gun in GunTypes)
            {
                if (gun is NormalGun)
                {
                    ChosenGun = gun;
                }
            }
        }
        SceneManager.LoadScene("GameScene");
    }
}
