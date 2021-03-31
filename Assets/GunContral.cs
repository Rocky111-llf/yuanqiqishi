using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContral : MonoBehaviour
{
    public Transform Player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gunmove();
    }
    //¸úËæ½ÇÉ«
    public void Gunmove()
    {
        if (Player.transform.localScale.x > 0)
        {
            transform.position = new Vector3(Player.transform.position.x + 0.5f, Player.transform.position.y - 0.5f, Player.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Player.transform.position.x - 0.5f, Player.transform.position.y - 0.5f, Player.transform.position.z);
        }
    }
}
