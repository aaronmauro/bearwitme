using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class RiftEffectManager : MonoBehaviour
{
    public static RiftEffectManager Instance;

    [Header("Fullscreen Effect")]
    [SerializeField] private FullScreenPassRendererFeature riftShader;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private string intensityProperty = "_Intensity";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Fade Settings")]
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 1f;

    private Material runtimeMaterial;
    private Coroutine fadeCoroutine;
    private int insideCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        runtimeMaterial = new Material(baseMaterial);
        runtimeMaterial.name = baseMaterial.name + " (Runtime)";
        runtimeMaterial.SetFloat(intensityProperty, 0f);

        if (riftShader != null)
            riftShader.passMaterial = runtimeMaterial;

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = true;
            audioSource.volume = 0f;
        }
    }

    public void EnterRift()
    {
        insideCount++;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (riftShader != null)
            riftShader.passMaterial = runtimeMaterial;

        fadeCoroutine = StartCoroutine(FadeTo(1f, fadeInDuration));
    }

    public void ExitRift()
    {
        insideCount = Mathf.Max(0, insideCount - 1);

        if (insideCount > 0)
            return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutAndStop());
    }

    public void ForceOff()
    {
        insideCount = 0;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        if (runtimeMaterial != null)
            runtimeMaterial.SetFloat(intensityProperty, 0f);

        if (riftShader != null)
            riftShader.passMaterial = runtimeMaterial;

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.volume = 0f;
        }
    }

    private IEnumerator FadeTo(float targetIntensity, float duration)
    {
        if (runtimeMaterial == null)
            yield break;

        float startIntensity = runtimeMaterial.GetFloat(intensityProperty);
        float startVolume = audioSource != null ? audioSource.volume : 0f;

        if (targetIntensity > 0f && audioSource != null && !audioSource.isPlaying)
            audioSource.Play();

        if (duration <= 0f)
        {
            runtimeMaterial.SetFloat(intensityProperty, targetIntensity);

            if (audioSource != null)
                audioSource.volume = targetIntensity;

            yield break;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            t = t * t * (3f - 2f * t);

            float currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            runtimeMaterial.SetFloat(intensityProperty, currentIntensity);

            if (audioSource != null)
                audioSource.volume = Mathf.Lerp(startVolume, targetIntensity, t);

            yield return null;
        }

        runtimeMaterial.SetFloat(intensityProperty, targetIntensity);

        if (audioSource != null)
            audioSource.volume = targetIntensity;
    }

    private IEnumerator FadeOutAndStop()
    {
        yield return FadeTo(0f, fadeOutDuration);

        if (runtimeMaterial != null)
            runtimeMaterial.SetFloat(intensityProperty, 0f);

        if (riftShader != null)
            riftShader.passMaterial = runtimeMaterial;

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.volume = 0f;
        }
    }

    private void OnDisable()
    {
        ForceOff();
    }

    private void OnDestroy()
    {
        ForceOff();

        if (runtimeMaterial != null)
            Destroy(runtimeMaterial);
    }
}