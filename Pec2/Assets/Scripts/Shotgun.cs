using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunController
{

    [SerializeField] private int numOfPellets = 8;

    public override void Shoot()
    {
        if (ammoController.RemainingAmmo() && !isRealoading)
        {
            base.Shoot();
            animator.SetTrigger("Pump");
            audioSource.clip = shootClip;
            audioSource.Play();
        }
    }

    protected override void PerformRaycast()
    {                
        for(int i = 0; i < numOfPellets; i++)
        {
            Vector3 shootDirection = Camera.main.transform.forward;
            shootDirection.x += Random.Range(-spread, spread);
            shootDirection.y += Random.Range(-spread, spread);

            if (Physics.Raycast(Camera.main.transform.position, shootDirection, out RaycastHit hit))
            {
                // Check if is a wall or an enemy
                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    // Init decal
                    Instantiate(decal, hit.point, Quaternion.LookRotation(hit.normal));
                }
                else if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    // Damage enemy
                    hit.collider.gameObject.GetComponent<EnemyAI>().Hit(damage);
                    audioSource.PlayOneShot(hitSound, 4f);
                }
                else if (hit.collider.gameObject.CompareTag("Terminal"))
                {
                    hit.collider.gameObject.GetComponent<TerminalController>().Shooted();
                }
            }
        }        
    }
}
