using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoSingleton<SceneChanger> 
{
    public static int currentScene;



    public void NextLevel()
    {
        currentScene++;

        if (currentScene >= SceneManager.sceneCount)
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
        SceneManager.LoadScene(0);
        GameHardness.level = 1;

    }

    public void SelectLevel(int level)
    {

        SceneManager.LoadScene(level);


    }
}
