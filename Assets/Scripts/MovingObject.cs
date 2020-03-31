using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour{

    public float moveTime = 0.1f;
    public LayerMask blokingLayer;
    
    private float movemetSpeed;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    
    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        movemetSpeed = 1 / moveTime;
    }

    protected IEnumerator SmoothMovemet(Vector2 end)
    {
        float remainigDistance = Vector2.Distance(rb2D.position, end);
        while (remainigDistance > float.Epsilon)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb2D.position, end, movemetSpeed * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            remainigDistance = Vector2.Distance(rb2D.position, end);
            yield return null;
        }
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blokingLayer);
        boxCollider.enabled = true;
        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovemet(end));
            return true;
        }
        return false;
    }

    protected abstract void OnCantMove(GameObject go);

    protected virtual bool AttemptMove(int xDir, int yDir)
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        if (!canMove)
        {
            OnCantMove(hit.transform.gameObject);
        }
        return canMove;
    }
}