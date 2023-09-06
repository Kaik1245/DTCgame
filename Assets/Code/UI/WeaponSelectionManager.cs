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
    public AnimationCurve healthDecreaseCurve;
    bool ChangeHealth;
    float MaxFillAmount = 1;
    float time = 0;
    float HealthDecreaseAmount = 0;
    bool ResetHealth = false;
    float LastFillAmount;
    public Sprite SelectedShotgun;
    public Sprite SelectedRifle;
    public Sprite SelectedLasergun;
    public Sprite NormalShotgun;
    public Sprite NormalRifle;
    public Sprite NormalLasergun;
    public Image ShotgunSprite;
    public Image RifleSprite;
    public Image LasergunSprite;
    private SoundEffectsManager SoundManager;

    private void Awake()
    {
        Explodes = false;
        DontDestroyOnLoad(this.gameObject);
        SoundManager = FindAnyObjectByType<SoundEffectsManager>();
    }
    public void ResetGun()
    {
        if(time <= 0)
        {
            SoundManager.StartButtonSelection();
            ShotgunSprite.sprite = NormalShotgun;
            RifleSprite.sprite = NormalRifle;
            LasergunSprite.sprite = NormalLasergun;
            ChosenGun = null;
            ResetHealth = true;
            ChangeHealth = false;
            HealthDecreaseAmount = 0;
            LastFillAmount = HealthSlider.fillAmount;
            PlayerChosenHealth = player.MaxHealth;
        }
    }
    public void SelectShotGun()
    {
        if(ChosenGun == null)
        {
            foreach (var gun in GunTypes)
            {
                if (gun is Shotgun && time == 0)
                {
                    SoundManager.StartWeaponSelection();
                    ChosenGun = gun;
                    float DecreasedHealth = player.MaxHealth - ShotgunHealthDecreaseAmount;
                    ChangeHealth = true;
                    HealthDecreaseAmount = DecreasedHealth / player.MaxHealth;
                    PlayerChosenHealth = (int)DecreasedHealth;
                    ShotgunSprite.sprite = SelectedShotgun;
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
                if (gun is Riflegun && time == 0)
                {
                    SoundManager.StartWeaponSelection();
                    ChosenGun = gun;
                    float DecreasedHealth = player.MaxHealth - RifleGunHealthDecreaseAmount;
                    ChangeHealth = true;
                    HealthDecreaseAmount = DecreasedHealth / player.MaxHealth;
                    PlayerChosenHealth = (int)DecreasedHealth;
                    RifleSprite.sprite = SelectedRifle;
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
                if (gun is LaserGun && time == 0)
                {
                    SoundManager.StartWeaponSelection();
                    ChosenGun = gun;
                    float DecreasedHealth = player.MaxHealth - LaserGunHealthDecreaseAmount;
                    ChangeHealth = true;
                    HealthDecreaseAmount = DecreasedHealth / player.MaxHealth;
                    PlayerChosenHealth = (int)DecreasedHealth;
                    LasergunSprite.sprite = SelectedLasergun;
                }
            }
        }
    }
    float map(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
    private void Update()
    {
        if(ChangeHealth && !ResetHealth)
        {
            HealthSlider.fillAmount = MaxFillAmount - map(healthDecreaseCurve.Evaluate(time), 0, 1, 0, HealthDecreaseAmount);
            time += Time.deltaTime;
        }
        if (ResetHealth)
        {
            HealthSlider.fillAmount = map(healthDecreaseCurve.Evaluate(time), 0, 1, LastFillAmount, MaxFillAmount);
            time += Time.deltaTime;
        }
        if (time >= 1)
        {
            ChangeHealth = false;
            HealthDecreaseAmount = 0;
            ResetHealth = false;
            time = 0;
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
        SoundManager.StartButtonSelection();
        if (ChosenGun == null)
        {
            foreach (var gun in GunTypes)
            {
                if (gun is NormalGun)
                {
                    PlayerChosenHealth = player.MaxHealth;
                    ChosenGun = gun;
                }
            }
        }
        SceneManager.LoadScene("GameScene");
    }
}
