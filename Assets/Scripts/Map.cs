using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public CorridorPoint playerStart;

    public Grid grid;

    public PlayerMovement player;

    public GameObject lineObject;

    [HideInInspector]
    public List<GameObject> lines = new List<GameObject>();



    private void Start()
    {
        player.transform.position = playerStart.transform.position;
        player.ReadyPlayer(playerStart);
    }
    public void CreateMap()
    {
        for (int i = lines.Count-1; i > 0; i--)
        {

            DestroyImmediate(lines[i]);
        }
        var points = gameObject.GetComponentsInChildren<CorridorPoint>();
        foreach (var point in points)
        {
            var cell = grid.WorldToCell(point.transform.position);
            var world = grid.CellToWorld(cell);
            point.transform.position = world;
        }
        foreach (var point in points)
        {
            GeneratePath(point);
        }

    }


    private void GeneratePath(CorridorPoint point)
    {

        foreach (var endPoint in point.connectedPoints)
        {

            float deltaX = (endPoint.transform.position.x - point.transform.position.x);
            float deltaZ = (endPoint.transform.position.z - point.transform.position.z);
            float absX = Mathf.Abs(deltaX);
            float absZ = Mathf.Abs(deltaZ);


            if (absX > absZ)
            {
                int z = (int)point.transform.position.z;
                int start = deltaX > 0 ? (int)point.transform.position.x : (int)endPoint.transform.position.x;
                int end = deltaX > 0 ? (int)endPoint.transform.position.x : (int)point.transform.position.x;

                for (int i = start; i <= end; i++)
                {
                    print(i);
                    lines.Add(Instantiate(lineObject,
                            new Vector3(i, point.transform.position.y, z),
                            Quaternion.identity,
                            transform));

           
                }
            }
            else
            {
                int start = deltaZ >  0 ? (int)point.transform.position.z : (int)endPoint.transform.position.z;
                int end = deltaZ > 0 ? (int)endPoint.transform.position.z : (int)point.transform.position.z;

                for (int i = start; i <= end; i++)
                {
                    lines.Add(Instantiate(lineObject,
                            new Vector3(point.transform.position.x, point.transform.position.y, i),
                            Quaternion.identity,
                            transform));
                }


            }


        }

    }
}
