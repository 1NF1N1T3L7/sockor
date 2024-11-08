using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorPoint : MonoBehaviour
{
    public CorridorPoint[] connectedPoints;

    public Vector3Int gridPosition;


    public virtual void OnPlayerArrive(PlayerMovement player)
    {

        print(gridPosition);

    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        var map = gameObject.GetComponentInParent<Map>();
        if (map == null)
        {
            Debug.LogWarning($"Sockor warning: map needs to be child of Map");
            return;
        }
        gridPosition = map.grid.WorldToCell(transform.position);
        transform.position = gridPosition;

        if (connectedPoints == null) return;

    }

    private void OnDrawGizmosSelected()
    {
        if (connectedPoints == null) return;
        foreach (var point in connectedPoints)
        {
            var posPoint = transform.position;
            var posEnd = point.transform.position;
            if(posPoint.x == posEnd.x || posPoint.z == posEnd.z)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.red;
            }
           
            Gizmos.DrawLine(posPoint, posEnd);
        }
    }

#endif
}
