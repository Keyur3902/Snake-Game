using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsatcleLeftRightMovement : MonoBehaviour
{
    private float speed = 1f; // Speed of the movement
    private float upDownDistance = 2f; // Distance to move up and down
    private Vector3 startPosition;
    private GameManager gameManager;

    void Start()
    {
        // Save the initial position of the object
        startPosition = transform.position;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Calculate the new position based on the sine wave
        if (gameManager.gameIsActive)
        {
            float newX = startPosition.x + Mathf.Sin(Time.time * speed) * upDownDistance;
            transform.position = new Vector3(newX, startPosition.y, startPosition.z);
        }
    }
}
