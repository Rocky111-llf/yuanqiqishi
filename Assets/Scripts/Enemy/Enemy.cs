using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    Idle,
    Traking,
    Stroll,
    Attack,
    Die
}

public class Enemy : MonoBehaviour,BeAttack
{
    public float HP;
    public bool isopen = false;
    protected Transform TargetPoingt;
    protected EnemyState enemestate;
    protected string role = "Enemy";


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    //伤害函数
    public virtual void BeAttack(float Value)
    {
        HP -= Value;
        if (HP <= 0)
        {
            enemestate = EnemyState.Die;
            GetComponent<Animator>().SetBool("die", true);
            GetComponent<Collider2D>().enabled = false;

        }
        else
        {
            GetComponent<Animator>().Play("BeAttack");
        }
    }
    //销毁函数
    private void Die()
    {
        Destroy(gameObject);
    }
    //初始化
    public void Initialzation()
    {
        Debug.Log("sheji");
        TargetPoingt = GameObject.FindGameObjectWithTag("Player").transform;
    }


}
