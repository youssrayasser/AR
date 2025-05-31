using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class CarImageTracker : MonoBehaviour
{
    [Header("AR Components")]
    public ARTrackedImageManager trackedImageManager;

    [Header("Car Prefabs (match names exactly with image names)")]
    public GameObject Porsche;
    public GameObject Lamborghini;

    // Internal dictionary for managing spawned cars
    private Dictionary<string, GameObject> spawnedCars = new();

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void Start()
    {
        // Initialize and disable all prefabs at start
        if (Porsche != null)
        {
            var car = Instantiate(Porsche);
            car.name = "Porsche";
            car.SetActive(false);
            spawnedCars["Porsche"] = car;
        }

        if (Lamborghini != null)
        {
            var car = Instantiate(Lamborghini);
            car.name = "Lamborghini";
            car.SetActive(false);
            spawnedCars["Lamborghini"] = car;
        }
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Update existing tracked images
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            string imageName = trackedImage.referenceImage.name;

            if (spawnedCars.ContainsKey(imageName))
            {
                GameObject car = spawnedCars[imageName];

                if (trackedImage.trackingState == TrackingState.Tracking)
                {
                    car.SetActive(true);
                    car.transform.position = trackedImage.transform.position;
                    car.transform.rotation = trackedImage.transform.rotation;
                }
                else
                {
                    car.SetActive(false);
                }
            }
        }

        // Disable objects for removed images
        foreach (ARTrackedImage trackedImage in args.removed)
        {
            string imageName = trackedImage.referenceImage.name;
            if (spawnedCars.ContainsKey(imageName))
            {
                spawnedCars[imageName].SetActive(false);
            }
        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
            {
                foreach (var trackedImage in args.updated)
                {
                    Debug.Log($"Detected Image: {trackedImage.referenceImage.name} | Tracking State: {trackedImage.trackingState}");

                    if (spawnedCars.ContainsKey(trackedImage.referenceImage.name))
                    {
                        GameObject car = spawnedCars[trackedImage.referenceImage.name];

                        if (trackedImage.trackingState == TrackingState.Tracking)
                        {
                            car.SetActive(true);
                            car.transform.position = trackedImage.transform.position;
                            car.transform.rotation = trackedImage.transform.rotation;
                        }
                        else
                        {
                            car.SetActive(false);
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"No prefab assigned for image: {trackedImage.referenceImage.name}");
                    }
                }
            }

        }
    }
}
