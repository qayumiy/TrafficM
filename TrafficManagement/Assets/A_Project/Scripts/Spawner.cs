using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   
    public List<GameObject> carPrefabs;           // y car types
    public List<Transform> PathGroup;           // z path containers
    public float spawnInterval = 3f;
    public int MaxSpawnNumber;
    
    private int count = 0;

    void Start()
    {
        InvokeRepeating("SpawnCar", 1f, spawnInterval);
    }

    void SpawnCar()
    {
        count++;
        if (carPrefabs.Count == 0 || PathGroup.Count == 0)
        {
            Debug.LogWarning("Spawner is missing configuration.");
            return;
        }
        GameObject prefab = carPrefabs[Random.Range(0, carPrefabs.Count)];
        Transform pathParent = PathGroup[Random.Range(0, PathGroup.Count)];      
        
        List<Transform> spawnGroup = new List<Transform>();
        for (int i = 0; i < pathParent.childCount; i++)
        {            
            spawnGroup.Add(pathParent.GetChild(i));
        }
        Transform spawn = spawnGroup[0];
        GameObject car = Instantiate(prefab, spawn.position, spawn.rotation);
        AITrafficCar ai = car.GetComponent<AITrafficCar>();
        ai.waypoints = new List<Transform>(spawnGroup);

        if (count >= MaxSpawnNumber)
        {
            CancelInvoke("SpawnCar");
        }
    }
}
