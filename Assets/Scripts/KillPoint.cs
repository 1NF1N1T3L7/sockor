using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPoint : CorridorPoint
{
    public override void OnPlayerArrive(PlayerMovement player)
    {
        player.Die();
    }
}
