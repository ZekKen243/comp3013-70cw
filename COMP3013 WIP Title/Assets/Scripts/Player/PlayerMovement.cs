using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private CharacterStats statsManager;
    public float moveSpeed;
    public float rotationSpeed;


    private Rigidbody rigidBody;
    private Camera mainCamera;
    public Animator animator;


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


    void Update()
    {
        UpdateBodyRotation();
        UpdateBodyVelocity();

        if (rigidBody.velocity.magnitude > 0)
        {
            animator.SetTrigger("Walking");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
    private void UpdateBodyRotation()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            pointToLook.y = transform.position.y; // Keep the same height as the player

            transform.LookAt(pointToLook);

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
