using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeBULin : Enemy
{
    //��������
    public float AttackRange;
    public float AttackCD;
    private float AttackTiming;
    public float attack;
    //׷������
    public float TrackRange;
    public float TrackCD;
    private float TrackTiming;
    //Ѳ������
    public float StrollCD;
    private float StrollTiming;
    //����
    public GameObject MyWeapon;
    private Weapon weapon;
    //�����Ұ
    private bool See;
    private LayerMask layermask;
    private RaycastHit2D raycasthit;

    private AstarAI MyAI;
    private Animator anim;
    

    void Start()
    {
        See = false;
        enemestate = EnemyState.Idle;
        MyWeapon = Instantiate(MyWeapon, transform.position,Quaternion.identity);
        weapon = MyWeapon.GetComponent<Weapon>();
        MyWeapon.transform.SetParent(transform);
        MyWeapon.transform.localPosition = new Vector3(0, -0.5f, 0);
        MyAI = GetComponent<AstarAI>();
        anim = GetComponent<Animator>();
        enemestate = EnemyState.Idle;
        layermask = ~LayerMask.GetMask("Enemy") & ~LayerMask.GetMask("Weapon") & ~LayerMask.GetMask("Room");
    }

    
    void Update()
    {
        switch (enemestate)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Tracking:
                Tracking();
                break;
            case EnemyState.Stroll:
                Stroll();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        if (isopen)
        {
            enemestate = EnemyState.Stroll;
            anim.SetBool("run", true);
        }
    }

    private void Die()
    {

    }

    //�����Ұ
    public void RaycastHit()
    {
        raycasthit = Physics2D.Raycast(transform.position + Vector3.up, (TargetPoingt.position - (transform.position + Vector3.up)).normalized, TrackRange, layermask);
        if (raycasthit.transform != null && raycasthit.transform == TargetPoingt)
        {
            Debug.Log("success");
            See = true;

        }
        else
        {
            See = false;
        }
    }
    //Ѳ��
    private void Stroll()
    {
        
        RaycastHit();
        if (See)
        {
            //Debug.Log("success");
            enemestate = EnemyState.Tracking;
        }
        else
        {
            if (Time.time - StrollTiming > StrollCD)
            {
                StrollTiming = Time.time;
                MyAI.RandomPath();
            }
            MyAI.MoveAI();
        }
    }

    //׷��
    private void Tracking()
    {
        if (Time.time - TrackTiming > TrackCD)
        {
            //Debug.Log("success");
            TrackTiming = Time.time;
            MyAI.UpdatePath(TargetPoingt.position);
        }
        MyAI.MoveAI();
        if (Vector3.Distance(transform.position, TargetPoingt.position) < AttackRange)
        {
            enemestate = EnemyState.Attack;
            anim.SetBool("run", false);
        }
        RaycastHit();
        if (!See)
        {
            enemestate = EnemyState.Stroll;
        }
    }

    //����
    private void Attack()
    {
        if (Vector3.Distance(transform.position, TargetPoingt.position) < AttackRange)
        {
            enemestate = EnemyState.Tracking;
            anim.SetBool("run", true);
        }
        if (Time.time - AttackTiming > AttackCD)
        {
            AttackTiming = Time.time;
            weapon.shoot();
        }
    }
    //����
    public override void BeAttack(float Value)
    {
        base.BeAttack(Value);
        weapon.gameObject.SetActive(false);
    }
}
