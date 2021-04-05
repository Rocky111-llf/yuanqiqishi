using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificValue
{
    public float CurrentValue;
    public float MaxValue;
    public float Exist
    {
        get { return CurrentValue / MaxValue; }
    }
}

public class PlayerControl : MonoBehaviour,BeAttack
{
    public static PlayerControl instance;
    //血量，护甲，魔法
    public SpecificValue PlayerHP;
    public SpecificValue PlayerDP;
    public SpecificValue PlayerMP;
    //移动速度
    public float speed;
    //护甲冷却时长
    private float DPtiming;
    public float DPcd;
    //护甲恢复
    bool DPrestore;
    //角色
    private GameObject MyPlayer;
    private GameObject MyWeapon;

    private Rigidbody2D rb;
    public Animator anim;
    public Weapon weapon;

    private GameObject WeaponInFloor;
    List<GameObject> NearWeapons = new List<GameObject>();


    private bool die;


    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        die = false;
        weapon = null;
        /*weapon = MyWeapon.GetComponent<Weapon>();//武器是否初始化
        weapon.PickUp();
        MyWeapon.transform.SetParent(this.transform);
        MyWeapon.transform.localPosition = new Vector3(0, 0, 0);
        MyWeapon.transform.rotation = Quaternion.identity;
        weapon.Initialization(gameObject.tag, gameObject.layer);*/
    }

     void Update()
    {
        //圆形检测
        RayForCycle();
        //射击
        if (WeaponInFloor != null && Input.GetMouseButtonDown(0))
        {
            GetWeapon();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (weapon != null)
                {
                    weapon.shoot();
                }
                else
                {
                    Debug.Log("手刀");
                }

            }
        }
        //判断是否回护甲
        if (Time.time - DPtiming > DPcd && DPrestore)
        {
            DPtiming = Time.time;
            PlayerDP.CurrentValue += 1;
            if (PlayerDP.CurrentValue >= PlayerDP.MaxValue)
            {
                PlayerDP.CurrentValue = PlayerDP.MaxValue;
                DPrestore = false;
            }
        }
        if (weapon != null)
        {
            MyWeapon.GetComponent<GunContral>().GunRorate();
        }
    }

     void FixedUpdate()
    {
        if (!die)
        {
            movement();
        }
    }
    //控制角色移动和方向
    public void movement()
    {
        //角色移动
        float horizontalmove = Input.GetAxis("Horizontal");
        float verticalmove = Input.GetAxis("Vertical");
        float facedirection = Input.GetAxisRaw("Horizontal");

        if(horizontalmove != 0 || verticalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime , verticalmove * speed * Time.fixedDeltaTime);
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false); 
        }
        //角色方向
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

    }
    //受伤函数
    public void BeAttack(float Value)
    {
        DPrestore = true;
        if (PlayerDP.CurrentValue == 0)
        {
            PlayerHP.CurrentValue -= Value;
        }else if (PlayerDP.CurrentValue < Value)
        {
            Value = Value - PlayerDP.CurrentValue;
            PlayerDP.CurrentValue = 0;
            PlayerHP.CurrentValue -= Value;
        }
        else
        {
            PlayerDP.CurrentValue -= Value;
        }
        if (PlayerHP.CurrentValue <= 0)
        {
            anim.SetBool("die", true);
            die = true;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            anim.Play("BeAttack");
        }
    }
    //判断周围是否有枪(圆形检测)
    public void RayForCycle()
    {
        WeaponInFloor = null;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 0.9f);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Weapon"))
                {
                    WeaponInFloor = cols[i].gameObject;
                }
            }
        }
    }
    //换枪函数
    public void GetWeapon()
    {
        if (WeaponInFloor != null)
        {
            if (weapon != null)
            {     
                MyWeapon.transform.SetParent(GameControl.instance.WeaponRecycle);
                weapon.PickDown();
            }
            MyWeapon = WeaponInFloor;
            weapon = MyWeapon.GetComponent<Weapon>();
            weapon.PickUp();
            MyWeapon.transform.SetParent(this.transform,true);
            MyWeapon.transform.localPosition = new Vector3(0, -0.5f, 0);
            MyWeapon.transform.rotation = Quaternion.identity;
            //注册
            weapon.Initialization(gameObject.tag, gameObject.layer);
        }
        

    }
}
