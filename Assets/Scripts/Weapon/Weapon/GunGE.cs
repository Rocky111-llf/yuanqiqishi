using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGE : Weapon
{
    public GameObject BulletPrefab;

    public override void shoot()
    {
        base.shoot();
        GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation * Quaternion.AngleAxis(Random.Range(0, shake), Vector3.forward));
        bullet.GetComponent<Bullet>().Initialization(attack, role, BulletForce);
    }


}
