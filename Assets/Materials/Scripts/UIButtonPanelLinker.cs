using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARCarController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject carSelectionPanel;
    public GameObject colorWheelPanel;

    [Header("Car Prefabs")]
    public GameObject porschePrefab;
    public GameObject lamborghiniPrefab;

    [Header("AR Placement")]
    public ARRaycastManager raycastManager;
    public Camera arCamera;

    [Header("Audio Clips")]
    public AudioClip porscheEngineSound;
    public AudioClip lamborghiniEngineSound;

    private GameObject currentCarInstance;
    private Renderer carBodyRenderer;
    private Renderer wheelRenderer;
    private AudioSource engineAudioSource;
    private string currentCar = "Porsche";

    public void ShowCarSelectionPanel()
    {
        carSelectionPanel.SetActive(true);
        colorWheelPanel.SetActive(false);
    }

    public void ShowColorWheelPanel()
    {
        colorWheelPanel.SetActive(true);
        carSelectionPanel.SetActive(false);
    }

    public void SelectCar(string carName)
    {
        carSelectionPanel.SetActive(false);

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (currentCarInstance != null)
                Destroy(currentCarInstance);

            GameObject selectedPrefab = (carName == "Porsche") ? porschePrefab : lamborghiniPrefab;
            currentCarInstance = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);

            AssignCarComponents(currentCarInstance);
            currentCar = carName;
            SetEngineSound();
        }
    }

    void AssignCarComponents(GameObject car)
    {
        // Assume the first child has the car mesh and wheels
        carBodyRenderer = car.transform.Find("Body")?.GetComponent<Renderer>();
        wheelRenderer = car.transform.Find("Wheels")?.GetComponent<Renderer>();

        engineAudioSource = car.GetComponent<AudioSource>();
        if (engineAudioSource == null)
        {
            engineAudioSource = car.AddComponent<AudioSource>();
        }
        engineAudioSource.loop = true;
    }

    void SetEngineSound()
    {
        if (engineAudioSource != null)
        {
            engineAudioSource.clip = (currentCar == "Porsche") ? porscheEngineSound : lamborghiniEngineSound;
        }
    }

    public void ToggleEngineSound()
    {
        if (engineAudioSource == null || engineAudioSource.clip == null) return;

        if (engineAudioSource.isPlaying)
            engineAudioSource.Stop();
        else
            engineAudioSource.Play();
    }

    public void OnColorWheelClick(Image clickedColorImage)
    {
        Color selectedColor = clickedColorImage.color;

        if (carBodyRenderer != null)
            carBodyRenderer.material.color = selectedColor;

        if (wheelRenderer != null)
            wheelRenderer.material.color = selectedColor;
    }
}
