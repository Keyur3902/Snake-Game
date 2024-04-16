using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUpDownMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 1f; // Speed of the movement
    private float upDownDistance = 2f; // Distance to move up and down
    private Vector3 startPosition;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Save the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position based on the sine wave
        if (gameManager.gameIsActive)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * speed) * upDownDistance;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }
}
