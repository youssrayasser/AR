using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMarkerlessScene()
    {
        SceneManager.LoadScene("ARMarkerless"); // Replace with your actual scene name
    }

    public void LoadMarkerBasedScene()
    {
        SceneManager.LoadScene("ARMarkerBased"); // Replace with your actual scene name
    }
}
