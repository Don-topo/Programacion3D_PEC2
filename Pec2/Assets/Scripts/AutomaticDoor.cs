using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;
    [SerializeField] private Transform openLeftDoor;
    [SerializeField] private Transform closedLeftDoor;
    [SerializeField] private Transform openRightDoor;
    [SerializeField] private Transform closedRightDoor;
    [SerializeField] private AudioClip openAudio;

    protected bool isOpening = false;
    protected bool isClosing = false;
    protected AudioSource audioSource;    
    private Vector3 distance;

    public float movementSpeed = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            OpenDoor();
        } else if (isClosing)
        {
            CloseDoor();
        }
    }

    protected void CloseDoor()
    {
        distance = leftDoor.transform.localPosition - closedLeftDoor.transform.localPosition;

        if(distance.magnitude < 0.001f)
        {
            isClosing = false;
            leftDoor.transform.localPosition = closedLeftDoor.transform.localPosition;
            rightDoor.transform.localPosition = closedRightDoor.transform.localPosition;
        }
        else
        {
            leftDoor.transform.localPosition = Vector3.Lerp(leftDoor.transform.localPosition, closedLeftDoor.transform.localPosition, Time.deltaTime * movementSpeed);
            rightDoor.transform.localPosition = Vector3.Lerp(rightDoor.transform.localPosition, closedRightDoor.transform.localPosition, Time.deltaTime * movementSpeed);
        }
    }

    protected void OpenDoor()
    {
        distance = leftDoor.transform.localPosition - openLeftDoor.transform.localPosition;

        if(distance.magnitude < 0.001f)
        {
            isOpening = false;
            leftDoor.transform.localPosition = openLeftDoor.transform.localPosition;
            rightDoor.transform.localPosition = openRightDoor.transform.localPosition;
        }
        else
        {
            leftDoor.transform.localPosition = Vector3.Lerp(leftDoor.transform.localPosition, openLeftDoor.transform.localPosition, Time.deltaTime * movementSpeed);
            rightDoor.transform.localPosition = Vector3.Lerp(rightDoor.transform.localPosition, openRightDoor.transform.localPosition, Time.deltaTime * movementSpeed);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            isOpening = true;
            isClosing = false;
            audioSource.clip = openAudio;
            audioSource.Play();
        }        
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            isOpening = false;
            isClosing = true;
            audioSource.clip = openAudio;
            audioSource.Play();
        }        
    }
}
