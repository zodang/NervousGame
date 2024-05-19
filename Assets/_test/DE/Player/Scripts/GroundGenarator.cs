using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject[] groundPrefabs; // Array to hold different ground prefabs
    public Transform player; // Reference to the player transform
    public float spawnDistance = 20f; // Distance ahead of the player to spawn new segments
    public int initialSegments = 5; // Number of initial ground segments
    public Vector3 startPosition = new Vector3(0, -10, 15); // Starting position for the first ground segment
    private List<GameObject> activeSegments = new List<GameObject>(); // List to track active ground segments
    private Vector3 lastEndPosition; // Position to spawn the next segment

    void Start()
    {
        lastEndPosition = startPosition; // Set the initial last end position

        // Initialize the ground with initial segments
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnGroundSegment();
        }
    }

    void Update()
    {
        // Check if we need to spawn new segments
        if (player.position.z + spawnDistance > lastEndPosition.z)
        {
            SpawnGroundSegment();
            RemoveOldSegment();
        }
    }

    void SpawnGroundSegment()
    {
        // Select a random ground prefab
        GameObject groundPrefab = groundPrefabs[Random.Range(0, groundPrefabs.Length)];
        
        // Get the length of the prefab (assuming length along z-axis)
        float segmentLength = groundPrefab.GetComponent<Renderer>().bounds.size.z;
        
        // Instantiate the ground segment at the last end position
        GameObject segment = Instantiate(groundPrefab, lastEndPosition, groundPrefab.transform.rotation);
        
        activeSegments.Add(segment);
        
        // Update the last end position for the next segment
        lastEndPosition = segment.transform.position + new Vector3(0, 0, segmentLength);
    }

    void RemoveOldSegment()
    {
        // Remove the oldest ground segment to save memory
        if (activeSegments.Count > initialSegments)
        {
            Destroy(activeSegments[0]);
            activeSegments.RemoveAt(0);
            
        }
    }
}
