using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Quaternion initialPosition;
    public Quaternion endPosition;
    public int duration = 10;

    private bool interactable = false;
    private bool isOpened = false;
    private AudioSource audioSource;
    [SerializeField] GameObject door;
    [SerializeField] Transform leverTransform;
    [SerializeField] AudioClip leverAudio;
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = leverAudio;
    }

    // Update is called once per frame
    void Update()
    {
        InteractWithDoor();
    }

    private void InteractWithDoor()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            door.GetComponent<ClosedDoorByLever>().PullLever();
            StartCoroutine(OpeningTime());
            isOpened = true;
            audioSource.Play();
        }
    }

    IEnumerator OpeningTime()
    {
        leverTransform.rotation = endPosition;
        yield return new WaitForSeconds(duration);
        audioSource.Play();
        isOpened = false;
        leverTransform.rotation = initialPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactable = true;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactable = true;
        }
    }
}
