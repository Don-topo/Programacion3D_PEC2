using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ammo 
{
    public int currentMagazine;
    public int maxMagazine;
    public int currentAmmo;
    public int maxAmmo;

    public Ammo(int magazine, int totalMagazine, int totalAmmo, int maxAmmo)
    {
        currentMagazine = magazine;
        maxMagazine = totalMagazine;
        currentAmmo = totalAmmo;
        this.maxAmmo = maxAmmo;
    }
}
