using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidSpawner : MonoBehaviour
{
    public MiniDroid007 droidPrefab; // Assign this in the inspector
    public Transform droidSpawnPoint; // Assign the spawn point's transform here

    void Start()
    {
        // Call the SpawnDroid method and pass the position and rotation from the spawn point
        MiniDroid007 newDroid = MiniDroid007.SpawnDroid(droidSpawnPoint.position, droidSpawnPoint.rotation, droidPrefab);
        if (newDroid == null)
        {
            // Handle the case where the droid couldn't be placed on the NavMesh
        }
    }
}
