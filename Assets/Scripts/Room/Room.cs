using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform[] doors;
    public bool IsExplored;
    public int RoomNumber;
    //public Room[] NextRooms;

    public virtual void Initialization()
    {
        IsExplored = false;
        OpenDoor();
    }
    //开门
    public void OpenDoor()
    {
        foreach(Transform door in doors)
        {
            foreach(Animator anim in door.GetComponentsInChildren<Animator>())
            {
                anim.Play("Open");
            }
            door.GetComponentInChildren<Collider2D>(true).gameObject.SetActive(false);
        }
    }
    //关门
    public void CloseDoor()
    {
        foreach (Transform door in doors)
        {
            foreach (Animator animator in door.GetComponentsInChildren<Animator>())
            {
                animator.Play("Close");
            }

            door.GetComponentInChildren<Collider2D>(true).gameObject.SetActive(true);
        }
    }

    public virtual void PlayerEnter()
    {
    }

    public virtual void UpdateRoomMessage()
    {

    }

    public virtual void EnemyDie(Enemy enemy)
    {

    }
}
