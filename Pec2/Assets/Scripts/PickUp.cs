using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AmmoController;

public class PickUp : MonoBehaviour
{
    public enum PickUpType { health, shield, ammo, key };

    public PickUpType pickUpType;
    public int amount;
    public Guns guns;
    public AudioClip healthSound;
    public AudioClip shieldSound;
    public AudioClip ammoSound;
    public AudioClip keySound;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (pickUpType)
            {
                case PickUpType.health:
                    PlayerControler playerControler = other.gameObject.GetComponent<PlayerControler>();
                    if (!playerControler.FullOfHealth())
                    {
                        playerControler.IncreaseHealth(amount);
                        // Play Sound
                        audioSource.clip = healthSound;
                        audioSource.Play();
                        Destroy(gameObject, audioSource.clip.length);
                    }
                    break;
                case PickUpType.shield:
                    PlayerControler player = other.gameObject.GetComponent<PlayerControler>();
                    if (!player.FullOfShield())
                    {
                        player.IncreaseShield(amount);
                        // Play Sound
                        audioSource.clip = shieldSound;
                        audioSource.Play();
                        Destroy(gameObject, audioSource.clip.length);
                    }
                    break;
                case PickUpType.ammo:
                    AmmoController ammoController = other.gameObject.GetComponent<AmmoController>();
                    if (!ammoController.FullAmmo(((int)guns)))
                    {
                        ammoController.IncreaseAmmo((int)guns, amount);
                        // Play Sound
                        audioSource.clip = ammoSound;
                        audioSource.Play();
                        Destroy(gameObject, audioSource.clip.length);
                    }
                    break;
                case PickUpType.key:
                    KeyController keyController = other.gameObject.GetComponent<KeyController>();
                    keyController.PickKey();
                    audioSource.clip = keySound;
                    audioSource.Play();
                    Destroy(gameObject, audioSource.clip.length);
                    break;
            }            
        }
    }
}
