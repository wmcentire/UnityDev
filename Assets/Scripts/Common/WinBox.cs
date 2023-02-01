using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBox : Interactable
{
    void Start()
    {
        GetComponent<CollisionEvent>().onEnter = OnInteract;
    }
    public override void OnInteract(GameObject target)
    {
        if (target.TryGetComponent<RollerPlayer>(out RollerPlayer player))
        {
            if (target.tag == "Player")
            {
                player.OnWin();
            }
        }
        else
        {
            

        }
        if (destroyOnInteract)
        {
            Destroy(gameObject);
        }
    }    
}
