using System;
using System.Collections;
using System.Collections.Generic;
using Model.Player;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var owner = other.gameObject.GetComponent<PlayerModel>();
        if (other != null)
        {
            owner.PickUpItem(this.gameObject);
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<PickUpItem>());
        }
    }
}
