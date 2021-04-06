using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private float HP = 4;

    public void BeAttack(float Value)
    {
        HP -= Value;
        if (HP <= 0)
        {
            foreach(Animator anim in GetComponentsInChildren<Animator>())
            {
                anim.Play("Destroy");
            }
            Destroy(gameObject);
        }
    }
}
