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
            MainMenu();
        }
        else
        {
            SceneManager.LoadScene(currentScene);
        }




    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);


    }

    public void SelectLevel(int level)
    {

        SceneManager.LoadScene(level);


    }
}
