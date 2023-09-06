using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Help
public class SoundEffectsManager : MonoBehaviour
{
    public AudioClip ButtonSelection;
    public AudioClip DeathSound;
    public AudioClip EnemyShoot;
    public AudioClip GetHit;
    public AudioClip StartGame;
    public AudioClip WeaponShoot;
    public AudioClip WeaponSelection;
    public AudioClip Jump;
    public AudioClip BulletHit;
    public AudioClip EnemyHit;

    public AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void StartButtonSelection()
    {
        audioSource.PlayOneShot(ButtonSelection);
    }
    public void StartDeathSound()
    {
        audioSource.PlayOneShot(DeathSound);
    }
    public void StartEnemyShoot()
    {
        audioSource.PlayOneShot(EnemyShoot);
    }
    public void StartGetHit()
    {
        audioSource.PlayOneShot(GetHit);
    }
    public void StartStartGame()
    {
        audioSource.PlayOneShot(StartGame);
    }
    public void StartWeaponShoot()
    {
        audioSource.PlayOneShot(WeaponShoot);
    }
    public void StartWeaponSelection()
    {
        audioSource.PlayOneShot(WeaponSelection);
    }
    public void StartJump()
    {
        audioSource.PlayOneShot(Jump);
    }
    public void StartBulletHit()
    {
        audioSource.PlayOneShot(BulletHit);
    }
    public void StartEnemyHit()
    {
        audioSource.PlayOneShot(EnemyHit);
    }
}
