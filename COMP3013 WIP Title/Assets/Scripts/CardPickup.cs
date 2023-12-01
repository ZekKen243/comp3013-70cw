using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickup : MonoBehaviour
{
    public SpellFireball castScript;
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        castScript.enabled = true;
    }
}
