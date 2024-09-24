using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoorByLever : AutomaticDoor
{
    [SerializeField] private int countdown = 10;
    [SerializeField] private AudioClip closedSound;

    public void PullLever()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        isClosing = false;
        isOpening = true;
        yield return new WaitForSeconds(countdown);
        isOpening = false;
        isClosing = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!isOpening)
        {
            audioSource.clip = closedSound;
            audioSource.Play();
        }        
    }

    protected override void OnTriggerExit(Collider other){}

}
