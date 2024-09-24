using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentShield;
    [SerializeField] private float maxShield;
    [SerializeField] private float shieldAbsorbRatio;
    [SerializeField] private GameObject hitPrefab;

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public float GetCurrentShield() => currentShield;
    public float GetMaxShield() => maxShield;

    public bool FullOfHealth() => currentHealth == maxHealth;

    public void SetInfo(float newCurrentHealth, float newCurrentShield, Vector3 newPosition)
    {
        currentHealth = newCurrentHealth;
        currentShield = newCurrentShield;
        transform.position = newPosition;
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public bool FullOfShield() => currentShield == maxShield;

    public void IncreaseShield(int amount)
    {
        currentShield += amount;
        if (currentShield > maxShield) currentShield = maxShield;
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckPointInfo checkPointInfo = FileManager.LoadCheckPoint();
        if(checkPointInfo != null)
        {
            currentHealth = checkPointInfo.health;
            currentShield = checkPointInfo.shield;
            gameObject.transform.position = new Vector3(checkPointInfo.playerXPosition, checkPointInfo.playerYPosition, checkPointInfo.playerZPosition);            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(float amount)
    {
        float shieldAbsorb = amount * shieldAbsorbRatio;
        float remainingDamage = amount - shieldAbsorb;
        if(currentShield > 0)
        {
            currentShield -= shieldAbsorb;
            if(currentShield < 0)
            {
                currentHealth += currentShield;
                currentShield = 0f;                
            }
            currentHealth -= remainingDamage;
        }
        else
        {
            currentHealth -= amount;
        }
        var pref = Instantiate(hitPrefab, transform);
        Destroy(pref, 2f);
        if (CheckIfPlayerIsDead())
        {
            GameManager.Instance.GameOver();
        }
    }

    public bool CheckIfPlayerIsDead()
    {
        return currentHealth <= 0;
    }
}
