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
    protected Room room;
    protected string role = "Enemy";


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    //�˺�����
    public virtual void BeAttack(float Value)
    {
        HP -= Value;
        if (HP <= 0)
        {
            enemestate = EnemyState.Die;
            GetComponent<Animator>().SetBool("die", true);
            GetComponent<Collider2D>().enabled = false;
            room.EnemyDie(this);
        }
        else
        {
            GetComponent<Animator>().Play("BeAttack");
        }
    }
    //���ٺ���
    private void Die()
    {
        Destroy(gameObject);
    }
    //��ʼ��
    public void Initialzation(Room room)
    {
        this.room = room;
        TargetPoingt = GameObject.FindGameObjectWithTag("Player").transform;
    }


}
