using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public AudioEffects audioEffects;

    public int startDifficulty = 1;
    protected override  void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
    }


    public void StartGame()
    {
        GameHardness.level = startDifficulty;
        SceneChanger.Instance.SelectLevel(1);
        

    }

}
