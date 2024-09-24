using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public int maxShield = 100;

    public int GetShield() { return currentShield; }

    private int currentShield;


    // Start is called before the first frame update
    void Start()
    {
        currentShield = 0;    
    }

    public bool MaxShield()
    {
        return (currentShield < maxShield);
    }

    public void IncreaseShield(int amount)
    {
        currentShield += amount;
    }
}
