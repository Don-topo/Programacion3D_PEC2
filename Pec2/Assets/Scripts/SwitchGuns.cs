using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGuns : MonoBehaviour
{

    public int selectedWeapon = 0;
    private AmmoController ammoController;

    // Start is called before the first frame update
    void Start()
    {
        ammoController = FindObjectOfType<AmmoController>();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedGun = selectedWeapon;

        if (!transform.GetChild(selectedWeapon).GetComponent<GunController>().GetIsReloading())
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                {
                    selectedWeapon = 0;
                }
                else
                {
                    selectedWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                {
                    selectedWeapon = transform.childCount - 1;
                }
                else
                {
                    selectedWeapon--;
                }
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                selectedWeapon = 1;
            }

            if (Input.GetKey(KeyCode.Alpha3))
            {
                selectedWeapon = 2;
            }

            if (previousSelectedGun != selectedWeapon)
            {
                SelectWeapon();
            }            
        }
        ammoController.SetCurrentAmmo(selectedWeapon);
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform gun in transform)
        {
            if(i == selectedWeapon)
            {
                gun.gameObject.SetActive(true);
                ammoController.SetCurrentAmmo(i);

            }
            else
            {
                gun.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
