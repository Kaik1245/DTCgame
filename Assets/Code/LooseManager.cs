using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LooseManager : MonoBehaviour
{
    private GameManager Manager;
    public TMP_Text HighSchoreText;
    int ActualHighScore = 0;
    public AnimationCurve HighscoreIncreaseCurve;
    float time = 0;

    private void Awake()
    {
        Manager = FindAnyObjectByType<GameManager>();
    }
    public void PlayGame()
    {
        Manager.loose = false;
        Destroy(Manager.gameObject);
        SceneManager.LoadScene("WeaponSelection");
    }
    private void Update()
    {
        if(ActualHighScore != Manager.HighScore)
        {
            ActualHighScore = (int)map(HighscoreIncreaseCurve.Evaluate(time), 0, 1, 0, (float)Manager.HighScore);
            HighSchoreText.text = "High score: " + ActualHighScore;
            time += Time.deltaTime;
        }
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    float map(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
}
