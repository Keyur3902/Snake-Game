// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using Unity.VisualScripting;
// using UnityEngine.SocialPlatforms.Impl;

// public class Snake : MonoBehaviour {

//     private enum Direction {
//         Left,
//         Right,
//         Up,
//         Down
//     }

//     private enum State { 
//         Alive,
//         Dead
//     }   

//     private State state;
//     private Direction gridMoveDirection;
//     private Vector3 gridPosition;
//     private float gridMoveTimer;
//     private float gridMoveTimerMax;
//     private LevelGrid levelGrid;
//     private int snakeBodySize;
//     private List<SnakeMovePosition> snakeMovePositionList;
//     private List<SnakeBodyPart> snakeBodyPartList;
//     private GameManager gameManager;
//     public AudioSource powerUp;
//     public AudioSource monster;
//     public AudioSource gameOver;
//     public AudioSource snakeBite;
//     public bool isPowerUp = false;
//     private float powerUpDuration = 10f;
//     private float currentTimer; 
//     public TextMeshProUGUI timerText;
//     private bool touchStarted = false;
//     private Vector2 touchStartPos;
//     private bool isMode;
//     public float speed;
//     public Sprite normalSprite;
//     public Sprite nearFoodSprite;
//     public Sprite gameOverSprite;
//     private SpriteRenderer snakeHeadSpriteRenderer;

//     public void Setup(LevelGrid levelGrid) {
//         this.levelGrid = levelGrid;
//     }

//     private void Awake() {
//         snakeHeadSpriteRenderer = GetComponent<SpriteRenderer>();
//         gridPosition = new Vector3(0, 0);
//         gridMoveTimerMax = .1f;
//         gridMoveTimer = gridMoveTimerMax;
//         gridMoveDirection = Direction.Right;

//         snakeMovePositionList = new List<SnakeMovePosition>();
//         snakeBodySize = 0;

//         snakeBodyPartList = new List<SnakeBodyPart>();

//         state = State.Alive;
//     }

//     private bool intToBool(int intValue)
//     {
//         return intValue == 1 ? true : false;
//     }

//     private void Start() {
//         speed = 1f;
//         gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
//         isMode = intToBool(PlayerPrefs.GetInt("isMode"));
//     }
//     private void Update() {
//         switch (state) {
//         case State.Alive:
//             HandleInput();
//             MobileInput();
//             HandleGridMovement();
//             CheckProximityToFood();
//             CheckProximityToPowerUp();
//             break;
//         case State.Dead:
//             CheckProximityToGameOver();
//             break;
//         }
//     }
//     private void CheckProximityToFood() {
//         Vector3 foodPosition = levelGrid.foodGridPosition;
//         float distanceToFood = Vector3.Distance(gridPosition, foodPosition);
//         if (distanceToFood < 1.5f) {
//             ChangeSprite(nearFoodSprite);
//         } else {
//             ChangeSprite(normalSprite);
//         }
//     }
//     private void CheckProximityToPowerUp() {
//         Vector3 powerUpPosition = FindObjectOfType<FoodManager>().powerUpPosition;
//         float distanceToFood = Vector3.Distance(gridPosition, powerUpPosition);
//         if (distanceToFood < 1.5f && FindObjectOfType<FoodManager>().isSpawning) {
//             ChangeSprite(nearFoodSprite);
//         }
//     }
//     private void CheckProximityToGameOver() {
//         if (state == State.Dead) {
//             ChangeSprite(gameOverSprite);
//         } else {
//             ChangeSprite(normalSprite);
//         }
//     }

//     private void ChangeSprite(Sprite sprite) {
//         snakeHeadSpriteRenderer.sprite = sprite;
//     }

//     private void HandleInput() {
    
//         if (Input.GetKeyDown(KeyCode.UpArrow)) {
//             if (gridMoveDirection != Direction.Down) {
//                 gridMoveDirection = Direction.Up;
//             }
//         }
//         if (Input.GetKeyDown(KeyCode.DownArrow)) {
//             if (gridMoveDirection != Direction.Up) {
//                 gridMoveDirection = Direction.Down;
//             }
//         }
//         if (Input.GetKeyDown(KeyCode.LeftArrow)) {
//             if (gridMoveDirection != Direction.Right) {
//                 gridMoveDirection = Direction.Left;
//             }
//         }
//         if (Input.GetKeyDown(KeyCode.RightArrow)) {
//             if (gridMoveDirection != Direction.Left) {
//                 gridMoveDirection = Direction.Right;
//             }
//         }
// }

//     public void MobileInput() {
//         if (Input.touchCount > 0 && gameManager.gameIsActive && isMode == false) {
//         Touch touch = Input.GetTouch(0);

//         if (touch.phase == TouchPhase.Began) {
//             touchStartPos = touch.position;
//             touchStarted = true;
//         }
//         else if (touch.phase == TouchPhase.Moved && touchStarted) {
//             Vector2 swipeDelta = touch.position - touchStartPos;
                
//         if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) {
//             if (swipeDelta.x < 0 && gridMoveDirection != Direction.Right) {
//                 gridMoveDirection = Direction.Left;
//             }
//         }
//         if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) {
//             if (swipeDelta.x > 0 && gridMoveDirection != Direction.Left) {
//                 gridMoveDirection = Direction.Right;
//             }
//         }
//                 if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y)) {
//             if (swipeDelta.y > 0 && gridMoveDirection != Direction.Down) {
//                 gridMoveDirection = Direction.Up;
//             }
//         }
//         if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y)) {
//             if (swipeDelta.y < 0 && gridMoveDirection != Direction.Up) {
//                 gridMoveDirection = Direction.Down;
//             }
//         }
//         }
//     }
//     }

//     public void UpButton() {
//         if (gridMoveDirection != Direction.Down) {
//             gridMoveDirection = Direction.Up;
//         }
//     }
//     public void DownButton() {
//         if (gridMoveDirection != Direction.Up) {
//             gridMoveDirection = Direction.Down;
//         }
//     }
//     public void RightButton() {
//         if (gridMoveDirection != Direction.Left) {
//             gridMoveDirection = Direction.Right;
//         }
//     }
//     public void LeftButton() {
//         if (gridMoveDirection != Direction.Right) {
//                 gridMoveDirection = Direction.Left;
//         }
//     }


//     private void HandleGridMovement() {
//         if (state != State.Alive) return; 

//         gridMoveTimer += Time.deltaTime * speed;
//         if (gridMoveTimer >= gridMoveTimerMax) {
//             gridMoveTimer -= gridMoveTimerMax;

//             SnakeMovePosition previousSnakeMovePosition = null;
//             if (snakeMovePositionList.Count > 0) {
//                 previousSnakeMovePosition = snakeMovePositionList[0];
//             }

//             SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
//             snakeMovePositionList.Insert(0, snakeMovePosition);

//             Vector3 gridMoveDirectionVector;
//             switch (gridMoveDirection) {
//             default:
//             case Direction.Right:   gridMoveDirectionVector = new Vector3(+0.5f, 0); break;
//             case Direction.Left:    gridMoveDirectionVector = new Vector3(-0.5f, 0); break;
//             case Direction.Up:      gridMoveDirectionVector = new Vector3(0, +0.5f); break;
//             case Direction.Down:    gridMoveDirectionVector = new Vector3(0, -0.5f); break;
//             }

//             gridPosition += gridMoveDirectionVector;

//             gridPosition = levelGrid.ValidateGridPosition(gridPosition);

//             bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
//             if (snakeAteFood) {
//                 // Snake ate food, grow body
//                 snakeBodySize++;
//                 CreateSnakeBodyPart();
//                 gameManager.UpdateScore(1);
//                 gameManager.UpdateHighScore();
//                 snakeBite.Play();
//                 ChangeSprite(normalSprite);
//             }

//             if (snakeMovePositionList.Count >= snakeBodySize + 1) {
//                 snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
//             }

//             UpdateSnakeBodyParts();

//             foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList) {
//                 Vector3 snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
//                 if (gridPosition == snakeBodyPartGridPosition && isPowerUp == false) {
//                     gameOver.Play();
//                     gameManager.GameOver();
//                     state = State.Dead;
//                 }
//             }

//             transform.position = new Vector3(gridPosition.x, gridPosition.y);
//             transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);
//         }
//     }

//     private void CreateSnakeBodyPart() {
//         snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
//     }

//     private void UpdateSnakeBodyParts() {
//         for (int i = 0; i < snakeBodyPartList.Count; i++) {
//             snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
//         }
//     }


//     private float GetAngleFromVector(Vector3 dir) {
//         float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//         if (n < 0) n += 360;
//         return n;
//     }

//     public Vector3 GetGridPosition() {
//         return gridPosition;
//     }

//     public List<Vector3> GetFullSnakeGridPositionList() {
//         List<Vector3> gridPositionList = new List<Vector3>() { gridPosition };
//         foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList) {
//             gridPositionList.Add(snakeMovePosition.GetGridPosition());
//         }
//         return gridPositionList;
//     }



//     private class SnakeBodyPart {

//         private SnakeMovePosition snakeMovePosition;
//         private Transform transform;

//         public SnakeBodyPart(int bodyIndex) {
//             GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
//             snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
//             snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1 - bodyIndex;
//             transform = snakeBodyGameObject.transform;
//         }

//         public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition) {
//             this.snakeMovePosition = snakeMovePosition;

//             transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

//             float angle;
//             switch (snakeMovePosition.GetDirection()) {
//             default:
//             case Direction.Up: // Currently going Up
//                 switch (snakeMovePosition.GetPreviousDirection()) {
//                 default: 
//                     angle = 0; 
//                     break;
//                 case Direction.Left: // Previously was going Left
//                     angle = 0 + 45; 
//                     transform.position += new Vector3(.2f, .2f);
//                     break;
//                 case Direction.Right: // Previously was going Right
//                     angle = 0 - 45; 
//                     transform.position += new Vector3(-.2f, .2f);
//                     break;
//                 }
//                 break;
//             case Direction.Down: // Currently going Down
//                 switch (snakeMovePosition.GetPreviousDirection()) {
//                 default: 
//                     angle = 180; 
//                     break;
//                 case Direction.Left: // Previously was going Left
//                     angle = 180 - 45;
//                     transform.position += new Vector3(.2f, -.2f);
//                     break;
//                 case Direction.Right: // Previously was going Right
//                     angle = 180 + 45; 
//                     transform.position += new Vector3(-.2f, -.2f);
//                     break;
//                 }
//                 break;
//             case Direction.Left: // Currently going to the Left
//                 switch (snakeMovePosition.GetPreviousDirection()) {
//                 default: 
//                     angle = +90; 
//                     break;
//                 case Direction.Down: // Previously was going Down
//                     angle = 180 - 45; 
//                     transform.position += new Vector3(-.2f, .2f);
//                     break;
//                 case Direction.Up: // Previously was going Up
//                     angle = 45; 
//                     transform.position += new Vector3(-.2f, -.2f);
//                     break;
//                 }
//                 break;
//             case Direction.Right: // Currently going to the Right
//                 switch (snakeMovePosition.GetPreviousDirection()) {
//                 default: 
//                     angle = -90; 
//                     break;
//                 case Direction.Down: // Previously was going Down
//                     angle = 180 + 45; 
//                     transform.position += new Vector3(.2f, .2f);
//                     break;
//                 case Direction.Up: // Previously was going Up
//                     angle = -45; 
//                     transform.position += new Vector3(.2f, -.2f);
//                     break;
//                 }
//                 break;
//             }

//             transform.eulerAngles = new Vector3(0, 0, angle);
//         }

//         public Vector3 GetGridPosition() {
//             return snakeMovePosition.GetGridPosition();
//         }
//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.tag == "Wall" && isPowerUp == false)
//         {
//             gameOver.Play();
//             state = State.Dead;
//             gameManager.GameOver();
//         }
//         if (collision.gameObject.tag == "Obstacle" && isPowerUp == false)
//         {
//             monster.Play();
//             state = State.Dead;
//             gameManager.GameOver();
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("PowerUp"))
//         {
//             isPowerUp = true;
//             StartCoroutine(IsPowerUp());
//             powerUp.Play();
//             Destroy(collision.gameObject);
//             FindObjectOfType<FoodManager>().StartSpawn();
//             StartCoroutine(StartPowerUpTimer());
//         }
//     }

//     IEnumerator IsPowerUp()
//     {
//         yield return new WaitForSeconds(10);
//         isPowerUp = false;
//     }

//     IEnumerator StartPowerUpTimer()
//     {
//         currentTimer = powerUpDuration;
//         while (currentTimer > 0)
//         {
//             timerText.text = "Power-Up: " + currentTimer.ToString("F1");
//             yield return new WaitForSeconds(1f);
//             currentTimer -= 1f;
//         }
//         timerText.text = "";
//         isPowerUp = false;
//     }

//     private class SnakeMovePosition {

//         private SnakeMovePosition previousSnakeMovePosition;
//         private Vector3 gridPosition;
//         private Direction direction;

//         public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector3 gridPosition, Direction direction) {
//             this.previousSnakeMovePosition = previousSnakeMovePosition;
//             this.gridPosition = gridPosition;
//             this.direction = direction;
//         }

//         public Vector3 GetGridPosition() {
//             return gridPosition;
//         }

//         public Direction GetDirection() {
//             return direction;
//         }

//         public Direction GetPreviousDirection() {
//             if (previousSnakeMovePosition == null) {
//                 return Direction.Right;
//             } else {
//                 return previousSnakeMovePosition.direction;
//             }
//         }

//     }

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class Snake : MonoBehaviour {

    private enum Direction {
        Left,
        Right,
        Up,
        Down
    }

    private enum State { 
        Alive,
        Dead
    }   

    private State state;
    private Direction gridMoveDirection;
    private Vector3 gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;
    private SnakeTailPart snakeTailPart;
    private GameManager gameManager;
    public AudioSource powerUp;
    public AudioSource monster;
    public AudioSource gameOver;
    public AudioSource snakeBite;
    public bool isPowerUp = false;
    private float powerUpDuration = 10f;
    private float currentTimer; 
    public TextMeshProUGUI timerText;
    private bool touchStarted = false;
    private Vector2 touchStartPos;
    private bool isMode;
    public float speed;
    public Sprite normalSprite;
    public Sprite nearFoodSprite;
    public Sprite gameOverSprite;
    private SpriteRenderer snakeHeadSpriteRenderer;

    public void Setup(LevelGrid levelGrid) {
        this.levelGrid = levelGrid;
    }

    private void Awake() {
        snakeHeadSpriteRenderer = GetComponent<SpriteRenderer>();
        gridPosition = new Vector3(0, 0);
        gridMoveTimerMax = .1f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Direction.Right;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodySize = 0;

        snakeBodyPartList = new List<SnakeBodyPart>();

        state = State.Alive;
    }

    private bool intToBool(int intValue)
    {
        return intValue == 1 ? true : false;
    }

    private void Start() {
        speed = 1f;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isMode = intToBool(PlayerPrefs.GetInt("isMode"));
    }

    private void Update() {
        switch (state) {
        case State.Alive:
            HandleInput();
            MobileInput();
            HandleGridMovement();
            CheckProximityToFood();
            CheckProximityToPowerUp();
            break;
        case State.Dead:
            CheckProximityToGameOver();
            break;
        }
    }

    private void CheckProximityToFood() {
        Vector3 foodPosition = levelGrid.foodGridPosition;
        float distanceToFood = Vector3.Distance(gridPosition, foodPosition);
        if (distanceToFood < 1.5f) {
            ChangeSprite(nearFoodSprite);
        } else {
            ChangeSprite(normalSprite);
        }
    }

    private void CheckProximityToPowerUp() {
        Vector3 powerUpPosition = FindObjectOfType<FoodManager>().powerUpPosition;
        float distanceToFood = Vector3.Distance(gridPosition, powerUpPosition);
        if (distanceToFood < 1.5f && FindObjectOfType<FoodManager>().isSpawning) {
            ChangeSprite(nearFoodSprite);
        }
    }

    private void CheckProximityToGameOver() {
        if (state == State.Dead) {
            ChangeSprite(gameOverSprite);
        } else {
            ChangeSprite(normalSprite);
        }
    }

    private void ChangeSprite(Sprite sprite) {
        snakeHeadSpriteRenderer.sprite = sprite;
    }

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (gridMoveDirection != Direction.Down) {
                gridMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (gridMoveDirection != Direction.Up) {
                gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (gridMoveDirection != Direction.Right) {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (gridMoveDirection != Direction.Left) {
                gridMoveDirection = Direction.Right;
            }
        }
    }

    public void MobileInput() {
        if (Input.touchCount > 0 && gameManager.gameIsActive && isMode == false) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                touchStartPos = touch.position;
                touchStarted = true;
            }
            else if (touch.phase == TouchPhase.Moved && touchStarted) {
                Vector2 swipeDelta = touch.position - touchStartPos;
                    
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) {
                    if (swipeDelta.x < 0 && gridMoveDirection != Direction.Right) {
                        gridMoveDirection = Direction.Left;
                    }
                }
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) {
                    if (swipeDelta.x > 0 && gridMoveDirection != Direction.Left) {
                        gridMoveDirection = Direction.Right;
                    }
                }
                if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y)) {
                    if (swipeDelta.y > 0 && gridMoveDirection != Direction.Down) {
                        gridMoveDirection = Direction.Up;
                    }
                }
                if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y)) {
                    if (swipeDelta.y < 0 && gridMoveDirection != Direction.Up) {
                        gridMoveDirection = Direction.Down;
                    }
                }
            }
        }
    }

    public void UpButton() {
        if (gridMoveDirection != Direction.Down) {
            gridMoveDirection = Direction.Up;
        }
    }

    public void DownButton() {
        if (gridMoveDirection != Direction.Up) {
            gridMoveDirection = Direction.Down;
        }
    }

    public void RightButton() {
        if (gridMoveDirection != Direction.Left) {
            gridMoveDirection = Direction.Right;
        }
    }

    public void LeftButton() {
        if (gridMoveDirection != Direction.Right) {
            gridMoveDirection = Direction.Left;
        }
    }

    private void HandleGridMovement() {
        if (state != State.Alive) return; 

        gridMoveTimer += Time.deltaTime * speed;
        if (gridMoveTimer >= gridMoveTimerMax) {
            gridMoveTimer -= gridMoveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0) {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }

            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector3 gridMoveDirectionVector;
            switch (gridMoveDirection) {
            default:
            case Direction.Right:   gridMoveDirectionVector = new Vector3(+0.5f, 0); break;
            case Direction.Left:    gridMoveDirectionVector = new Vector3(-0.5f, 0); break;
            case Direction.Up:      gridMoveDirectionVector = new Vector3(0, +0.5f); break;
            case Direction.Down:    gridMoveDirectionVector = new Vector3(0, -0.5f); break;
            }

            gridPosition += gridMoveDirectionVector;

            gridPosition = levelGrid.ValidateGridPosition(gridPosition);

            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood) {
                // Snake ate food, grow body
                snakeBodySize++;
                CreateSnakeBodyPart();
                gameManager.UpdateScore(1);
                gameManager.UpdateHighScore();
                snakeBite.Play();
                ChangeSprite(normalSprite);
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1) {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyParts();
            UpdateSnakeTailPart();

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList) {
                Vector3 snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition && isPowerUp == false) {
                    gameOver.Play();
                    gameManager.GameOver();
                    state = State.Dead;
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);
        }
    }

    private void CreateSnakeBodyPart() {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
        if (snakeBodyPartList.Count == 1) {
            snakeTailPart = new SnakeTailPart(snakeBodyPartList.Count);
        }
    }

    private void UpdateSnakeBodyParts() {
        for (int i = 0; i < snakeBodyPartList.Count; i++) {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private void UpdateSnakeTailPart() {
        if (snakeTailPart != null) {
            snakeTailPart.SetSnakeMovePosition(snakeMovePositionList[snakeMovePositionList.Count - 1]);
        }
    }

    private float GetAngleFromVector(Vector3 dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector3 GetGridPosition() {
        return gridPosition;
    }

    public List<Vector3> GetFullSnakeGridPositionList() {
        List<Vector3> gridPositionList = new List<Vector3>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList) {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }

    private class SnakeBodyPart {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex) {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1 - bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition) {
            this.snakeMovePosition = snakeMovePosition;

            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection()) {
            default:
            case Direction.Up: // Currently going Up
                switch (snakeMovePosition.GetPreviousDirection()) {
                default: 
                    angle = 0; 
                    break;
                case Direction.Left: // Previously was going Left
                    angle = 0 + 45; 
                    transform.position += new Vector3(.2f, .2f);
                    break;
                case Direction.Right: // Previously was going Right
                    angle = 0 - 45; 
                    transform.position += new Vector3(-.2f, .2f);
                    break;
                }
                break;
            case Direction.Down: // Currently going Down
                switch (snakeMovePosition.GetPreviousDirection()) {
                default: 
                    angle = 180; 
                    break;
                case Direction.Left: // Previously was going Left
                    angle = 180 - 45;
                    transform.position += new Vector3(.2f, -.2f);
                    break;
                case Direction.Right: // Previously was going Right
                    angle = 180 + 45; 
                    transform.position += new Vector3(-.2f, -.2f);
                    break;
                }
                break;
            case Direction.Left: // Currently going to the Left
                switch (snakeMovePosition.GetPreviousDirection()) {
                default: 
                    angle = +90; 
                    break;
                case Direction.Down: // Previously was going Down
                    angle = 180 - 45; 
                    transform.position += new Vector3(-.2f, .2f);
                    break;
                case Direction.Up: // Previously was going Up
                    angle = 45; 
                    transform.position += new Vector3(-.2f, -.2f);
                    break;
                }
                break;
            case Direction.Right: // Currently going to the Right
                switch (snakeMovePosition.GetPreviousDirection()) {
                default: 
                    angle = -90; 
                    break;
                case Direction.Down: // Previously was going Down
                    angle = 180 + 45; 
                    transform.position += new Vector3(.2f, .2f);
                    break;
                case Direction.Up: // Previously was going Up
                    angle = -45; 
                    transform.position += new Vector3(.2f, -.2f);
                    break;
                }
                break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector3 GetGridPosition() {
            return snakeMovePosition.GetGridPosition();
        }
    }

    private class SnakeTailPart {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;

        public SnakeTailPart(int bodyIndex) {
            GameObject snakeTailGameObject = new GameObject("SnakeTail", typeof(SpriteRenderer));
            snakeTailGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeTailPart;
            snakeTailGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1 - bodyIndex;
            transform = snakeTailGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition) {
            this.snakeMovePosition = snakeMovePosition;

            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection()) {
            default:
            case Direction.Up: // Currently going Up
                angle = 0;
                break;
            case Direction.Down: // Currently going Down
                angle = 180;
                break;
            case Direction.Left: // Currently going Left
                angle = 90;
                break;
            case Direction.Right: // Currently going Right
                angle = -90;
                break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector3 GetGridPosition() {
            return snakeMovePosition.GetGridPosition();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall" && isPowerUp == false) {
            gameOver.Play();
            state = State.Dead;
            gameManager.GameOver();
        }
        if (collision.gameObject.tag == "Obstacle" && isPowerUp == false) {
            monster.Play();
            state = State.Dead;
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("PowerUp")) {
            isPowerUp = true;
            StartCoroutine(IsPowerUp());
            powerUp.Play();
            Destroy(collision.gameObject);
            FindObjectOfType<FoodManager>().StartSpawn();
            StartCoroutine(StartPowerUpTimer());
        }
    }

    IEnumerator IsPowerUp() {
        yield return new WaitForSeconds(10);
        isPowerUp = false;
    }

    IEnumerator StartPowerUpTimer() {
        currentTimer = powerUpDuration;
        while (currentTimer > 0) {
            timerText.text = "Power-Up: " + currentTimer.ToString("F1");
            yield return new WaitForSeconds(1f);
            currentTimer -= 1f;
        }
        timerText.text = "";
        isPowerUp = false;
    }

    private class SnakeMovePosition {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector3 gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector3 gridPosition, Direction direction) {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector3 GetGridPosition() {
            return gridPosition;
        }

        public Direction GetDirection() {
            return direction;
        }

        public Direction GetPreviousDirection() {
            if (previousSnakeMovePosition == null) {
                return Direction.Right;
            } else {
                return previousSnakeMovePosition.direction;
            }
        }
    }
}

