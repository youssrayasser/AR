using UnityEngine;
using UnityEngine.UI;

public class CarSelectionManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject porschePrefab, lamborghiniPrefab;
    private GameObject currentCar;

    [Header("Panels")]
    public GameObject carSelectionPanel;
    public GameObject colorPanel;
    public GameObject enginePanel;

    [Header("Engine Toggles")]
    public Toggle porscheToggle;
    public Toggle lamborghiniToggle;

    [Header("Voice Audio")]
    public AudioClip porscheVoiceClip;
    public AudioClip lamborghiniVoiceClip;

    private AudioSource voiceAudioSource;

    void Start()
    {
        // Create an AudioSource for voice playback
        voiceAudioSource = gameObject.AddComponent<AudioSource>();
        voiceAudioSource.playOnAwake = false;

        // Hide all panels initially
        carSelectionPanel.SetActive(false);
        colorPanel.SetActive(false);
        enginePanel.SetActive(false);

        // Reset toggles
        porscheToggle.isOn = false;
        lamborghiniToggle.isOn = false;
    }

    public void ShowCarSelectionPanel()
    {
        carSelectionPanel.SetActive(true);
        colorPanel.SetActive(false);
        enginePanel.SetActive(false);
    }

    public void ShowColorPanel()
    {
        carSelectionPanel.SetActive(false);
        colorPanel.SetActive(true);
        enginePanel.SetActive(false);
    }

    public void ShowEnginePanel()
    {
        carSelectionPanel.SetActive(false);
        colorPanel.SetActive(false);
        enginePanel.SetActive(true);
    }

    public void SelectCar(string carName)
    {
        if (currentCar != null)
            Destroy(currentCar);

        switch (carName)
        {
            case "Porsche":
                currentCar = Instantiate(porschePrefab, Vector3.zero, Quaternion.identity);
                SetupEngine("Porsche");
                break;

            case "Lamborghini":
                currentCar = Instantiate(lamborghiniPrefab, Vector3.zero, Quaternion.identity);
                SetupEngine("Lamborghini");
                break;
        }

        carSelectionPanel.SetActive(false);
    }

    private void SetupEngine(string carName)
    {
        // Show only relevant toggle
        porscheToggle.gameObject.SetActive(carName == "Porsche");
        lamborghiniToggle.gameObject.SetActive(carName == "Lamborghini");

        // Reset toggles
        porscheToggle.isOn = false;
        lamborghiniToggle.isOn = false;

        // Stop any playing voice clip
        if (voiceAudioSource.isPlaying) voiceAudioSource.Stop();
    }

    // Toggle voice clip playback when toggles change
    public void ToggleEngineSound(bool isOn, string carName)
    {
        if (!isOn)
        {
            if (voiceAudioSource.isPlaying)
                voiceAudioSource.Stop();
            return;
        }

        if (carName == "Porsche")
        {
            voiceAudioSource.clip = porscheVoiceClip;
            voiceAudioSource.Play();
        }
        else if (carName == "Lamborghini")
        {
            voiceAudioSource.clip = lamborghiniVoiceClip;
            voiceAudioSource.Play();
        }
    }

    public void OnPorscheEngineToggleChanged(bool isOn)
    {
        ToggleEngineSound(isOn, "Porsche");
    }

    public void OnLamborghiniEngineToggleChanged(bool isOn)
    {
        ToggleEngineSound(isOn, "Lamborghini");
    }

    public void ChangeCarColor(Color newColor)
    {
        if (currentCar == null)
        {
            Debug.LogWarning("No car selected!");
            return;
        }

        Renderer[] renderers = currentCar.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            if (r.gameObject.name.Contains("Body") || r.gameObject.tag == "CarBody")
            {
                r.material.color = newColor;
            }
        }
    }

    public void PlayCarVoice()
    {
        if (currentCar == null || voiceAudioSource == null) return;

        if (currentCar.name.Contains("Porsche"))
            voiceAudioSource.clip = porscheVoiceClip;
        else if (currentCar.name.Contains("Lamborghini"))
            voiceAudioSource.clip = lamborghiniVoiceClip;
        else return;

        voiceAudioSource.Play();
    }
}
