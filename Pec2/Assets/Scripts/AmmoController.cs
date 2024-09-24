using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public enum Guns
    {
        PISTOL,
        MACHINEGUN,
        SHOTGUN
    }

    public Ammo[] ammo;

    private int currentGun;

    public Ammo GetCurrentAmmo() => ammo[currentGun];

    public Ammo[] GetAll() => ammo;

    public void SetCurrentAmmo(int newCurrentAmmo) { currentGun = newCurrentAmmo; }

    public int GetCurrentAmmoIndex() => currentGun;

    public void SetInfo(Ammo[] newAmmo, int newCurrentGun)
    {
        ammo = newAmmo;
        currentGun = newCurrentGun;
    }

    public void DecreaseAmmo() { ammo[currentGun].currentMagazine--; }

    public void ReloadAmmo() {
        int reloadAmount = ammo[currentGun].maxMagazine - ammo[currentGun].currentMagazine;
        reloadAmount = (ammo[currentGun].currentAmmo - reloadAmount) >= 0 ? reloadAmount : ammo[currentGun].currentAmmo;
        ammo[currentGun].currentMagazine += reloadAmount;
        ammo[currentGun].currentAmmo -= reloadAmount;
    }

    public bool RemainingAmmo()
    {
        return ammo[currentGun].currentMagazine >= 1;
    }

    public bool FullAmmo(int gun) => ammo[gun].currentAmmo == ammo[gun].maxAmmo;

    public void IncreaseAmmo(int gun, int amount)
    {
        ammo[gun].currentAmmo += amount;
        if (ammo[gun].currentAmmo > ammo[gun].maxAmmo) ammo[gun].currentAmmo = ammo[gun].maxAmmo;        
    }

    public bool IsMagazineFull() => ammo[currentGun].currentMagazine == ammo[currentGun].maxMagazine;


    // Start is called before the first frame update
    void Start()
    {
        CheckPointInfo checkPointInfo = FileManager.LoadCheckPoint();
        if(checkPointInfo != null)
        {
            currentGun = checkPointInfo.currentGun;
            ammo = checkPointInfo.ammo;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
