using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    private Transform target;
    private bool isDead = false;
    

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        GetComponent<Health>().onDeath += OnDeath;
    }

    
    void Update()
    {
        animator.SetFloat("Speed",navMeshAgent.velocity.magnitude);
        if (!isDead)
        {
            navMeshAgent.SetDestination(target.position);
        }

    }
    void OnDeath()
    {
        isDead = true;
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}
