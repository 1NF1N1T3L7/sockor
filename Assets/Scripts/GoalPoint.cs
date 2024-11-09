using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : CorridorPoint
{
    public override void OnPlayerArrive(PlayerMovement player)
    {
        OnPlayerArriveEvent.Invoke();
        SceneChanger.Instance.NextLevel();
    }
}
