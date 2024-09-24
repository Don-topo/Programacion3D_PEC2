using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoorByKey : AutomaticDoor
{
    [SerializeField] private AudioClip closedDoor;
    private bool opened = false;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !opened)
        {
            KeyController keyController = other.gameObject.GetComponent<KeyController>();
            if (keyController.IsKeyPicked())
            {
                base.OnTriggerEnter(other);
                opened = true;
            }
            else
            {
                audioSource.clip = closedDoor;
                audioSource.Play();
            }
        }
    }

    protected override void OnTriggerExit(Collider other){}

}
