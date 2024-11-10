using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MapTraverser
{

    PlayerMovement player;

    CorridorPoint targetPoint;




    [SerializeField]
    float stoppingDistance = 0.0001f;


    [SerializeField]
    float killDistance = 0.1f;

    [SerializeField]
    Animator animator;

    [Header("Monster scaling")]
    [SerializeField]
    Transform difficultyScaledObject;
    [SerializeField] float scalingAmount;


    private void Start()
    {
        difficultyScaledObject.transform.localScale += difficultyScaledObject.transform.localScale * scalingAmount * (GameHardness.level - 1); 
    }
    internal void StartChasing(CorridorPoint start, PlayerMovement target)
    {
        if (!target.IsAlive) return;
        SetSpeed();
        this.enabled = true;
        player = target;

        //transform.position = start.transform.position;

        //if (player.corridors.Count == 0)
        //{
        //    targetPoint = player.targetPoint;
        //    if(player.targetPoint == null)
        //    {
        //        player.Die();
        //    }
        //}
        //else
        //{
        //    targetPoint = player.corridors.Dequeue();
        //}
        player.OnPlayerDie.AddListener(() => this.enabled = false);
        player.OnPlayerWin.AddListener(PlayerWin);
        targetPoint = start;
    }

    // Update is called once per frame
    void Update()
    {

       

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetPoint.gridPosition, movementSpeed * Time.deltaTime);

        float DistanceCorridor = (transform.position - nextPosition).magnitude;
        float distanceToPlayer = (transform.position - player.transform.position).magnitude;
        transform.position = nextPosition;
        if (distanceToPlayer < killDistance)
        {
            player.Die();
            
            animator.SetTrigger("Eat");
            this.enabled = false;
            return;
        }

       

        if (DistanceCorridor <= stoppingDistance)
        {

            if (player.corridors.Count == 0)
            {
                targetPoint = player.targetPoint;
                //if(targetPoint == null)
                //{
                //    targetPoint = player.currentCorridor;
                //}
            }
            else
            {
                targetPoint = player.corridors.Dequeue();
            }
            transform.LookAt(targetPoint.gridPosition);
        }

        
    }
    void PlayerWin()
    {
        this.enabled = false;
        animator.SetTrigger("Anger");
    }
}
