using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string role;
    public float shake;
    public float attack;
    public float BulletForce;
    public Vector3 target;
    //���
    public virtual void shoot()
    {

    }
    //��ʼ��
    public virtual void Initialization(string role,int layer)
    {
        this.role = role;
    }
    //��ǹ
    public void PickUp()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    //��ǹ
    public void PickDown()
    {
        GetComponent<Collider2D>().enabled = true;
    }

}
