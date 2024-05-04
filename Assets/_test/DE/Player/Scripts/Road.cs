using System.Collections.Generic;
using UnityEngine;
public class Road : MonoBehaviour
{
    [SerializeField] private List<GameObject> roads;
    [SerializeField] private Transform player;

    public void SpawnRoad(Collider collider)
    {
        int randInt = Random.Range(0, roads.Count);
        
        GameObject newRoad = Instantiate(roads[randInt]);
        Transform prevObject = collider.transform.parent;
        newRoad.transform.position = prevObject.position + Vector3.forward * prevObject.localScale.z;
    }
    
}
