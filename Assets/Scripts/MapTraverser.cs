using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapTraverser : MonoBehaviour
{
    public UnityEvent<CorridorPoint> OnArriveAtCorridor = new();
    public UnityEvent OnAllowMovement = new();
}
