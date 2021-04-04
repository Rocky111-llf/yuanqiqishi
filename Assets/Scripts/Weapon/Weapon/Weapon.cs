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

    public virtual void shoot()
    {

    }
    public virtual void UpdateLookAt(Vector3 target)
    {

    }
    public virtual void Initialization(string role,int layer)
    {
        this.role = role;
    }
    public void PickUp()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void PickDown()
    {
        GetComponent<Collider2D>().enabled = true;
    }

}
