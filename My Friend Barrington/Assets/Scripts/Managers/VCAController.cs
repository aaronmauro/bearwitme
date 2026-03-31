using UnityEngine;
using UnityEngine.UI;

public class VCAController : MonoBehaviour
{
    private FMOD.Studio.VCA vca;
    public string vcaName;

    private Slider slider;
    private string saveKey;

    void Start()
    {
        vca = FMODUnity.RuntimeManager.GetVCA("vca:/" + vcaName);
        slider = GetComponent<Slider>();

        saveKey = "VCA_" + vcaName;

        // Load saved volume (default = 1.0)
        float savedVolume = PlayerPrefs.GetFloat(saveKey, 1.0f);

        slider.value = savedVolume;
        vca.setVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        vca.setVolume(volume);

        // Save it
        PlayerPrefs.SetFloat(saveKey, volume);
        PlayerPrefs.Save();
    }
}