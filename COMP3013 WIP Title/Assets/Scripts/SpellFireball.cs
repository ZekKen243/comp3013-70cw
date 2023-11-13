using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellFireball : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        HandleShootInput();
    }

    void HandleShootInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Cast
            Debug.Log("Casting!");
            PlayerCast.Instance.Shoot();
        }
    }
}
