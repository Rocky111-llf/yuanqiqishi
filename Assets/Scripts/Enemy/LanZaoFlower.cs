using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanZaoFlower : Enemy
{
    public Transform pos;
    private Animator anim;
    //private AstarAI MyAI;
    public GameObject BulletPrefab;

    public int BulletCount;
    public float AttackRange;
    public float CD = 5;
    private float timing;
    public float attack;
    public float BulletForce;

    void Start()
    {
        enemestate = EnemyState.Idle;
        timing = -CD;
        anim = GetComponent<Animator>();
        //MyAI = GetComponent<AstarAI>();

    }


    void Update()
    {
        switch (enemestate)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Traking:
                Traking();
                break;
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
        }
    }
    private void Die()
    {

    }
    private void Idle()
    {
        if (isopen)
        {
            enemestate = EnemyState.Traking;
        }
    }
    private void Traking()
    {
        if (Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < AttackRange && Time.time - timing > CD)
        {
            timing = Time.time;
            anim.SetBool("run", true);
            for (int i = 0; i < BulletCount; i++)
            {
                GameObject go = Instantiate(BulletPrefab, pos.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                go.GetComponent<Bullet>().Initialization(attack, role, BulletForce);
                go.transform.SetParent(GameControl.instance.WeaponRecycle);
            }
        }
    }
}
