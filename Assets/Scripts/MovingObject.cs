using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour{

    public float moveTime = 0.1f;
    public LayerMask blokingLayer;
    
    private float movemetSpeed;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        movemetSpeed = 1 / moveTime;
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blokingLayer)
        boxCollider.enabled = true;
    }
}
