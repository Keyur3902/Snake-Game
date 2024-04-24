using System.Collections;
using UnityEngine;
using TMPro;

public class FoodManager : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public TextMeshProUGUI timerText;
    private float minSpawnDelay = 10;
    private float maxSpawnDelay = 15;
    private GameManager gameManager;
    private bool isSpawning;
    private bool powerUpActive = false;
    private float powerUpDuration = 7f;
    private ViewportHandler viewportHandler;
    float globalHeight;
    float globalWidth;

    private void Start()
    {
        viewportHandler = FindObjectOfType<ViewportHandler>();
        globalHeight = viewportHandler.gridHeight + 0.5f;
        globalWidth = viewportHandler.gridWidth + 0.5f;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartSpawn();
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            if (gameManager.gameIsActive && !isSpawning && !powerUpActive)
            {
                SpawnPowerUp();
            }
            yield return null;
        }
    }

    public void SpawnPowerUp()
    {
        float x = Random.Range(-globalWidth+2, globalWidth-2);
        float y = Random.Range(-globalHeight-2, globalHeight+2);
        GameObject newPowerUp = Instantiate(powerUpPrefab, new Vector3(x, y, 0), Quaternion.identity);
        isSpawning = true;
        powerUpActive = true;
        StartCoroutine(RotatePowerUp(newPowerUp));
        StartCoroutine(DestroyPowerUpAfterDelay(newPowerUp));
    }

    IEnumerator DestroyPowerUpAfterDelay(GameObject powerUp)
    {
        yield return new WaitForSeconds(powerUpDuration);
        Destroy(powerUp);
        isSpawning = false;
        timerText.text = "";
        powerUpActive = false;
    }

    IEnumerator RotatePowerUp(GameObject powerUp)
    {
        while (isSpawning)
        {
            powerUp.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
            yield return null;
        }
    }
}


