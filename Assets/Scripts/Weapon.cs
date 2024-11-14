using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    BoxCollider triggerBox;

    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Damage Dealt");
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }
      
    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }
}