using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class RollerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(2,20)] private float distance = 10;
    [SerializeField, Range(20,80)] private float pitch = 10;
    [SerializeField, Range(0.1f,5)] private float sensitivity = 1;

    private float yaaaas = 0;


    public void SetTarget(Transform transform)
    {
        this.target = transform;
        yaaaas = target.rotation.eulerAngles.y;
    }

    private void LateUpdate()
    {
        if(target==null) return;
        yaaaas += Input.GetAxis("Mouse X") * sensitivity;

        Quaternion qYaw = Quaternion.AngleAxis(yaaaas, Vector3.up);
        Quaternion qPitch = Quaternion.AngleAxis(pitch, Vector3.right);

        Quaternion rotation = qYaw * qPitch;

        Vector3 offset = rotation * Vector3.back * distance;

        transform.position = target.position + offset;
        transform.rotation = Quaternion.LookRotation(-offset);
    }
}
