using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelGrid {

    private Vector3 foodGridPosition;
    private GameObject foodGameObject;
    private float width;
    private float height;
    private Snake snake;
    private float globalWidth;
    private float globalHeight;
    private ViewportHandler viewportHandler;

    public LevelGrid(float width, float height, ViewportHandler viewportHandler){
        this.width = width;
        this.height = height;
        this.viewportHandler = viewportHandler;
    }

    public void Setup(Snake snake) {
        this.snake = snake;

        SpawnFood();
    }

    private void Awake(){
        globalWidth = viewportHandler.gridWidth - 0.5f;
        globalHeight = viewportHandler.gridHeight - 0.5f;
    }

    private void SpawnFood() {
        do {
            foodGridPosition = new Vector3(Random.Range(-globalWidth+2, width-3), Random.Range(-globalHeight-2, height+2));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

    // public bool TrySnakeEatFood(Vector3 snakeGridPosition) {
    //     Vector3 roundedSnakeGridPosition = new Vector3(Mathf.Round(snakeGridPosition.x), Mathf.Round(snakeGridPosition.y));
    //     Vector3 roundedFoodGridPosition = new Vector3(Mathf.Round(foodGridPosition.x), Mathf.Round(foodGridPosition.y));
    //     if (roundedSnakeGridPosition  == roundedFoodGridPosition) {
    //         Object.Destroy(foodGameObject);
    //         SpawnFood();
    //         return true;
    //     } else {
    //         return false;
    //     }
    // }

    public bool TrySnakeEatFood(Vector3 snakeGridPosition) {
        float tolerance = 0.7f;

        float squaredDistance = (snakeGridPosition - foodGridPosition).sqrMagnitude;

        if (squaredDistance <= tolerance * tolerance) {
            Object.Destroy(foodGameObject);
            SpawnFood();
            return true;
        } else {
            return false;
        }
    }


    public Vector3 ValidateGridPosition(Vector3 gridPosition) {
        globalHeight = viewportHandler.gridHeight - 0.5f;
        globalWidth = viewportHandler.gridWidth - 0.5f;
        if (gridPosition.x < -globalWidth) {
            gridPosition.x = width - 1;
        }
        if (gridPosition.x > width - 1) {
            gridPosition.x = -globalWidth;
        }
        if (gridPosition.y < globalHeight) {
            gridPosition.y = -height -0.8f;
        }
        if (gridPosition.y > -height -0.8f) {
            gridPosition.y = globalHeight;
        }
        return gridPosition;
    }
}
