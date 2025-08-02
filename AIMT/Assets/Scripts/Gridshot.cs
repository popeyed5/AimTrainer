using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gridshot : MonoBehaviour
{
    public GameObject targetPrefab;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public List<GameObject> targets = new List<GameObject>();

    private float BASE_SPAWN_RATE = 1.0f; // Starting spawn rate
    private float MIN_SPAWN_RATE = 0.1f;   // Fastest possible spawn rate

    private float difficultyTimer = 0f;
    private float spawnTimer = 0f;

    private int difficultyLevel = 1;
    private int maxDifficulty = 10;
    private int score = 0;
    private int lives = 3;

    private bool gameActive = false;
    private float currentSpawnRate;
    void Update()
    {
        if (gameActive)
        {
            // Handle target spawning
            if (spawnTimer < currentSpawnRate)
            {
                spawnTimer += Time.deltaTime;
            }
            else
            {
                SpawnTarget();
                spawnTimer = 0f;
            }

            difficultyTimer += Time.deltaTime;
            if (difficultyTimer > 10f && difficultyLevel < maxDifficulty)
            {
                difficultyLevel++;
                difficultyTimer = 0f;
                UpdateDifficulty();
            }
        }
    }

    private void UpdateDifficulty()
    {
        // Exponential difficulty increase - gets harder faster as you progress
        float difficultyFactor = Mathf.Pow(1.3f, difficultyLevel);

        currentSpawnRate = BASE_SPAWN_RATE / difficultyFactor;
        currentSpawnRate = Mathf.Max(currentSpawnRate, MIN_SPAWN_RATE);
    }
    private void UpdateLives()
    {
        if (livesText != null)
        {
            livesText.text = "lives: " + lives;
        }
        else
        {
            Debug.LogWarning("Lives Text is not assigned.");
        }
    }
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned.");
        }
    }

    private void SpawnTarget()
    {
        if (targetPrefab == null)
        {
            Debug.LogError("Target prefab not assigned.");
            return;
        }

        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, 0f);
        Vector3 spawnPosition = new Vector3(
            UnityEngine.Random.Range(-5f, 5f),
            UnityEngine.Random.Range(-3f, 2f),
            0f
        );

        GameObject newTarget = Instantiate(targetPrefab, spawnPosition, spawnRotation);
        targets.Add(newTarget);
    }

    public void StartGridshot()
    {
        if (gameActive)
        {
            return;
        }

        lives = 3;
        score = 0;
        spawnTimer = 0f;
        difficultyTimer = 0f;
        difficultyLevel = 1;
        currentSpawnRate = BASE_SPAWN_RATE;
        CleanTargets();

        gameActive = true;
        UpdateLives();
        UpdateDifficulty();
        UpdateScoreDisplay();
    }

    public void StopGridshot()
    {
        if (!gameActive)
        {
            return;
        }
        gameActive = false;
        CleanTargets();
    }
    private void CleanTargets()
    {
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                target.GetComponent<SpriteRenderer>().color = Color.red;
                Destroy(target, 0.1f);
            }
        }
        targets.Clear();
    }
    public void RemoveTarget(GameObject target)
    {
        if (target != null && targets.Contains(target))
        {
            targets.Remove(target);
            Destroy(target, 0.1f);
        }
    }
    public void OnTargetHit() // Put [ContextMenu("IncreaseScore")] on top for debugging in the editor.
    {
        score++;
        UpdateScoreDisplay();
    }
    public void OnTargetMissed()
    {
        if (!gameActive)
        {
            return;
        }

        lives--;
        UpdateLives();
        if (lives <= 0)
        {
            StopGridshot();
            return;
        }
    }
}