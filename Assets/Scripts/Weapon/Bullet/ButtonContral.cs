using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonContral : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Bullet;
    


    void Update()
    {
        //�����ӵ����
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bullet, Gun.transform.position, Quaternion.identity);
        }
    }
    


}
