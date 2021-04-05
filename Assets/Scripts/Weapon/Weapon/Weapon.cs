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
    //Éä»÷
    public virtual void shoot()
    {

    }
    //³õÊ¼»¯
    public virtual void Initialization(string role,int layer)
    {
        this.role = role;
    }
    //¼ñÇ¹
    public void PickUp()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    //¶ªÇ¹
    public void PickDown()
    {
        GetComponent<Collider2D>().enabled = true;
    }

}
