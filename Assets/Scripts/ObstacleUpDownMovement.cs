using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUpDownMovement : MonoBehaviour
{
    private float speed = 1f;
    private float upDownDistance = 2f;
    private Vector3 startPosition;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (gameManager.gameIsActive)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * speed) * upDownDistance;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
    }
}
