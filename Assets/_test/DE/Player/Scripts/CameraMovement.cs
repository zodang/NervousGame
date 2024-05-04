using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 _cameraOffset;
    private void Start()
    {
        _cameraOffset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        transform.position = player.position + _cameraOffset;
    }
}
