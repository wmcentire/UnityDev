using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colidable : MonoBehaviour
{
    [SerializeField] private string hitTagName = string.Empty;

    public delegate void CollisionEvent(GameObject other);
    public CollisionEvent onEnter;
    public CollisionEvent onExit;
    public CollisionEvent onStay;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
