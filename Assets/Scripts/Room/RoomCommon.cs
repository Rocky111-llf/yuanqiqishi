using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCommon : Room
{
    public GameObject EnemyGroup;
    List<Enemy> enemys = new List<Enemy>();
    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoomMessage();
    }

    public override void Initialization()
    {
        base.Initialization();
        EnemyGroup.SetActive(true);
        Enemy[] TheEnemys = EnemyGroup.GetComponentsInChildren<Enemy>();
        for (int i = 0; i < TheEnemys.Length; i++)
        {
            enemys.Add(TheEnemys[i]);
            TheEnemys[i].Initialzation(this);//敌人属于该房间
        }
    }

    //敌人死亡
    public override void EnemyDie(Enemy enemy)
    {
        enemys.Remove(enemy);
    }
    //角色进入
    public override void PlayerEnter()
    {
        base.PlayerEnter();
        if (!IsExplored)
        {
            CloseDoor();
            AwakeEnemy();
        }
    }
    //唤醒敌人
    public void AwakeEnemy()
    {
        foreach (Enemy enemy in enemys)
        {
            enemy.isopen = true;
        }
    }
    //更新房间信息
    public override void UpdateRoomMessage()
    {
        if (enemys.Count == 0)
        {
           
            IsExplored = true;
            OpenDoor();

        }
        
    }
}
