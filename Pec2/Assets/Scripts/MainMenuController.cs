using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas optionsCanvas;
    public Canvas missionsCanvas;
    public Canvas exitCanvas;
    public Slider generalVolumeSlider;
    public Slider soundEffectsSlider;
    public AudioMixer audioMixer;
    public Animator animator;
    public TextMeshProUGUI generalVolumeText;
    public TextMeshProUGUI soundEffectsText;

    private GameInfo gameInfo;
    private int waitTimeTransition = 1;
    //private PlayInfo playInfo;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
        gameInfo = FileManager.LoadGameConfig();
       // playInfo = new PlayInfo();
        PrepareAudioMixers();
        SetAudio();
    }

    public void ShowOptions()
    {
        mainCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
    }

    public void HideOptions()
    {
        optionsCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        FileManager.SaveGameConfig(gameInfo);
    }


    public void CancelExit()
    {
        exitCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
    }

    public void ShowExit()
    {
        mainCanvas.gameObject.SetActive(false);
        exitCanvas.gameObject.SetActive(true);
    }

    public void ShowMissions()
    {
        mainCanvas.gameObject.SetActive(false);
        missionsCanvas.gameObject.SetActive(true);
    }

    public void BackFromMissions()
    {
        missionsCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
    }

    public void SetGeneralVolume()
    {
        generalVolumeText.text = ((int)generalVolumeSlider.value).ToString();
        float db = 20 * Mathf.Log10(generalVolumeSlider.value / 100);
        audioMixer.SetFloat("General", db);
        gameInfo.generalVolume = generalVolumeSlider.value;
    }

    public void SetSoundEffects()
    {
        soundEffectsText.text = ((int)soundEffectsSlider.value).ToString();
        float db = 20 * Mathf.Log10(soundEffectsSlider.value / 100);
        audioMixer.SetFloat("Effects", db);
        gameInfo.effectsVolume = soundEffectsSlider.value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void SelectLevel1()
    {
        StartCoroutine(LoadScene("Level1"));
    }

    public void SelectLevel2()
    {
        StartCoroutine(LoadScene("Level2"));
    }

    public void SelectLevel3()
    {
        StartCoroutine(LoadScene("Level3"));
    }


    IEnumerator LoadScene(string sceneName)
    {
        PreparePlayData();
        animator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(waitTimeTransition);
        SceneManager.LoadScene(sceneName);
    }

    private void PreparePlayData()
    {        
        FileManager.DeleteCheckPoint();
    }

    private void PrepareAudioMixers()
    {
        generalVolumeSlider.maxValue = 100;
        generalVolumeSlider.minValue = 0;
        generalVolumeSlider.value = gameInfo.generalVolume;
        generalVolumeText.text = ((int)gameInfo.generalVolume).ToString();
        soundEffectsSlider.maxValue = 100;
        soundEffectsSlider.minValue = 0;
        soundEffectsSlider.value = gameInfo.effectsVolume;
        soundEffectsText.text = ((int)gameInfo.effectsVolume).ToString();
    }

    private void SetAudio()
    {
        float db = 20 * Mathf.Log10(gameInfo.generalVolume / 100);
        audioMixer.SetFloat("General", db);
        db = 20 * Mathf.Log10(generalVolumeSlider.value / 100);
        audioMixer.SetFloat("Effects", db);
    }

}
