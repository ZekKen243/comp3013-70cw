using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameEntity gameEntity;
    private Rigidbody rigidBody;
    private Camera mainCamera;

    public int moveSpeed
    {
        get
        {
            return gameEntity.stats.moveSpeed;
        }
    }

    void Start()
    {
        gameEntity = gameObject.GetComponent<GameEntity>();
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
