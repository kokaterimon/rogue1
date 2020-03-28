using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public static GameManager instance;
    public float turnDelay = 0.01f;

    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector]public bool playersTurn = true;

    private List<Enemy> enemies = new List<Enemy>();
    private bool enemiesMoving;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else if (GameManager.instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
    }

    private void Start()
    {
        InitGame();
    }

    void InitGame()
    {
        enemies.Clear();
        boardScript.SetupScene(16);
    }

    public void GameOver()
    {
        enabled = false;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playersTurn = true;
        enemiesMoving = false;
    }
    private void Update()
    {
        if (playersTurn || enemiesMoving) return;

        StartCoroutine(MoveEnemies());  
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }
}
