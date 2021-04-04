using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Seeker seeker;
    protected Path path;
    protected int CurrentPoint;
    public Transform TargetPosition;
    public float AttackRange;


    public float Speed = 6f;
    public float RandomRadius = 3f;
    //´ý»ú×´Ì¬

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
    }
    public void DaiJi()
    {
        anim.SetBool("isopen", false);
    }
    //Ñ²Âß×´Ì¬
    public void XunLuo()
    {
        anim.SetBool("isopen", true);
        var point = Random.insideUnitSphere * RandomRadius;
        point += transform.position;
        seeker.StartPath(transform.position, point, OnPathComplete);

    }
    //»Øµ÷º¯Êý
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            CurrentPoint = 0;
        }
    }
    //Ç°ÍùÑ²Âßµã
    public void MoveAI()
    {
        if (path == null)
        {
            return;
        }
        while (true)
        {

            if (CurrentPoint + 1 < path.vectorPath.Count)
            {
                CurrentPoint += 1;
            }
            else
            {
                break;
            }
        }
        Vector3 dir = (path.vectorPath[CurrentPoint] - transform.position).normalized;
        Vector3 volcity = dir * Speed;
        transform.position += volcity * Time.deltaTime;
    }

    //×·Öð×´Ì¬
    //·¢¶¯¹¥»÷
    /*public void Attack()
    {
        if (Vector3.Distance(transform.position, TargetPosition.position)<AttackRange)
        {
            
        }

        
    }*/
    //ËÀÍö×´Ì¬
    public void Death()
    {
        anim.SetBool("death", true);
        Destroy(gameObject);
    }
}
