using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(TagManager.TAG_PLAYER)) return;
        Debug.Log($"OnTriggerEnter");
        other.attachedRigidbody.AddExplosionForce(5f, Vector3.up, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag(TagManager.TAG_PLAYER)) return;
        Debug.Log($"OnCollisionEnter");
        other.rigidbody.AddExplosionForce(5f, Vector3.up, 5f);
    }
}
