//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace y01cu {
    public class PuzzleCounterManager : MonoBehaviour {
        private TextMeshProUGUI puzzleCounterText;
        private float puzzleTime = 25f;
        private Vector2 enemySpawnTransform;
        [SerializeField] private Enemy enemyPrefab;
        private List<Enemy> puzzleEnemies = new List<Enemy>();
        private Player player;

        private void Awake() {
        }

        private void Start() {
            InvokeRepeating("FindRequiredGameObjectsAndComponents", 2f, 1f);
        }

        private void FindRequiredGameObjectsAndComponents() {
            puzzleCounterText = transform.Find("Text_TimeLeft").GetComponent<TextMeshProUGUI>();

            bool isItLevelTwo = SceneManager.GetActiveScene().buildIndex == 2;
            if (isItLevelTwo) {
                enemySpawnTransform = GameObject.Find("PuzzleEnemySpawnPosition").GetComponent<Transform>().position;
            }

            player = GameObject.Find("Player").GetComponent<Player>();
        }

        private float puzzleCounterSpeedMultiplier = 1f;

        private IEnumerator StartAndUpdatePuzzleCounter() {
            if (puzzleTime > 0) {
                yield return new WaitForSeconds(1f);
                puzzleTime -= 1 * puzzleCounterSpeedMultiplier;
                puzzleCounterText.text = "Time left: " + puzzleTime;
                Debug.Log("Puzzle time: " + puzzleTime);
                StartCoroutine(StartAndUpdatePuzzleCounter());
            }

            if (puzzleTime <= 0) {
                player.RecieveDamageFromPuzzle();
                RefreshCounter();
            }
        }

        private void SpawnEnemy() {
            Enemy enemy = Instantiate(enemyPrefab, enemySpawnTransform, Quaternion.identity);

            puzzleEnemies.Add(enemy);
        }

        public void DestroyEnemies() {
            foreach (Enemy puzzleEnemy in puzzleEnemies) {
                puzzleEnemy.GetDestroyedByPuzzleSolution();
                puzzleEnemies.Remove(puzzleEnemy);
            }
        }


        public void StartCounter() {
            Debug.Log("Counter is started.");
            StartCoroutine(StartAndUpdatePuzzleCounter());
        }

        public void RefreshCounter() {
            puzzleTime = 25f;
            StartCoroutine(StartAndUpdatePuzzleCounter());
        }


        //  TODO: ai suggested puzzleTime.ToString("F0"); Learn what that means later on. And also learn what yield return null means.
    }
}