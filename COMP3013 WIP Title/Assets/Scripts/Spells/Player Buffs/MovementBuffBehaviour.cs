using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovementBuffBehaviour : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;
    public float speedIncrease;
    public float buffDuration;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        StartCoroutine(TemporaryHealthIncrease(speedIncrease, buffDuration));
    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.E))
    //    {
    //        StartCoroutine(TemporaryHealthIncrease(speedIncrease, buffDuration)); //debug code
    //    }
    //}

    private IEnumerator TemporaryHealthIncrease(float moveSpeedIncrease, float buffDuration)
    {
        float originalSpeed = playerMovement.moveSpeed;

        playerMovement.moveSpeed += moveSpeedIncrease;

        yield return new WaitForSeconds(buffDuration);

        playerMovement.moveSpeed = originalSpeed;

        //Destroy(gameObject);
    }
}