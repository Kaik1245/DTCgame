using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LooseManager : MonoBehaviour
{
    private GameManager Manager;
    public TMP_Text HighSchoreText;

    private void Awake()
    {
        Manager = FindAnyObjectByType<GameManager>();
        HighSchoreText.text = "High score: " + Manager.HighScore;
    }
    public void PlayGame()
    {
        Manager.loose = false;
        Destroy(Manager.gameObject);
        SceneManager.LoadScene("WeaponSelection");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
