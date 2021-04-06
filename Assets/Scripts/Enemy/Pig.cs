using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{
    //—≤¬ﬂ¿‰»¥
    public float StrollCD;
    private float StrollTiming;

    public float AttackCD;
    public float speed;
    public float attack;

    private Animator anim;
    private AstarAI MyAI;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        MyAI = GetComponent<AstarAI>();
    }
    void Update()
    {
        switch (enemestate)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Stroll:
                Stroll();
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
            enemestate = EnemyState.Stroll;
            anim.SetBool("run", true);
        }
    }
    //—≤¬ﬂ
    private void Stroll()
    {
        if (Time.time - StrollTiming > StrollCD)
        {
            StrollTiming = Time.time;
            MyAI.RandomPath();    
        }
        MyAI.MoveAI();
    }
    //‘Ï≥……À∫¶
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<BeAttack>() != null && collision.gameObject.tag!="Enemy" )
        {
            collision.transform.GetComponent<BeAttack>().BeAttack(attack);
        }
    }

}
