using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject DoorCollider;

    public void OpenDoor()
    {
        foreach(Animator animator in GetComponentsInChildren<Animator>())
        {
            animator.Play("Close");
            DoorCollider.SetActive(false);
        }
    }
    public void CloseDoor()
    {
        foreach (Animator animator in GetComponentsInChildren<Animator>())
        {
            animator.Play("Open");
            DoorCollider.SetActive(true);
        }
    }
}
