using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BttonPhysical : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed; 


    // Start is called before the first frame update
    void Start()
    {
        //���Ʒ���ǶȺ��ٶ�
        transform.eulerAngles = new Vector3(0, 0, GunContral.WeaponAngle);
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    //�ӵ�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Background")
        {
            Destroy(gameObject);
        }
    }

}
