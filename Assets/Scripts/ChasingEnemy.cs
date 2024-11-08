using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{

    PlayerMovement player;

    CorridorPoint targetPoint;

    public float speed = 10;

    [SerializeField]
    float stoppingDistance = 0.0001f;

    [SerializeField]
    float killDistance = 0.1f;
    internal void StartChasing(CorridorPoint start, PlayerMovement target)
    {
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
        targetPoint = start;
    }

    // Update is called once per frame
    void Update()
    {



        Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetPoint.gridPosition, speed * Time.deltaTime);

        float sqrDistanceCorridor = (transform.position - nextPosition).sqrMagnitude;
        float distanceToPlayer = (transform.position - player.transform.position).magnitude;
        transform.position = nextPosition;
        if (distanceToPlayer < killDistance)
        {
            player.Die();
            return;
        }

       

        if (sqrDistanceCorridor <= stoppingDistance)
        {

            if (player.corridors.Count == 0)
            {
                targetPoint = player.targetPoint;
            }
            else
            {
                targetPoint = player.corridors.Dequeue();
            }

        }

        
    }
}
