using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelGrid {

    public Vector3 foodGridPosition;
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
        globalWidth = viewportHandler.gridWidth;
        globalHeight = viewportHandler.gridHeight;
    }

    private void SpawnFood() {
        do {
            foodGridPosition = new Vector3(Random.Range(-globalWidth+2, width-3), Random.Range(-globalHeight-2, height+2));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

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
        globalHeight = viewportHandler.gridHeight;
        globalWidth = viewportHandler.gridWidth;
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
