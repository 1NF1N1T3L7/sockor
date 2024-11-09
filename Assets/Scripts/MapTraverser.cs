using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapTraverser : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 10;

    public float speedScaling = 1;

    protected float movementSpeed = 1;

    public UnityEvent<CorridorPoint> OnArriveAtCorridor = new();
    public UnityEvent OnAllowMovement = new();

    protected void SetSpeed()
    {
        movementSpeed = speed + (speed * speedScaling * (GameHardness.level - 1));
    }
}
