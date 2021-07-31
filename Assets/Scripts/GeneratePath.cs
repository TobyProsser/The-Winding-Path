using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

[RequireComponent(typeof(PathCreator))]
public class GeneratePath : MonoBehaviour
{
    public GameObject Player;
    private List<Transform> Points = new List<Transform>();
    public GameObject PointObject;
    private GameObject CurPathGroup;
    private Vector3 NextPos = Vector3.zero;

    public float MaxChange = 1;

    private RoadMeshCreator RMC;

    private float Xval = 10;
    private float Zval = 0;

    private void Awake()
    {
        RMC = this.GetComponent<RoadMeshCreator>();
    }
    void Start()
    {
        StartRoad();
        //StartCoroutine(CreateNewRoad());
    }

    private void StartRoad()
    {
        GameObject PathGroup = new GameObject();
        PathGroup.tag = "PathGroup";

        for (int i = 0; i < 2; i++)
        {
            GameObject NewPoint = Instantiate(PointObject, NextPos, Quaternion.identity);

            if (Points.Count % 2 == 0) //Every other point is set on opposite side of X axis
            {
                Zval = Random.Range(0, 1);
                NextPos = new Vector3(Xval, 0, Zval);
            }
            else
            {
                NextPos = new Vector3(Xval, 0, -Zval);
            }

            Xval += 10;
            Points.Add(NewPoint.transform);
            NewPoint.transform.parent = PathGroup.transform;
        }

        BezierPath bezierPath = new BezierPath(Points, false, PathSpace.xyz);
        GetComponent<PathCreator>().bezierPath = bezierPath;

        RMC.UpdateRoad();

        PathGroup = new GameObject();
        CurPathGroup = PathGroup;
        PathGroup.tag = "PathGroup";
        for (int i1 = 0; i1 < 10; i1++)
        {
            GameObject NewPoint = Instantiate(PointObject, NextPos, Quaternion.identity);

            if (Points.Count % 2 == 0) //Every other point is set on opposite side of X axis
            {
                Zval = Random.Range(0, MaxChange);
                NextPos = new Vector3(Xval, 0, Zval);
            }
            else
            {
                NextPos = new Vector3(Xval, 0, -Zval);
            }

            Xval += 10;
            Points.Add(NewPoint.transform);
            NewPoint.transform.parent = PathGroup.transform;
        }

        bezierPath = new BezierPath(Points, false, PathSpace.xyz);
        GetComponent<PathCreator>().bezierPath = bezierPath;

        RMC.UpdateRoad();
    }

    private void LateUpdate()
    {
        if (CurPathGroup != null && Player != null)
        {
            if (Player.transform.position.x > CurPathGroup.transform.GetChild(3).transform.position.x)
            {
                SpawnPath();
                //destroySections();
            }
        }
    }

    private void SpawnPath()
    {
        GameObject PathGroup = new GameObject();
        CurPathGroup = PathGroup;
        PathGroup.tag = "PathGroup";
        for (int i1 = 0; i1 < 10; i1++)
        {
            GameObject NewPoint = Instantiate(PointObject, NextPos, Quaternion.identity);

            if (Points.Count % 2 == 0) //Every other point is set on opposite side of X axis
            {
                Zval = Random.Range(0, MaxChange);
                NextPos = new Vector3(Xval, 0, Zval);
            }
            else
            {
                NextPos = new Vector3(Xval, 0, -Zval);
            }

            Xval += 10;
            Points.Add(NewPoint.transform);
            NewPoint.transform.parent = PathGroup.transform;
        }

        BezierPath bezierPath = new BezierPath(Points, false, PathSpace.xyz);
        GetComponent<PathCreator>().bezierPath = bezierPath;

        RMC.UpdateRoad();
    }

    private void destroySections()
    {
        List<GameObject> Sections = new List<GameObject>();
        Sections.AddRange(GameObject.FindGameObjectsWithTag("PathGroup"));

        for (int i = 0; i < Sections.Count; i++)
        {
            if (Sections[i].transform.position.x < Player.transform.position.x - 210)
            {
                Destroy(Sections[i]);
            }
        }
    }
}
