using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoss : Room
{
    public Enemy Boss;
    //������
    public GameObject ChuanSongMen;
    //�����ų�����
    public Transform Point;

    private void Start()
    {
        Initialization();
    }
    private void Update()
    {
        if (IsExplored)
        {
            Instantiate(ChuanSongMen, Point.position,Quaternion.identity);
            if (Input.GetMouseButtonDown(0))
            {
                GameControl.instance.GameOver();
            }
        }
    }

    public override void Initialization()
    {
        base.Initialization();
        Boss.Initialzation(this);
    }

    public override void EnemyDie(Enemy enemy)
    {
        base.EnemyDie(enemy);
        IsExplored = true;
    }

    public override void PlayerEnter()
    {
        base.PlayerEnter();
        if (!IsExplored)
        {
            CloseDoor();
            Boss.isopen = true;
        }
    }

}
