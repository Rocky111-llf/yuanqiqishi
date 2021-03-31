using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
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
            rb.velocity = new Vector2(horizontalmove * speed, verticalmove * speed);
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false); 
        }
        //角色方向
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

    }
}
