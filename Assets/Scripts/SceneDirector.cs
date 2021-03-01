using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector  : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
}
