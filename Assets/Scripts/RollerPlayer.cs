using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RollerPlayer : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private Transform view;
    private Vector3 force;
    private Rigidbody rb;
    [SerializeField] int magnitude = 2;
    [SerializeField] int jumpForce = 50;
    [SerializeField] private float groundRayLength = 1;
    [SerializeField] private LayerMask groundLayer;
    private bool touchGround = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        view = Camera.main.transform;
        Camera.main.GetComponent<RollerCamera>().SetTarget(transform);

        GetComponent<Health>().onDamage += OnDamage;
        GetComponent<Health>().onDeath += OnDeath;
        RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        force = view.rotation * (direction * magnitude);

        Ray ray = new Ray(transform.position, Vector3.down);
        bool onGround = Physics.Raycast(ray, groundRayLength, groundLayer);
        Debug.DrawRay(transform.position, ray.direction * groundRayLength);

        if (Input.GetButtonDown("Jump") && touchGround)
        {
            // touchGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
        }
        //implement grounded checker

    }

    private void FixedUpdate()
    {
        rb.AddForce(force);
    }

    public void AddPoints(int points)
    {
        score += points;
        RollerGameManager.Instance.SetScore(score);
    }

    public void OnDamage()
    {
        RollerGameManager.Instance.SetHealth((int)GetComponent<Health>().health);
    }

    public void OnDeath()
    {
        RollerGameManager.Instance.SetGameOver();
    }
}
