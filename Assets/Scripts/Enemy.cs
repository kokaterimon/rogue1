using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject{

    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;
           
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
    }

    protected override void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    protected override void AttemptMove(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttemptMove(xDir, yDir);
        skipMove = true;
    }

    public void moveEnemy()
    {
        int xDir, yDir;
        //if()
    }

    protected override void OnCantMove(GameObject go)
    {
        //FALTA CÓDIGO AQUÍ!!!!!
    }


}
