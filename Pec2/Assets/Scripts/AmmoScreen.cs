using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text remainingAmmo;
    [SerializeField] private TMP_Text remainingMagazine;
    private AmmoController gun;
    

    // Start is called before the first frame update
    void Start()
    {
        gun = FindAnyObjectByType<AmmoController>();    
    }

    // Update is called once per frame
    void Update()
    {
        gun = FindAnyObjectByType<AmmoController>();
        remainingMagazine.text = gun.GetCurrentAmmo().currentMagazine.ToString();
        remainingAmmo.text = gun.GetCurrentAmmo().currentAmmo.ToString();
    }
}
