using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Colidable
{
    [SerializeField] GameObject fx;
    // Start is called before the first frame update
    void Start()
    {
        onEnter += OnCoinPickup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCoinPickup(GameObject go) 
    {
        if (go.TryGetComponent<RollerPlayer>(out RollerPlayer player))
        {
            player.AddPoints(10);
        }
        Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
