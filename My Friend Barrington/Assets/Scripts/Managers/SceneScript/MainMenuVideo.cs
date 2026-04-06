using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class MainMenuVideo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private GameObject mainMenuVideo;
    [SerializeField]
    private GameObject fmodEmiter;
    [Header("Audio (FMOD)")]
    [SerializeField] private EventReference mainMenuPressedSound;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = Resources.Load<VideoClip>("Video/UnityVideoFolder/Intro Animation V2(1)");
        mainMenuVideo.SetActive(false);
        fmodEmiter = GameObject.Find("FMODEvent");
        fmodEmiter.SetActive(true);
        //Debug.Log(Resources.Load<VideoClip>("Videos/MainMenuVideo/Intro Animation V2(1)"));
    }

    // play video, wait to end, then load next scene
    public IEnumerator buttonPressed()
    {
        fmodEmiter.SetActive(false);
        RuntimeManager.PlayOneShotAttached(mainMenuPressedSound, gameObject);
        yield return new WaitForSeconds(2f);
        mainMenuVideo.SetActive(true);
        videoPlayer.Play();
        yield return new WaitForSeconds((float)videoPlayer.clip.length);
        SceneManagerScript.instance.nextScene("Lvl 1");
        mainMenuVideo.SetActive(false);
    }

    public void playVideo()
    {
        StartCoroutine(buttonPressed());
    }
}
