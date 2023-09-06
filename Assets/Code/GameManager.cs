using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float HighScoreTime;
    public float AddToHighScore;
    public float HighScore;
    public bool loose = false;
    Player player;
    public Image Image;
    float LastPlayerhealth;
    float time = 0;
    public AnimationCurve HealthDecreaseCurve;
    public ShakeCamera CameraShake;
    public float CameraShakeTime;
    public float CameraShakeAmount;
    public SoundEffectsManager SoundManager;

    // Start is called before the first frame update
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        player.PlayerHealth = FindAnyObjectByType<WeaponSelectionManager>().PlayerChosenHealth;
        LastPlayerhealth = player.PlayerHealth;
        Image.fillAmount = (float)player.PlayerHealth / (float)player.MaxHealth;
        Destroy(FindAnyObjectByType<WeaponSelectionManager>().gameObject);
        DontDestroyOnLoad(this);
        StartCoroutine(AddOnHighScore());
        SoundManager = FindAnyObjectByType<SoundEffectsManager>();
    }
    private void Update()
    {
        // If the player has lost and the scene hasn't yet changed
        if(loose && SceneManager.GetActiveScene().name != "LooseScene")
        {
            SoundManager.StartDeathSound();
            SceneManager.LoadScene("LooseScene");
        }
        if(Image != null)
        {
            if (LastPlayerhealth != player.PlayerHealth)
            {
                Image.fillAmount = map(HealthDecreaseCurve.Evaluate(time), 0, 1, LastPlayerhealth / (float)player.MaxHealth, (float)player.PlayerHealth / (float)player.MaxHealth);
                time += Time.deltaTime;
            }
            if (time >= 1)
            {
                time = 0;
                LastPlayerhealth = player.PlayerHealth;
            }
        }
        if(player.HasShot)
        {
            if(CameraShake != null)
            {
                StartCoroutine(CameraShake.shake(CameraShakeTime, CameraShakeAmount));
            }
        }
    }
    float map(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
    IEnumerator AddOnHighScore()
    {
        while (!loose)
        {
            yield return new WaitForSeconds(HighScoreTime);
            HighScore += AddToHighScore;
        }
    }
}
