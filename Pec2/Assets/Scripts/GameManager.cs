using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas confirmCanvas;
    public Animator animator;

    public static GameManager Instance { get { return gmInstance; } }

    private static GameManager gmInstance;
    private bool isGamePaused = false;
    private bool isGameOnConfirmedPage = false;
    private bool playerCanPlay = true;
    private bool blockUI = false;
    private PlayerControler player;
    private AmmoController ammoController;
    private int waitTimeTransition = 1;
    private CheckPointInfo checkPoint;


    // Start is called before the first frame update
    private void Awake()
    {
        if (gmInstance != null && gmInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {            
            gmInstance = this;            
        }
    }

    private void Start()
    {
        checkPoint = FileManager.LoadCheckPoint();
        if (checkPoint == null)
        {
            player = FindObjectOfType<PlayerControler>();
            ammoController = FindObjectOfType<AmmoController>();
            checkPoint = new CheckPointInfo(
               SceneManager.GetActiveScene().name,
               player.GetCurrentHealth(),
               player.GetCurrentShield(),
               ammoController.GetAll(),
               ammoController.GetCurrentAmmoIndex(),
               player.transform.position.x,
               player.transform.position.y + 0.8f,
               player.transform.position.z
           );
            FileManager.SaveCheckpoint(checkPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                if (isGameOnConfirmedPage)
                {
                    ExitConfirmMenu();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Cursor.visible = true; 
        playerCanPlay = false;
        pauseCanvas.gameObject.SetActive(true);
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        playerCanPlay = true;
        pauseCanvas.gameObject.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void ConfirmExitMenu()
    {
        pauseCanvas.gameObject.SetActive(false);
        confirmCanvas.gameObject.SetActive(true);
        Cursor.visible = true;
        isGameOnConfirmedPage = true;
    }

    public void ExitConfirmMenu()
    {
        isGameOnConfirmedPage = false;
        confirmCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        animator.SetTrigger("StartTransition");
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        playerCanPlay = false;        
        StartCoroutine(LoadScene(5));
    }

    public bool IsGameOver()
    {
        return true;
    }

    public void CompleteLevel()
    {
        FileManager.DeleteCheckPoint();
        if (SceneManager.GetActiveScene().name.Equals("Level3"))
        {
            // End Game
            StartCoroutine(LoadScene(6));
        }
        else
        {            
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));            
        }
    }

    IEnumerator LoadScene(int sceneName)
    {
        animator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(waitTimeTransition);
        SceneManager.LoadScene(sceneName);
    }

    public bool PlayerCanPlay() { return playerCanPlay; }

    public bool BlockUI() { return blockUI; }

}
