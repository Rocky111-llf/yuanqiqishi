using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SpecificValue
{
     public float CurrentValue;
     public float MaxValue;

}

public class PlayerControl : MonoBehaviour,BeAttack
{
    public static PlayerControl instance;
    //Ѫ�������ף�����
    public  SpecificValue PlayerHP;
    public SpecificValue PlayerDP;
    //public SpecificValue PlayerMP;
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
    //UI
    public Text playerhp;
    public Text playerdp;


    private Rigidbody2D rb;
    private Animator anim;
    private Weapon weapon;

    private GameObject WeaponInFloor;
    List<GameObject> NearWeapons = new List<GameObject>();

    public float localscale;
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
        PlayerHP.MaxValue = 6;
        PlayerHP.CurrentValue = 6;
        PlayerDP.MaxValue = 5;
        PlayerDP.CurrentValue = 5;
        playerhp.text = PlayerHP.CurrentValue.ToString();
        playerdp.text = PlayerDP.CurrentValue.ToString();
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
            localscale = transform.localScale.x;
            GetWeapon();
        }
        else
        {
            if (Input.GetMouseButtonDown(0)||Input.GetMouseButton(0))
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
            playerdp.text = PlayerDP.CurrentValue.ToString();
            if (PlayerDP.CurrentValue >= PlayerDP.MaxValue)
            {
                PlayerDP.CurrentValue = PlayerDP.MaxValue;
                playerdp.text = PlayerDP.CurrentValue.ToString();
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
            if (PlayerHP.CurrentValue < 0)
            {
                PlayerHP.CurrentValue = 0;
            }
            playerhp.text = PlayerHP.CurrentValue.ToString();
        }
        else if (PlayerDP.CurrentValue < Value)
        {
            Value = Value - PlayerDP.CurrentValue;
            PlayerDP.CurrentValue = 0;
            PlayerHP.CurrentValue -= Value;
            if (PlayerHP.CurrentValue < 0)
            {
                PlayerHP.CurrentValue = 0;
            }
            playerdp.text = PlayerDP.CurrentValue.ToString();
            playerhp.text = PlayerHP.CurrentValue.ToString();
        }
        else
        {
            PlayerDP.CurrentValue -= Value;
            playerdp.text = PlayerDP.CurrentValue.ToString();
        }
        if (PlayerHP.CurrentValue <= 0)
        {
            anim.SetBool("die", true);
            die = true;
            GetComponent<Collider2D>().enabled = false;
            Destroy(rb);
            DPrestore = false;
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
