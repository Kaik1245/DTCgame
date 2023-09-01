using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("WeaponSelection");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
