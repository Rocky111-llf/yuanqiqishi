using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarAI : MonoBehaviour
{
    public Transform TargetPosition;
    public float NextPointDistance;
    public float speed;
    public float randomRadius;



    private Seeker seeker;
    private Path path;
    private int CurrentPoint = 0;
    private Vector3 TargetLastPosition;
    private bool isEndTarget;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, TargetPosition.position, OnPathComplete);
        TargetLastPosition = TargetPosition.position;
    }


    public void Update()
    {
        MoveAI();
        UpdatePath();

    }
    //回调函数
    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            CurrentPoint = 0;
        }
    }
    //判断是否需要更改路径
    public void UpdatePath()
    {
        if (Vector3.Distance(TargetPosition.position,TargetLastPosition ) > 2)
        {
            TargetLastPosition = TargetPosition.position;
            seeker.StartPath(transform.position, TargetPosition.position, OnPathComplete);
        }
    }
   

    public void MoveAI()
    {
        if(path == null)
        {
            return;
        }
        isEndTarget = false;
        float DistanceToNextPoint;
        while (true)
        {
            DistanceToNextPoint = Vector3.Distance(transform.position, TargetPosition.position);
            if (DistanceToNextPoint < NextPointDistance)
            {
                if (CurrentPoint + 1 < path.vectorPath.Count)
                {
                    CurrentPoint += 1;
                }
                else
                {
                    isEndTarget = true;
                    break;
                }
            }
            else
            {

                break;
            }


        }
        var SpeedFactor = isEndTarget ? Mathf.Sqrt(DistanceToNextPoint / NextPointDistance) : 1f;
        Vector3 dir = (path.vectorPath[CurrentPoint] - transform.position).normalized;
        Vector3 volcity = dir * SpeedFactor * speed;
        transform.position += volcity * Time.deltaTime;


    }





}
