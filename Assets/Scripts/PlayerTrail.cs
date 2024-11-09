using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
   [SerializeField] LineRenderer LineRenderer;

    PlayerMovement playerMovement;

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
        LineRenderer.SetPosition(LineRenderer.positionCount-1, transform.position);
    }



}
