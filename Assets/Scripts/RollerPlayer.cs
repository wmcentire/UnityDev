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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        force = view.rotation * (direction * magnitude);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            view = Camera.main.transform;
            Camera.main.GetComponent<RollerCamera>().SetTarget(transform);
        }

        RollerGameManager.Instance.SetHealth(50);
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
}
