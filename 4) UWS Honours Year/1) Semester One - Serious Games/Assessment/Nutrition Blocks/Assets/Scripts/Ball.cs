﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    ManagerGame ManagerGame;    // Getting ManagerGame.cs.
    Rigidbody2D Rigidbody2D;
    CircleCollider2D CircleCollider2D;
    SpriteRenderer SpriteRenderer;
    GameObject PlayerObject;
    int Direction;
    public float AngleX;
    public bool CheckDefault;
    public bool CheckFruit;
    public bool CheckGrains;
    public bool CheckDairy;
    public bool CheckMeat;
    public bool CheckBad;
    public Sprite WhiteDefault; // #e2e2e2.
    public Sprite GreenFruit;   // #00ff01.
    public Sprite YellowGrains; // #ffeb01.
    public Sprite BlueDairy;    // #00aaff.
    public Sprite RedMeat;      // #ff1500.
    public Sprite PurpleBad;    // #aa00ff.

    void Start()
    {
        ManagerGame = GameObject.Find("ManagerGame").GetComponent<ManagerGame>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CircleCollider2D = GetComponent<CircleCollider2D>();
        PlayerObject = GameObject.Find("Player"); // For the Ball to be ontop of Player before launching.
        Direction = Random.Range(0, 2) * 2 - 1;   // Direction can be left(1) or right(-1).
        SpriteRenderer.sprite = WhiteDefault;
        BallWhite();
    }

    void Update()
    {   // Displaying speed of ball in Inspector.
        ManagerGame.BallSpeedCurrent = Rigidbody2D.velocity.magnitude;
        if (ManagerGame.BallLaunched == false)
        {   // Starting Ball Y = -9.648623.
            transform.position = new Vector2(PlayerObject.transform.position.x, PlayerObject.transform.position.y + 3.351377f);
        }
        if (Input.GetKey(KeyCode.Space) && ManagerGame.BallLaunched == false)
        {   // Launching the ball to start.
            ManagerGame.BallLaunched = true;                    // Gravity of RigidBody == 0.
            Rigidbody2D.AddForce(new Vector2(AngleX * Direction, 5) * ManagerGame.BallSpeed);
        }
        if (Input.GetKey(KeyCode.Q)) { BallWhite(); }
        if (Input.GetKey(KeyCode.W)) { BallGreen(); }
        if (Input.GetKey(KeyCode.E)) { BallYellow(); }
        if (Input.GetKey(KeyCode.Alpha1)) { BallBlue(); }
        if (Input.GetKey(KeyCode.Alpha2)) { BallRed(); }
        if (Input.GetKey(KeyCode.Alpha3)) { BallPurple(); }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ManagerGame.PlayerIsPlaying == false)
        {   // Reducing the bouce of ball with below when Ball hits Player.
            Rigidbody2D.drag = 1; Rigidbody2D.gravityScale = 2;
            if (collision.gameObject.tag == "wall") { CircleCollider2D.sharedMaterial = null; }
        }
    }

    public void BallWhite()
    {
        SpriteRenderer.sprite = WhiteDefault;
        CheckDefault = true;
        CheckFruit = false;
        CheckGrains = false;
        CheckDairy = false;
        CheckMeat = false;
        CheckBad = false;
    }
    public void BallGreen()
    {
        SpriteRenderer.sprite = GreenFruit;
        CheckDefault = false;
        CheckFruit = true;
        CheckGrains = false;
        CheckDairy = false;
        CheckMeat = false;
        CheckBad = false;
    }
    public void BallYellow()
    {
        SpriteRenderer.sprite = YellowGrains;
        CheckDefault = false;
        CheckFruit = false;
        CheckGrains = true;
        CheckDairy = false;
        CheckMeat = false;
        CheckBad = false;
    }
    public void BallBlue()
    {
        SpriteRenderer.sprite = BlueDairy;
        CheckDefault = false;
        CheckFruit = false;
        CheckGrains = false;
        CheckDairy = true;
        CheckMeat = false;
        CheckBad = false;
    }
    public void BallRed()
    {
        SpriteRenderer.sprite = RedMeat;
        CheckDefault = false;
        CheckFruit = false;
        CheckGrains = false;
        CheckDairy = false;
        CheckMeat = true;
        CheckBad = false;
    }
    public void BallPurple()
    {
        SpriteRenderer.sprite = PurpleBad;
        CheckDefault = false;
        CheckFruit = false;
        CheckGrains = false;
        CheckDairy = false;
        CheckMeat = false;
        CheckBad = true;
    }
}