using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoorByTerminal : AutomaticDoor
{
    [SerializeField] private int remainingTerminals = 1;
    [SerializeField] private AudioClip closedSound;

    public void DestroyTerminal()
    {
        remainingTerminals--;
        if (remainingTerminals <= 0) isOpening = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(remainingTerminals > 0)
        {
            audioSource.clip = closedSound;
            audioSource.Play();
        }
    }

    protected override void OnTriggerExit(Collider other){}
}
