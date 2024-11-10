using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : CorridorPoint
{
    public override void OnPlayerArrive(PlayerMovement player)
    {
        OnPlayerArriveEvent.Invoke();
        GameManager.Instance.audioEffects.WindSFX();
      SceneChanger.Instance.NextLevel();
      
    }
}
