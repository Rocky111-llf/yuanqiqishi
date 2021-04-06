using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTreasure : Room
{
    private void Start()
    {
        Initialization();
    }
    public override void PlayerEnter()
    {
        base.PlayerEnter();
        IsExplored = true;

    }
}
