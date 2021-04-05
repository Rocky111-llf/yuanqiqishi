using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStart : Room
{
    // Start is called before the first frame update
    private void Start()
    {
        Initialization();
    }
    public override void Initialization()
    {
        base.Initialization();
    }

    public override void PlayerEnter()
    {
        base.PlayerEnter();
        IsExplored = true;
    }
}
