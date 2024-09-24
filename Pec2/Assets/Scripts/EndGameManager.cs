using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{

    [SerializeField] Animator animator;
    private int waitTimeTransition = 1;
    private CheckPointInfo checkPointInfo;

    // Start is called before the first frame update
    void Start()
    {
        checkPointInfo = FileManager.LoadCheckPoint();
        Cursor.lockState = 0;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {        
        StartCoroutine(LoadScene(checkPointInfo.levelName));
    }

    public void End()
    {
        StartCoroutine(LoadScene("Menu"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        animator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(waitTimeTransition);
        SceneManager.LoadScene(sceneName);
    }
}
