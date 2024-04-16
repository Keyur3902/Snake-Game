// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
// using TMPro;

// public class SnakeMovement : MonoBehaviour
// {
//     private float moveSpeed = 5.0f;
//     public bool isPowerUp = false;
//     public GameObject powerUpPrefab;
//     public AudioSource snakeBite;
//     public AudioSource powerUp;
//     public AudioSource monster;
//     public AudioSource gameOver;
//     private Vector3 moveDirection;
//     private GameManager gameManager;
//     private bool touchStarted = false;
//     private Vector2 touchStartPos;
//     public Rigidbody2D rb;
//     private List<Transform> _snakeSpawn;
//     public Transform snakePrefab;
//     public TextMeshProUGUI timerText;
//     private float powerUpDuration = 10f; 
//     private float currentTimer;

//     void Start()
//     {
//         gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

//         rb = GetComponent<Rigidbody2D>();
//         rb.velocity = new Vector2(moveSpeed, 0);

//         _snakeSpawn = new List<Transform>();
//         _snakeSpawn.Add(this.transform);
//     }

//     void Update()
//     {

//         if (Input.touchCount > 0 && gameManager.gameIsActive)
//         {
//             Touch touch = Input.GetTouch(0);

//             // Check if the touch phase is moved
//             if (touch.phase == TouchPhase.Began)
//             {
//                 // Record the starting position of the touch
//                 touchStartPos = touch.position;
//                 touchStarted = true;
//             }
//             else if (touch.phase == TouchPhase.Moved && touchStarted)
//             {
//                 // Calculate the swipe direction based on the difference between the current touch position and the starting position
//                 Vector2 swipeDelta = touch.position - touchStartPos;

//                 // Check if the swipe delta is greater in the horizontal direction
//                 if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
//                 {
//                     // Set the movement direction horizontally
//                     moveDirection = (swipeDelta.x > 0) ? Vector2.right : Vector2.left;
//                 }
//                 else
//                 {
//                     // Set the movement direction vertically
//                     moveDirection = (swipeDelta.y > 0) ? Vector2.up : Vector2.down;
//                 }
//             }
//         }
//         else if (!touchStarted && gameManager.gameIsActive)
//         {
//             // Check for arrow key input if touch input hasn't started
//             if (Input.GetKeyDown(KeyCode.UpArrow))
//             {
//                 moveDirection = Vector2.up;
//             }
//             else if (Input.GetKeyDown(KeyCode.DownArrow))
//             {
//                 moveDirection = Vector2.down;
//             }
//             else if (Input.GetKeyDown(KeyCode.LeftArrow))
//             {
//                 moveDirection = Vector2.left;
//             }
//             else if (Input.GetKeyDown(KeyCode.RightArrow))
//             {
//                 moveDirection = Vector2.right;
//             }
//         }

//         else
//         {
//             // Check for the first touch input
//             if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
//             {
//                 touchStarted = true; // Set the flag to true to indicate touch input has started
//             }
//         }

//     }

//     private void FixedUpdate()
//     {
//         // Move the head of the snake
//         rb.velocity = moveDirection * moveSpeed;

//         // Update positions using smooth lerp interpolation
//         float lerpSpeed = 14f; // Adjust this value for desired smoothness
//         for (int i = 1; i < _snakeSpawn.Count; i++)
//         {
//             Vector3 targetPosition = _snakeSpawn[i - 1].position;
//             _snakeSpawn[i].position = Vector3.Lerp(_snakeSpawn[i].position, targetPosition, lerpSpeed * Time.fixedDeltaTime);
//         }
//     }



//     private void Grow()
//     {
//         Transform snakeSpawn = Instantiate(this.snakePrefab);
//         snakeSpawn.position = _snakeSpawn[_snakeSpawn.Count - 1].position;

//         _snakeSpawn.Add(snakeSpawn);
//     }


//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Food"))
//         {
//             Destroy(collision.gameObject);
//             FindObjectOfType<GameManager>().UpdateScore(1);
//             Grow();
//             snakeBite.Play();
//             FindObjectOfType<GameManager>().SpawnTarget();
//         }
//         if (collision.CompareTag("PowerUp"))
//         {
//             isPowerUp = true;
//             StartCoroutine(IsPowerUp());
//             powerUp.Play();
//             Destroy(collision.gameObject);
//             FindObjectOfType<FoodManager>().StartSpawn();
//             StartCoroutine(StartPowerUpTimer());
//         }

//         // if(collision.CompareTag("SnakeBody") && isPowerUp == false){
//         //     gameManager.GameOver();
//         // }
//     }


//     IEnumerator IsPowerUp()
//     {
//         yield return new WaitForSeconds(10);
//         isPowerUp = false;
//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.tag == "Wall" && isPowerUp == false)
//         {
//             gameOver.Play();
//             rb.velocity = Vector2.zero;
//             gameManager.GameOver();
//         }
//         if (collision.gameObject.tag == "Obstacle" && isPowerUp == false)
//         {
//             monster.Play();
//             rb.velocity = Vector2.zero;
//             gameManager.GameOver();
//         }
//     }

//      IEnumerator StartPowerUpTimer()
//     {
//         currentTimer = powerUpDuration;
//         while (currentTimer > 0)
//         {
//             timerText.text = "Power-Up: " + currentTimer.ToString("F1");
//             yield return new WaitForSeconds(1f); // Update the timer every second
//             currentTimer -= 1f;
//         }
//         timerText.text = ""; // Clear the timer text when power-up expires
//         isPowerUp = false; // Reset the power-up state
//     }
// }


using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SnakeMovement : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    public bool isPowerUp = false;
    public AudioSource powerUp;
    public AudioSource monster;
    public AudioSource gameOver;
    private Transform snakeHead;
    public AudioSource snakeBite;
    private GameManager gameManager;
    private float powerUpDuration = 10f;
    private List<Transform> _snakeSpawn;
    public Transform snakePrefab;
    private bool touchStarted = false;
    private Vector2 touchStartPos;
    private float currentTimer;
    public TextMeshProUGUI timerText;
    private Vector2 screenBounds;
    private Vector3 moveDirection;
    private Camera mainCamera;

    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        snakeHead = transform;
        rb.velocity = new Vector2(moveSpeed, 0);
        _snakeSpawn = new List<Transform>();
        _snakeSpawn.Add(this.transform);
        mainCamera = Camera.main;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));        
    }

    void Update()
    {

        if (Input.touchCount > 0 && gameManager.gameIsActive)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                touchStarted = true;
            }
            else if (touch.phase == TouchPhase.Moved && touchStarted)
            {
                Vector2 swipeDelta = touch.position - touchStartPos;

                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    moveDirection = (swipeDelta.x > 0) ? Vector2.right : Vector2.left;
                }
                else
                {
                    moveDirection = (swipeDelta.y > 0) ? Vector2.up : Vector2.down;
                }
            }
        }
        else if (!touchStarted && gameManager.gameIsActive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveDirection = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDirection = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDirection = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveDirection = Vector2.right;
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchStarted = true;
            }
        }
    }

   

    // private void FixedUpdate() 
    // {
    //     if (snakeHead.position.x > screenBounds.x)
    //     {
    //         WrapAround(Vector3.left * screenBounds.x * 2f);
    //     }
    //     else if (snakeHead.position.x < -screenBounds.x)
    //     {
    //         WrapAround(Vector3.right * screenBounds.x * 2f);
    //     }
    //     else if (snakeHead.position.y > screenBounds.y)
    //     {
    //         WrapAround(Vector3.down * screenBounds.y * 2f);
    //     }
    //     else if (snakeHead.position.y < -screenBounds.y)
    //     {
    //         WrapAround(Vector3.up * screenBounds.y * 2f);
    //     }

    //     rb.velocity = moveDirection * moveSpeed;
    //     float lerpSpeed = 14f;

    //     for (int i = _snakeSpawn.Count - 1; i > 0; i--)
    //     {
    //         Vector3 targetPosition = _snakeSpawn[i - 1].position;
    //         _snakeSpawn[i].position = Vector3.Lerp(_snakeSpawn[i].position, targetPosition, lerpSpeed * Time.fixedDeltaTime);
    //     }

    // }

    private void FixedUpdate() 
    {
        // Wrap around the walls if the snake's head reaches the boundary
        if (snakeHead.position.x > screenBounds.x)
        {
            WrapAround(Vector3.left * screenBounds.x * 2f);
        }
        else if (snakeHead.position.x < -screenBounds.x)
        {
            WrapAround(Vector3.right * screenBounds.x * 2f);
        }
        else if (snakeHead.position.y > screenBounds.y)
        {
            WrapAround(Vector3.down * screenBounds.y * 2f);
        }
        else if (snakeHead.position.y < -screenBounds.y)
        {
            WrapAround(Vector3.up * screenBounds.y * 2f);
        }

        // Move the snake's head
        rb.velocity = moveDirection * moveSpeed;

        // Update positions for each segment of the snake's body
        float lerpSpeed = 14f;
        for (int i = _snakeSpawn.Count - 1; i > 0; i--)
        {
            Vector3 targetPosition = _snakeSpawn[i - 1].position;
            _snakeSpawn[i].position = Vector3.Lerp(_snakeSpawn[i].position, targetPosition, lerpSpeed * Time.fixedDeltaTime);
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            gameManager.UpdateScore(1);
            Grow();
            snakeBite.Play();
            // gameManager.SpawnTarget();
        }
        if (collision.CompareTag("PowerUp"))
        {
            isPowerUp = true;
            StartCoroutine(IsPowerUp());
            powerUp.Play();
            Destroy(collision.gameObject);
            FindObjectOfType<FoodManager>().StartSpawn();
            StartCoroutine(StartPowerUpTimer());
        }
    }

    IEnumerator IsPowerUp()
    {
        yield return new WaitForSeconds(10);
        isPowerUp = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Wall" && isPowerUp == false)
        // {
        //     gameOver.Play();
        //     rb.velocity = Vector2.zero;
        //     gameManager.GameOver();
        // }
        if (collision.gameObject.tag == "Obstacle" && isPowerUp == false)
        {
            monster.Play();
            rb.velocity = Vector2.zero;
            gameManager.GameOver();
        }
    }

    IEnumerator StartPowerUpTimer()
    {
        currentTimer = powerUpDuration;
        while (currentTimer > 0)
        {
            timerText.text = "Power-Up: " + currentTimer.ToString("F1");
            yield return new WaitForSeconds(1f);
            currentTimer -= 1f;
        }
        timerText.text = "";
        isPowerUp = false;
    }

    // void WrapAround(Vector3 offset)
    // {
    //     transform.position += offset;

    //     for (int i = 1; i < _snakeSpawn.Count; i++)
    //     {
    //         _snakeSpawn[i].position += offset;
    //     }
    // }

//     void WrapAround(Vector3 offset)
// {
//     // Calculate the position after wrapping for the snake head
//     Vector3 newHeadPosition = snakeHead.position + offset;
//     if (newHeadPosition.x > screenBounds.x)
//     {
//         newHeadPosition.x = -screenBounds.x;
//     }
//     else if (newHeadPosition.x < -screenBounds.x)
//     {
//         newHeadPosition.x = screenBounds.x;
//     }
//     if (newHeadPosition.y > screenBounds.y)
//     {
//         newHeadPosition.y = -screenBounds.y;
//     }
//     else if (newHeadPosition.y < -screenBounds.y)
//     {
//         newHeadPosition.y = screenBounds.y;
//     }

//     // Calculate the offset for the snake head
//     Vector3 headOffset = newHeadPosition - snakeHead.position;

//     // Move the snake head to the wrapped position
//     snakeHead.position = newHeadPosition;

//     // Update positions for each snake segment
//     for (int i = _snakeSpawn.Count - 1; i > 0; i--)
//     {
//         // Calculate the position after wrapping for the current segment
//         Vector3 newSegmentPosition = _snakeSpawn[i - 1].position;

//         // Apply the same offset as the snake head to the current segment
//         newSegmentPosition += headOffset;

//         // Move the segment to the wrapped position
//         _snakeSpawn[i].position = newSegmentPosition + (2 * headOffset);
//     }
// }

void WrapAround(Vector3 offset)
    {
        // Calculate the new position after wrapping for the snake's head
        Vector3 newHeadPosition = snakeHead.position + offset;

        // Move the snake's head to the new wrapped position
        snakeHead.position = newHeadPosition;

        // Update positions for each segment of the snake's body
        for (int i = _snakeSpawn.Count - 1; i > 0; i--)
        {
            Vector3 newSegmentPosition = _snakeSpawn[i - 1].position;
            newSegmentPosition += offset;
            _snakeSpawn[i].position = newSegmentPosition + (2 * offset);
        }
    }

    private void Grow()
    {
        Transform snakeSpawn = Instantiate(this.snakePrefab);
        snakeSpawn.position = _snakeSpawn[_snakeSpawn.Count - 1].position;
        _snakeSpawn.Add(snakeSpawn);
    }
}