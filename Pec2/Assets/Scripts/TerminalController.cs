using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour
{
    public GameObject door;
    public new Light light;

    private AudioSource audioSource;
    private bool exploded = false;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private GameObject explotionPrefab;
    [SerializeField] private Transform explotionTransform;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = destroySound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shooted()
    {
        if (!exploded)
        {
            exploded = true;
            light.enabled = false;
            // Play sound
            audioSource.Play();
            // Play Effect
            door.GetComponent<ClosedDoorByTerminal>().DestroyTerminal();
            var pref = Instantiate(explotionPrefab, explotionTransform);
            GetComponent<BoxCollider>().enabled = false;
            Destroy(pref, 3);
        }
        
    }
}
