using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoSingleton<SceneChanger> 
{
    public static int currentScene;

    public bool isOnMenu = true;

    public void NextLevel()
    {
        isOnMenu = false;
        currentScene++;

        if (currentScene >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(1);
            currentScene = 1;
            GameHardness.level++;
        }
        else
        {
            SceneManager.LoadScene(currentScene);
        }

    }

    public void MainMenu()
    {
        if (isOnMenu)
        {
            Application.Quit();
            return;
        }
        isOnMenu = true;
        SceneManager.LoadScene(0);
        GameHardness.level = 1;

    }

    public void SelectLevel(int level)
    {
        isOnMenu = false;
        currentScene = level;
        GameHardness.level = 1;
        SceneManager.LoadScene(level);


    }
}
