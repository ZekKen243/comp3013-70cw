using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private CharacterStats statsManager;
    public float moveSpeed;


    private Rigidbody rigidBody;
    private Camera mainCamera;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        statsManager = player.GetComponent<CharacterStats>();
        moveSpeed = statsManager.movementSpeed;
        InitReferences();
    }

    private void InitReferences()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }


    void FixedUpdate()
    {
        UpdateBodyRotation();
        UpdateBodyVelocity();
    }

    private void UpdateBodyRotation()
    {
        if(!mainCamera)
        {
            Debug.LogError("mainCamera reference is null");
            return;
        }

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if (groundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void UpdateBodyVelocity()
    {
        if(!rigidBody)
        {
            Debug.LogError("rigidBody reference is null");
            return;
        }

        rigidBody.velocity = GetMoveVelocity();
    }


    private Vector3 GetMoveVelocity()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        return moveInput * moveSpeed;
    }
}
