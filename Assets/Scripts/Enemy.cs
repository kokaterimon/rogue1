<<<<<<< HEAD
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject{
>>>>>>> 9ae4b4c3eeb9e5c92dcfe4dcf9b5400f51a7783d

    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;
<<<<<<< HEAD

=======
           
>>>>>>> 9ae4b4c3eeb9e5c92dcfe4dcf9b5400f51a7783d
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

<<<<<<< HEAD
    public void MoveEnemy()
    {
        int xDir, yDir;
        if(Math.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
=======
    public void moveEnemy()
    {
        int xDir, yDir;
        //if()
>>>>>>> 9ae4b4c3eeb9e5c92dcfe4dcf9b5400f51a7783d
    }

    protected override void OnCantMove(GameObject go)
    {
<<<<<<< HEAD
        //FALTA CÒDIGO AQUÌ
    }



}       
=======
        //FALTA CÓDIGO AQUÍ!!!!!
    }


}
>>>>>>> 9ae4b4c3eeb9e5c92dcfe4dcf9b5400f51a7783d
