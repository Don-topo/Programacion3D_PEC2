using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{

    public Image shieldImage;

    private PlayerControler playerControler;

    // Start is called before the first frame update
    void Start()
    {
        playerControler = FindObjectOfType<PlayerControler>();    
    }

    // Update is called once per frame
    void Update()
    {
        shieldImage.fillAmount = (float)((float)playerControler.GetCurrentShield() / (float)playerControler.GetMaxShield());
    }
}
