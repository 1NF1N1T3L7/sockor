using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
   [SerializeField] LineRenderer LineRenderer;

    PlayerMovement playerMovement;

    public float heightOffset = 0.1f;

    public float minDistanceFromCorner = 0.1f;

    void Awake()
    {
        LineRenderer.enabled = false;
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.OnArriveAtCorridor.AddListener(AddPoint);
        playerMovement.OnPlayerStart.AddListener(SetTrailStart);
      
    }

    private void SetTrailStart()
    {
        LineRenderer.SetPosition(0, transform.position);
        LineRenderer.SetPosition(1, transform.position);
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
        newPositions[LineRenderer.positionCount - 1] = transform.position + Vector3.up * heightOffset;

        LineRenderer.SetPositions(newPositions);
    }

    public void LateUpdate()
    {
        Vector3 newPos = transform.position + Vector3.up * heightOffset;
        float distToLastSpot = (LineRenderer.GetPosition(LineRenderer.positionCount - 2) - newPos).magnitude;
        if(distToLastSpot < minDistanceFromCorner)
        {
            return;
        }
        LineRenderer.SetPosition(LineRenderer.positionCount-1, newPos);
    }



}
