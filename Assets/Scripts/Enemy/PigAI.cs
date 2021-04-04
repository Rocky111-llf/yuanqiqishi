using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAI : MonoBehaviour
{
    private Animator anim;
    private Seeker seeker;
    private Path path;
    private int CurrentPoint;
    public float speed;
    public float RandomRadius;
    public Transform TargetPosition;
    private bool oncetime = true;
    private Vector3 point;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        anim = GetComponent<Animator>();
        anim.SetBool("isopen", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isopen"))
        {        
            MoveAI();
            UpdateStart();
        }

    }
    //�ж��Ƿ����Ѳ��״̬
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isopen", true);
        }
    }
    //����Ѳ��·��
    public void UpdatePath()
    {      
            point = Random.insideUnitSphere * RandomRadius;
            point += transform.position;
            seeker.StartPath(transform.position, point, OnPathComplete);
    }
    //ִ��Ѳ��·��
    public void UpdateStart()
    {
        if (oncetime)
        {
            InvokeRepeating("UpdatePath", 0f, 1f);
            oncetime = false;
        }
        
    }
    //�ص�����
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Debug.Log("success");
            path = p;
            CurrentPoint = 0;
        }
    }
    //ǰ��Ѳ�ߵ�
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
        Vector3 volcity = dir * speed;
        transform.position += volcity * Time.deltaTime;
    }
    //�ж��Ƿ�׷������
    public void CatchTarget()
    {
        if (Vector3.Distance(TargetPosition.position, transform.position) < 5f)
        {
            
        }
    }
}
