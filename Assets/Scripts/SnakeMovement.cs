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

    private void FixedUpdate() 
    {
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

        rb.velocity = moveDirection * moveSpeed;

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
void WrapAround(Vector3 offset)
    {
        Vector3 newHeadPosition = snakeHead.position + offset;

        snakeHead.position = newHeadPosition;

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