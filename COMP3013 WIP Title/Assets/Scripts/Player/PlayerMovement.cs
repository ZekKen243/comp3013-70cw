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

    void Update()
    {
        OnDrawGizmos();
        UpdateBodyVelocity();
    }

    private void OnDrawGizmos()
    {
        if (!mainCamera)
        {
            Debug.LogError("mainCamera reference is null");
            return;
        }

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            pointToLook.y = transform.position.y; // Keep the same height as the player

            transform.LookAt(pointToLook);

            // Draw the raycast using Gizmos
            Gizmos.color = Color.red;
            Gizmos.DrawLine(cameraRay.origin, pointToLook);
        }
    }

    private void UpdateBodyVelocity()
    {
        if (!rigidBody)
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