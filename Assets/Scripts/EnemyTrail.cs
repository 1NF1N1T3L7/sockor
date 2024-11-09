using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrail : MonoBehaviour
{
    [SerializeField] LineRenderer LineRenderer;

    MapTraverser traverser;

    void Awake()
    {
        LineRenderer.enabled = false;
        traverser = GetComponent<MapTraverser>();
        traverser.OnArriveAtCorridor.AddListener(AddPoint);
        traverser.OnAllowMovement.AddListener(SetTrailStart);

    }

    private void SetTrailStart()
    {
        LineRenderer.SetPosition(0, transform.position);
        LineRenderer.enabled = true;
    }

    private void AddPoint(CorridorPoint point)
    {
        Vector3[] positions = new Vector3[LineRenderer.positionCount];
        LineRenderer.GetPositions(positions);
        LineRenderer.positionCount++;
        Vector3[] newPositions = new Vector3[LineRenderer.positionCount];


        for (int i = 0; i < positions.Length; i++)
        {
            newPositions[i] = positions[i];
        }
        newPositions[LineRenderer.positionCount - 1] = transform.position;

        LineRenderer.SetPositions(positions);
    }

    public void Update()
    {
        LineRenderer.SetPosition(LineRenderer.positionCount - 1, transform.position);
    }
}
