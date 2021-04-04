using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContral : Weapon
{
    public GameControl BulletPrefab;
    public Transform born;

    public float CD = 1;
    public float timing;

    public static float WeaponAngle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GunRorate();
    }
    
    //枪的旋转
    public void GunRorate()
    {
        //获取鼠标坐标
        Vector3 MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
        MousePosition.z = 0;

        //武器朝向角度
        WeaponAngle = Vector2.Angle(MousePosition - this.transform.position,Vector2.right);
        if (transform.position.y > MousePosition.y)
        {
            WeaponAngle = -WeaponAngle;
        }
        if ((MousePosition - transform.position).x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, -1f, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }
        transform.eulerAngles = new Vector3(0, 0, WeaponAngle);

    }
    //射击
    public override void shoot()
    {
        if (Time.time - timing > CD)
        {
            timing = Time.time;
            GameObject bullet = Instantiate(BulletPrefab, born.position, born.rotation * Quaternion.AngleAxis(Random.Range(0, shake), Vector3.forward);
            bullet.GetComponent <Bullet>().
        }
    }

}
