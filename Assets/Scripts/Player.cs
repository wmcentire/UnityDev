using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1,10)] public float speed = 5;
    public GameObject prefab;

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
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        //if (Input.GetKey(KeyCode.W)) direction.z = 1;
        //if (Input.GetKey(KeyCode.S)) direction.z = -1;
        //if (Input.GetKey(KeyCode.A)) direction.x = -1;
        //if (Input.GetKey(KeyCode.D)) direction.x = 1;
        transform.position += direction * speed * Time.deltaTime * 5;

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Gun Noise");
            GetComponent<AudioSource>().Play();
            GameObject go = Instantiate(prefab, transform.position, transform.rotation);
            Destroy(go, 5);
        }

    }
}
