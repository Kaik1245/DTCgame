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

    // Start is called before the first frame update
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        player.PlayerHealth = FindAnyObjectByType<WeaponSelectionManager>().PlayerChosenHealth;
        Destroy(FindAnyObjectByType<WeaponSelectionManager>().gameObject);
        DontDestroyOnLoad(this);
        StartCoroutine(AddOnHighScore());
    }
    private void Update()
    {
        // If the player has lost and the scene hasn't yet changed
        if(loose && SceneManager.GetActiveScene().name != "LooseScene")
        {
            SceneManager.LoadScene("LooseScene");
        }
        Image.fillAmount = (float)player.PlayerHealth / (float)player.MaxHealth;
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
