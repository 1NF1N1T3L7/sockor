using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    [Header("Map things")]
    public Grid grid;

    public CorridorPoint playerStart;

    [Header("Moving things")]

    public PlayerMovement player;

    public ChasingEnemy enemy;

    public float enemyDelay = 2f;

    [Header("Prefabs for map generation")]

    public GameObject pointObject;

    public GameObject killObject;

    public GameObject goalPoint;

    public GameObject startPointPrefab;

    [SerializeField] GameObject lineRendererPrefab;

    [Header("Generated parents (OLD AUTO GENERATED GAMEOBJECST ARE DELETED ON GENERATE MAP)")]
    [SerializeField] Transform tileParent;
    [SerializeField] Transform visualPointsParent;

    [HideInInspector]
    public List<GameObject> lines = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> visualPoints = new List<GameObject>();

    bool firstPoint;

    private IEnumerator Start()
    {
        player.transform.position = playerStart.transform.position;
        player.ReadyPlayer(playerStart);
        GameManager.Instance.audioEffects.LoadMapSFX(this);
        yield return new WaitForSeconds(enemyDelay);
        enemy.StartChasing(playerStart, player);

    }
    public void CreateMap()
    {
        for (int i = lines.Count - 1; i > 0; i--)
        {
            DestroyImmediate(lines[i]);
        }
        for (int i = visualPoints.Count - 1; i > 0; i--)
        {
            DestroyImmediate(visualPoints[i]);
        }
        var points = gameObject.GetComponentsInChildren<CorridorPoint>();
        foreach (var point in points)
        {
            var cell = grid.WorldToCell(point.transform.position);
            var world = grid.CellToWorld(cell);
            point.transform.position = world;
        }
        firstPoint = true;
        foreach (var point in points)
        {
            GeneratePath(point);
        }

    }


    private void GeneratePath(CorridorPoint point)
    {
        if (firstPoint && startPointPrefab != null)
        {
            visualPoints.Add(Instantiate(startPointPrefab,
                              point.transform.position,
                              Quaternion.identity,
                              tileParent));
        }
        else
        {
            GameObject pointPrefab = PointToPrefab(point);
            if (pointPrefab != null)
            {
                visualPoints.Add(Instantiate(pointPrefab,
                              point.transform.position,
                              Quaternion.identity,
                              tileParent));
            }

        }



        foreach (var endPoint in point.connectedPoints)
        {
            var newObject = Instantiate(lineRendererPrefab, visualPointsParent);
            var lineRenderer = newObject.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { point.transform.position, endPoint.transform.position });
            lines.Add(newObject);


            //var endObject = endPoint is KillPoint ? killObject : pointObject;

            //float deltaX = (endPoint.transform.position.x - point.transform.position.x);
            //float deltaZ = (endPoint.transform.position.z - point.transform.position.z);
            //float absX = Mathf.Abs(deltaX);
            //float absZ = Mathf.Abs(deltaZ);


            //if (absX > absZ)
            //{
            //    int z = (int)point.transform.position.z;
            //    int start = deltaX > 0 ? (int)point.transform.position.x : (int)endPoint.transform.position.x;
            //    int end = deltaX > 0 ? (int)endPoint.transform.position.x : (int)point.transform.position.x;

            //    for (int i = start; i <= end; i++)
            //    {

            //        if (i == end)
            //        {
            //            lines.Add(Instantiate(endObject,
            //                new Vector3(i, point.transform.position.y, z),
            //                Quaternion.identity,
            //                tileParent));
            //        }
            //        else
            //        {
            //            lines.Add(Instantiate(pointObject,
            //                new Vector3(i, point.transform.position.y, z),
            //                Quaternion.identity,
            //                tileParent));
            //        }

            //    }
            //}
            //else
            //{
            //    int start = deltaZ > 0 ? (int)point.transform.position.z : (int)endPoint.transform.position.z;
            //    int end = deltaZ > 0 ? (int)endPoint.transform.position.z : (int)point.transform.position.z;

            //    for (int i = start; i <= end; i++)
            //    {
            //        if (i == end)
            //        {
            //            lines.Add(Instantiate(endObject,
            //               new Vector3(point.transform.position.x, point.transform.position.y, i),
            //               Quaternion.identity,
            //               tileParent));
            //        }
            //        else
            //        {
            //            lines.Add(Instantiate(pointObject,
            //              new Vector3(point.transform.position.x, point.transform.position.y, i),
            //              Quaternion.identity,
            //              tileParent));
            //        }

            //    }


            //}


        }

    }

    private GameObject PointToPrefab(CorridorPoint point)
    {
        if (point is KillPoint)
        {
            return killObject;

        }
        if (point is GoalPoint)
        {
            return goalPoint;
        }

        return pointObject;

    }

}
