using UnityEngine;
using Vuforia;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stagePrefabs;
    private int currentStageIndex = 0;
    private GameObject spawnedObject;

    // Reference to the content positioning behaviour from Vuforia
    public ContentPositioningBehaviour contentPositioningBehaviour;

    // Reference to the plane finder behaviour for ground plane detection
    public PlaneFinderBehaviour planeFinderBehaviour;

    private void Start()
    {
        // Set up callbacks for plane detection events
        planeFinderBehaviour.OnInteractiveHitTest.AddListener(OnPlaneDetected);
    }

    // This method will be called when the ground plane is detected
    private void OnPlaneDetected(HitTestResult result)
    {
        // Spawn the object for the current stage
        SpawnStageObject(result.Position);
    }

    // Method to spawn the object for the current stage
    private void SpawnStageObject(Vector3 position)
    {
        // Remove any previously spawned object
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }

        // Instantiate the new object at the detected position
        spawnedObject = Instantiate(stagePrefabs[currentStageIndex], position, Quaternion.identity);
    }

    // Method to move to the next stage
    public void NextStage()
    {
        // Increment the stage index and loop back if needed
        currentStageIndex = (currentStageIndex + 1) % stagePrefabs.Length;

        // Optionally, you could re-detect the ground and spawn a new object for the new stage
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);  // Remove the current object
            planeFinderBehaviour.PerformHitTest(Vector2.zero); // Trigger new ground detection
        }
    }
}
