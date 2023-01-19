using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class RollerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(2,20)] private float distance = 2;
    [SerializeField, Range(2,20)] private float offset = 2;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = target.position + (Vector3.up * distance) + (Vector3.back * offset);
        transform.rotation = Quaternion.LookRotation(target.transform.position);
    }
}
