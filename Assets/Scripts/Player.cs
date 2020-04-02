using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject{       

    public AudioClip moveSound1, moveSound2, eatSound1, eatSound2, drinkSound1, drinkSound2, gameOverSound;

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;
    public Text foodText;
    
    private Animator animator;
    private int food;

    private Vector2 touchOrigin = -Vector2.one;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
    }
    protected override void Start()
    {
        food = GameManager.instance.playerFoodPoints;
        foodText.text = "food: " + food;
        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
    }

    void CheckIfGameOver()
    {
        if (food <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }

    protected override bool AttemptMove(int xDir, int yDir)
    {
        food--;
        foodText.text = "food: " + food;
        bool canMove = base.AttemptMove(xDir, yDir);
        if (canMove) SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
        return canMove;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playersTurn || GameManager.instance.doingSetup) return;

        int horizontal = 0;
        int vertical = 0;

#if UNITY_STANDOLE || UNITY_WEBPLAYER || UNITY_EDITOR
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        if (horizontal != 0) vertical = 0;
#else
        if (Input.touchCount > 0)
        {
            Touch mytouch = Input.touches[0];
            if (mytouch.phase == TouchPhase.Began)
            {
                touchOrigin = mytouch.position;
            }else if (mytouch.phase == TouchPhase.Ended && touchOrigin != Vector2.one)
            {
                Vector2 touchEnd = mytouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    horizontal = x > 0 ? 1 : -1;
                }else
                {
                    vertical = y > 0 ? 1 : -1;
                }
            }
        }
#endif
        if (horizontal != 0 || vertical != 0) AttemptMove(horizontal, vertical);        
    }

    protected override void OnCantMove(GameObject go)
    {
        Wall hitWall = go.GetComponent<Wall>();
        if (hitWall != null)
        {
            hitWall.DamageWall(wallDamage);
            animator.SetTrigger("playerChop");
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoseFood(int loss)
    {
        food -= loss;
        foodText.text = "-" + loss + "food: " + food;
        animator.SetTrigger("playerHit");
        CheckIfGameOver();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit"))
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }else if (other.CompareTag("Food"))
        {
            food += pointsPerFood;
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            foodText.text = "+"+pointsPerFood+"food: " + food;
            other.gameObject.SetActive(false);
        }else if (other.CompareTag("Soda"))
        {
            food += pointsPerSoda;
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
            foodText.text = "+" + pointsPerSoda + "food: " + food;
            other.gameObject.SetActive(false);
        }
    }
}
