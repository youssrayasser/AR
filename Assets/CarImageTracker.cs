using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;

    // Track spawned prefabs by image name
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // When new images are detected
        foreach (var trackedImage in eventArgs.added)
        {
            string imageName = trackedImage.referenceImage.name;

            foreach (var arPrefab in ArPrefabs)
            {
                if (arPrefab.name == imageName && !spawnedPrefabs.ContainsKey(imageName))
                {
                    // Instantiate the prefab at the image's position and rotation
                    GameObject newPrefab = Instantiate(arPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    // Parent it to the tracked image
                    newPrefab.transform.parent = trackedImage.transform;
                    // Save to dictionary
                    spawnedPrefabs[imageName] = newPrefab;
                }
            }
        }

        // When images are updated (tracking state changed or moved)
        foreach (var trackedImage in eventArgs.updated)
        {
            string imageName = trackedImage.referenceImage.name;

            if (spawnedPrefabs.TryGetValue(imageName, out GameObject prefab))
            {
                prefab.SetActive(trackedImage.trackingState == TrackingState.Tracking);
                prefab.transform.position = trackedImage.transform.position;
                prefab.transform.rotation = trackedImage.transform.rotation;
            }
        }

        // When images are removed (no longer tracked)
        foreach (var trackedImage in eventArgs.removed)
        {
            string imageName = trackedImage.referenceImage.name;

            if (spawnedPrefabs.TryGetValue(imageName, out GameObject prefab))
            {
                Destroy(prefab);
                spawnedPrefabs.Remove(imageName);
            }
        }
    }


}
