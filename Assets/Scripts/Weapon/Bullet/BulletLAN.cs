using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLAN : Bullet
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != role)
        {
            if (collision.GetComponent<BeAttack>() != null && collision.gameObject.tag != "Enemy")
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
}
