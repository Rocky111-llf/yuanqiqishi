using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject treasure;
    public Transform treasurePosition;
    public Animator anim1;
    public Animator anim2;
    private bool isopen = false;


    //判断宝箱是否需要打开
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isopen)
        {
            treasurePosition.position = new Vector3(treasurePosition.position.x, treasurePosition.position.y+0.5f, treasurePosition.position.z);
            anim1.SetBool("1", true);
            anim2.SetBool("2", true);
            Instantiate(treasure,treasurePosition.position, Quaternion.identity);
            isopen = true;
        }
    }

}
