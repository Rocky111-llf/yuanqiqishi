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
    //Ѫ�������ף�ħ��
    public SpecificValue PlayerHP;
    public SpecificValue PlayerDP;
    public SpecificValue PlayerMP;
    //�ƶ��ٶ�
    public float speed;
    //������ȴʱ��
    private float DPtiming;
    public float DPcd;
    //���׻ָ�
    bool DPrestore;
    //��ɫ
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
        /*weapon = MyWeapon.GetComponent<Weapon>();//�����Ƿ��ʼ��
        weapon.PickUp();
        MyWeapon.transform.SetParent(this.transform);
        MyWeapon.transform.localPosition = new Vector3(0, 0, 0);
        MyWeapon.transform.rotation = Quaternion.identity;
        weapon.Initialization(gameObject.tag, gameObject.layer);*/
    }

     void Update()
    {
        //Բ�μ��
        RayForCycle();
        //���
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
                    Debug.Log("�ֵ�");
                }

            }
        }
        //�ж��Ƿ�ػ���
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
    //���ƽ�ɫ�ƶ��ͷ���
    public void movement()
    {
        //��ɫ�ƶ�
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
        //��ɫ����
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

    }
    //���˺���
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
    //�ж���Χ�Ƿ���ǹ(Բ�μ��)
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
    //��ǹ����
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
            //ע��
            weapon.Initialization(gameObject.tag, gameObject.layer);
        }
        

    }
}
