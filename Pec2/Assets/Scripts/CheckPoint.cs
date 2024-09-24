using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private PlayerControler player;
    private AmmoController ammoController;
    private bool checkPointTriggered = false;
    [SerializeField] private GameObject message;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControler>();
        ammoController = FindObjectOfType<AmmoController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !checkPointTriggered){
            CheckPointInfo checkPointInfo = new CheckPointInfo(
                SceneManager.GetActiveScene().name, 
                player.GetCurrentHealth(), 
                player.GetCurrentShield(), 
                ammoController.GetAll(), 
                ammoController.GetCurrentAmmoIndex(), 
                player.transform.position.x,
                player.transform.position.y + 0.8f,
                player.transform.position.z
            );
            FileManager.DeleteCheckPoint();
            FileManager.SaveCheckpoint(checkPointInfo);
            checkPointTriggered = true;
            StartCoroutine(ViewText());
        }
    }

    IEnumerator ViewText()
    {
        message.SetActive(true);
        yield return new WaitForSeconds(2);
        message.SetActive(false);
    }
}
