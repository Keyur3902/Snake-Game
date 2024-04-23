using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    [SerializeField] private Snake snake;

    private LevelGrid levelGrid;
    private ViewportHandler viewportHandler;

    private void Start() {
        viewportHandler = FindObjectOfType<ViewportHandler>();
        Debug.Log("GameHandler.Start");
        float globalHeight = viewportHandler.gridHeight + 0.5f;
        float globalWidth = viewportHandler.gridWidth + 0.5f;

        levelGrid = new LevelGrid(globalWidth, globalHeight, viewportHandler);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

}
