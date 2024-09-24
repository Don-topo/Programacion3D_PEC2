using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointOfView : MonoBehaviour
{
    // Start is called before the first frame update
    private GunController gun;
    private Image sigh;

    void Start()
    {
        gun = FindObjectOfType<GunController>();
        sigh = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color temp = sigh.color;
        gun = FindObjectOfType<GunController>();

        if (gun.GetIsAiming())
        {
            temp.a = 0;
            sigh.color = temp;
        }
        else
        {
            temp.a = 255;
            sigh.color = temp;
        }
    }
}
