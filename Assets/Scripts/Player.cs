using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1,20)] public float speed = 5;
    [Range(1,90)]public float rotationRate = 60;
    public GameObject prefab;
    public Transform bOrigin;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
    }

    private void Awake()
    {
        Debug.Log("awake");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(2, 2, 2);
        //transform.rotation = Quaternion.Euler(20f, 40f, 10f);
        //transform.localScale = Vector3.one * Random.value * 5;
        //if (Input.GetKey(KeyCode.W)) direction.z = 1;
        //if (Input.GetKey(KeyCode.S)) direction.z = -1;
        //if (Input.GetKey(KeyCode.A)) direction.x = -1;
        //if (Input.GetKey(KeyCode.D)) direction.x = 1;



        Vector3 direction = Vector3.zero;

        //direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");

        Quaternion rotate = Quaternion.Euler(rotation * rotationRate * Time.deltaTime);
        transform.rotation = transform.rotation * rotate;

        transform.Translate(direction * speed * Time.deltaTime);
        //transform.position += direction * speed * Time.deltaTime * 5;

        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Gun Noise");
            GameObject go = Instantiate(prefab, bOrigin.position, bOrigin.rotation);
        }

    }
}
