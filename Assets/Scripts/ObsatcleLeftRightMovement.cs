using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsatcleLeftRightMovement : MonoBehaviour
{
    private float speed = 1f;
    private float upDownDistance = 2f;
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
        if (gameManager.gameIsActive)
        {
            float newX = startPosition.x + Mathf.Sin(Time.time * speed) * upDownDistance;
            transform.position = new Vector3(newX, startPosition.y, startPosition.z);
        }
    }
}
