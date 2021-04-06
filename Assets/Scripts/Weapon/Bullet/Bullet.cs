using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected string role;
    protected float attack;


    void Update()
    {
        
    }
    //造成伤害
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != role)
        {
            if (collision.GetComponent<BeAttack>() != null && collision.gameObject.tag != "Player")
            {
                collision.GetComponent<BeAttack>().BeAttack(attack);
                Die();
            }
            else if (collision.gameObject.tag == "Background")
            {
                Die();
            }
            
        }
    }
    //初始化数据
    public void Initialization(float attack, string role, float bulletForce)
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
        this.attack = attack;
        this.role = role;
    }
    //销毁
    public void Die ()
    {
        Destroy(gameObject);
    }
}
