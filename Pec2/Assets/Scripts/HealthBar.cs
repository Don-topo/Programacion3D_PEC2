using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthImage;

    private PlayerControler playerControler;

    // Start is called before the first frame update
    void Start()
    {
        playerControler = FindObjectOfType<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = playerControler.GetCurrentHealth() / playerControler.GetMaxHealth();
    }
}
