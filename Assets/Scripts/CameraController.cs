using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform transformToFollow;



    private void Start()
    {
        transformToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transformToFollow.position.x, transformToFollow.position.y, -10);
    }
}