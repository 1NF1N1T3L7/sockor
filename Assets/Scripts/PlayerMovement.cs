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

    [SerializeField] float deathDelay = 2f;

    [SerializeField]
    float stoppingDistance = 0.01f;

    private Vector2Int Direction;

    public CorridorPoint currentCorridor { private set; get; }
    public CorridorPoint targetPoint { private set; get; }

    public Queue<CorridorPoint> corridors = new();

    public UnityEvent OnPlayerStart = new();


    private delegate void LastInput();
    private LastInput lastInput;
    private float inputTime;

    [Header("Input Buffer")]

    [SerializeField] private float bufferTime = 0.1f;

    public void ReadyPlayer(CorridorPoint point)
    {
        currentCorridor = point;
        this.enabled = true;
        SetSpeed();
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
        lastInput = BufferedUp;
        inputTime = Time.time;
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
        lastInput = BufferedDown;
        inputTime = Time.time;
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
        lastInput = BufferedLeft;
        inputTime = Time.time;
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
        lastInput = BufferedRight;
        inputTime = Time.time;
    }
    //Lazy way
    private void BufferedUp()
    {
        int x = currentCorridor.gridPosition.x;
        int z = currentCorridor.gridPosition.z;
        foreach (var target in currentCorridor.connectedPoints)
        {
            if (target.gridPosition.x == x && target.gridPosition.z > z)
            {
                targetPoint = target;
                return;
            }
        }
    }
    private void BufferedDown()
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
    private void BufferedLeft()
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
    private void BufferedRight()
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
        if(Time.time < bufferTime + inputTime)
        {
            
            lastInput?.Invoke();
        }


        if (targetPoint == null) return;
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetPoint.gridPosition, movementSpeed * Time.deltaTime);
        float distance = (transform.position - nextPosition).magnitude;
        transform.position = nextPosition;
        if(distance <= stoppingDistance)
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
        this.enabled = false;
        OnPlayerDie.Invoke();
        StartCoroutine(DeathDelay());

    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(1);
    }





}
