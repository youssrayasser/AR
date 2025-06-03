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

    [Header("Engine Audio")]
    public AudioSource porscheEngineAudio;
    public AudioSource lamborghiniEngineAudio;

    [Header("Engine Toggles")]
    public Toggle porscheToggle;
    public Toggle lamborghiniToggle;

    // Show the car selection panel
    public void ShowCarSelectionPanel()
    {
        carSelectionPanel.SetActive(true);
        colorPanel.SetActive(false);
        enginePanel.SetActive(false);
    }

    // Show the color panel
    public void ShowColorPanel()
    {
        carSelectionPanel.SetActive(false);
        colorPanel.SetActive(true);
        enginePanel.SetActive(false);
    }

    // Show the engine panel
    public void ShowEnginePanel()
    {
        carSelectionPanel.SetActive(false);
        colorPanel.SetActive(false);
        enginePanel.SetActive(true);
    }

    // Called by clicking on car image (Porsche or Lamborghini)
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

        // Hide car selection panel after choosing
        carSelectionPanel.SetActive(false);
    }

    // Setup engine toggles for selected car
    private void SetupEngine(string carName)
    {
        enginePanel.SetActive(true);

        // Show only the selected car's toggle
        porscheToggle.gameObject.SetActive(carName == "Porsche");
        lamborghiniToggle.gameObject.SetActive(carName == "Lamborghini");

        // Turn both toggles off
        porscheToggle.isOn = false;
        lamborghiniToggle.isOn = false;
    }

    // Called by toggles
    public void ToggleEngineSound(bool isOn, string carName)
    {
        if (carName == "Porsche" && porscheEngineAudio != null)
        {
            if (isOn) porscheEngineAudio.Play();
            else porscheEngineAudio.Stop();
        }
        else if (carName == "Lamborghini" && lamborghiniEngineAudio != null)
        {
            if (isOn) lamborghiniEngineAudio.Play();
            else lamborghiniEngineAudio.Stop();
        }
    }

    // Called by color buttons with Color values
    public void ChangeCarColor(Color newColor)
    {
        if (currentCar != null)
        {
            Renderer[] renderers = currentCar.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.material.color = newColor;
            }
        }
    }
}
