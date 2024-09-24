using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GunController : MonoBehaviour
{
    [SerializeField] protected float cadence = 1f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected AudioClip shootClip;
    [SerializeField] protected AudioClip reloadClip;
    [SerializeField] protected GameObject decal;
    [SerializeField] protected Vector3 recoil;
    [SerializeField] protected GameObject bulletShell;
    [SerializeField] protected Transform casingTransform;
    [SerializeField] protected Vector3 shootPresitionPosition;
    [SerializeField] protected float smooth;
    [SerializeField] protected float smoothMultiply;
    [SerializeField] protected float aimSpeed;
    [SerializeField] protected GameObject fire;
    [SerializeField] protected Transform shootPosition;
    [SerializeField] protected float spread;
    [SerializeField] protected int reloadTime;
    [SerializeField] protected AudioClip hitSound;

    protected Animator animator;
    protected bool gunOnCooldown = false;
    protected bool isAiming = false;
    protected bool isRealoading = false;
    protected AudioSource audioSource;
    protected Vector3 startRotation;
    protected Vector3 StartPosition;
    protected AmmoController ammoController;

    public bool GetIsAiming() => isAiming;

    public bool GetIsReloading() => isRealoading;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startRotation = transform.localEulerAngles;
        StartPosition = transform.localPosition;
        animator = GetComponent<Animator>();
        ammoController = FindObjectOfType<AmmoController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PlayerCanPlay())
        {
            Aim();
            PrepareShoot();
            Sway();
            CheckReload();
        }        
    }


    public virtual void Shoot()
    {
        if (ammoController.RemainingAmmo() && !isRealoading)
        {
            // Delay between shoots
            StartCoroutine(GunCooldown());
            // Reduce ammo            
            ammoController.DecreaseAmmo();
            // Raycast
            PerformRaycast();
            // Add recoil
            AddRecoil();
            // Fire effect
            Instantiate(fire, shootPosition);
            // Create Casing
            CreateCasing();
        }
    }

    protected virtual void PerformRaycast()
    {

        Vector3 shootDirection = Camera.main.transform.forward;
        if (!isAiming)
        {
            shootDirection.x += Random.Range(-spread, spread);
            shootDirection.y += Random.Range(-spread, spread);
        }

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
                // Destroy terminal
                hit.collider.gameObject.GetComponent<TerminalController>().Shooted();
            }
        }
    }

    protected void AddRecoil()
    {
        transform.localEulerAngles += recoil;
    }

    protected void SubRecoil()
    {
        transform.localEulerAngles = startRotation;
    }

    protected void CreateCasing()
    {
        Instantiate(bulletShell, casingTransform);
    }

    protected void Sway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * smoothMultiply;
        float mouseY = Input.GetAxisRaw("Mouse Y") * smoothMultiply;

        Quaternion rotateX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotateY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion target = rotateX * rotateY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, smooth * Time.deltaTime);
    }

    protected void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            transform.localPosition = Vector3.Lerp(transform.localPosition, shootPresitionPosition, aimSpeed * Time.deltaTime);
        }
        else
        {
            isAiming = false;
            transform.localPosition = Vector3.Lerp(transform.localPosition, StartPosition, aimSpeed * Time.deltaTime);
        }
    }

    protected void PrepareShoot()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && !gunOnCooldown)
        {
            Shoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SubRecoil();
        }
    }

    protected void CheckReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRealoading && ammoController.GetCurrentAmmo().currentAmmo > 0 && !ammoController.IsMagazineFull())
        {
            StartCoroutine(ReloadCooldown());
            animator.SetTrigger("triggerReload");
            audioSource.clip = reloadClip;
            audioSource.PlayOneShot(reloadClip, 0.7f);
            ammoController.ReloadAmmo();  
        }
    }

    protected IEnumerator GunCooldown()
    {
        gunOnCooldown = true;
        yield return new WaitForSeconds(cadence);
        gunOnCooldown = false;
    }

    protected IEnumerator ReloadCooldown()
    {
        isRealoading = true;
        yield return new WaitForSeconds(reloadTime);
        isRealoading = false;
    }
}
