using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Road : MonoBehaviour
{
    [SerializeField] private List<GameObject> roads;

    private GameObject _newRoad;

    public void SpawnRoad(Collider collider)
    {
        int randInt = Random.Range(0, roads.Count);
        
        _newRoad = Instantiate(roads[randInt], gameObject.transform);
        Transform prevObject = collider.transform.parent;
        _newRoad.transform.position = prevObject.position + Vector3.forward * prevObject.localScale.z;
    }

    [SerializeField] private float swayDegree;
    [SerializeField] private Timer _timer;
    [SerializeField] private float swayAmount;
    [SerializeField] private float swaySpeed;
    private int _swayStep;
    
    private void Start()
    {
        _timer.OnStateChangedAction += ChangeRoadMovement;
    }

    private void Update()
    {
        swayDegree = Mathf.Sin(Time.time * swaySpeed) * swayAmount * _swayStep;
        transform.Translate(Vector3.right * (Time.deltaTime * swayDegree));
    }
    
    private void ChangeRoadMovement(int state)
    {
        _swayStep = state;
    }
}
