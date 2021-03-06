using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContral : Weapon
{
    public GameObject BulletPrefab;
    //public Transform born;

    public float CD;
    private float timing = 0;

    private float WeaponAngle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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
        if (PlayerControl.instance.localscale > 0)
        {
            if (transform.parent.localScale.x == -PlayerControl.instance.localscale)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            if (transform.parent.localScale.x == -PlayerControl.instance.localscale)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        }
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
            GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation * Quaternion.AngleAxis(Random.Range(0, shake), Vector3.forward));
            bullet.GetComponent<Bullet>().Initialization(attack, role, BulletForce);
        }
    }

}
