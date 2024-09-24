using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CheckPointInfo 
{
    public string levelName;
    public float health;
    public float shield;
    public Ammo[] ammo;
    public int currentGun;
    public float playerXPosition;
    public float playerYPosition;
    public float playerZPosition;

    public CheckPointInfo(string newLevelName, float newHealth, float newShield, Ammo[] newAmmo, int newCurrentGun, float newX, float newY, float newZ)
    {
        levelName = newLevelName;
        health = newHealth;
        shield = newShield;
        ammo = newAmmo;
        currentGun = newCurrentGun;
        playerXPosition = newX;
        playerYPosition = newY;
        playerZPosition = newZ;
    }
}
