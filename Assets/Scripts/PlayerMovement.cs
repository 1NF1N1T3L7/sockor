using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MapTraverser
{

    [SerializeField] InputAction up;
    [SerializeField] InputAction down;
    [SerializeField] InputAction left;
    [SerializeField] InputAction right;

    public UnityEvent OnPlayerDie;

    public float speed = 1;

    [SerializeField]
    float stoppingDistance = 0.01f;

    private Vector2Int Direction;

    private CorridorPoint currentCorridor;
    public CorridorPoint targetPoint { private set; get; }

    public Queue<CorridorPoint> corridors = new();

    public UnityEvent OnPlayerStart = new();

    public void ReadyPlayer(CorridorPoint point)
    {
        currentCorridor = point;
        this.enabled = true;
        OnPlayerStart.Invoke();
    }

    private void OnEnable()
    {
        up.performed += TryMoveUp;
  
        down.performed += TryMoveDown;

        left.performed += TryMoveLeft;
        right.performed += TryMoveRight;

        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();
    }


    private void TryMoveUp(InputAction.CallbackContext context)
    {
        int x = currentCorridor.gridPosition.x;
        int z = currentCorridor.gridPosition.z;
        foreach (var target in currentCorridor.connectedPoints)
        {
            if(target.gridPosition.x == x && target.gridPosition.z > z)
            {
                targetPoint = target;
                return;
            }
        }

    }
    private void TryMoveDown(InputAction.CallbackContext context)
    {
        int x = currentCorridor.gridPosition.x;
        int z = currentCorridor.gridPosition.z;
        foreach (var target in currentCorridor.connectedPoints)
        {
            if (target.gridPosition.x == x && target.gridPosition.z < z)
            {
                targetPoint = target;
                return;
            }
        }

    }
    private void TryMoveLeft(InputAction.CallbackContext context)
    {
        int x = currentCorridor.gridPosition.x;
        int z = currentCorridor.gridPosition.z;
        foreach (var target in currentCorridor.connectedPoints)
        {
            if (target.gridPosition.z == z && target.gridPosition.x < x)
            {
                targetPoint = target;
                return;
            }
        }
    }
    private void TryMoveRight(InputAction.CallbackContext context)
    {
        int x = currentCorridor.gridPosition.x;
        int z = currentCorridor.gridPosition.z;
        foreach (var target in currentCorridor.connectedPoints)
        {
            if (target.gridPosition.z == z && target.gridPosition.x > x)
            {
                targetPoint = target;
                return;
            }
        }
    }

    void Update()
    {
        if (targetPoint == null) return;
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetPoint.gridPosition, speed * Time.deltaTime);
        float sqrDistance = (transform.position - nextPosition).sqrMagnitude;
        transform.position = nextPosition;
        if(sqrDistance <= stoppingDistance)
        {
            currentCorridor = targetPoint;
            corridors.Enqueue(currentCorridor);
            currentCorridor.OnPlayerArrive(this);
            OnArriveAtCorridor.Invoke(currentCorridor); 
            targetPoint = null;
        }
    
    }


    public void Die()
    {
        OnPlayerDie.Invoke();
        SceneManager.LoadScene(0);

    }





}
