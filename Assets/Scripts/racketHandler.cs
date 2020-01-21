﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racketHandler : MonoBehaviour
{

    public string axis = "Horizontal";
    public float paddleSpeed = 150;
    GameObject[] balls;
    Collider2D ballCollision;
    public int counter = 0;

    private void Start()
    {
        balls = GameObject.FindGameObjectsWithTag("ball");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = Vector2.right * h * paddleSpeed;

        float p = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(p, 0) * paddleSpeed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "sizeDown(Clone)") shrinkPaddle();

        else if (collision.gameObject.name == "sizeUp(Clone)") enlargePaddle();

        else if (collision.gameObject.name == "slowDown(Clone)") slowPaddleDown();

        else if (collision.gameObject.name == "speedUp(Clone)") speedPaddleUp();

        else if (collision.gameObject.name == "slowBall(Clone)") slowBallsDown();


        //TODO:REFACTOR THIS CODE AS WELL
        else if (collision.gameObject.name == "rocketBall(Clone)")
        {
            foreach (GameObject ballobj in balls)
            {
                ballobj.SendMessage("increaseSpeed", 0.5f, SendMessageOptions.RequireReceiver);
            }
        }

        //TODO: Refactor this code as well
        else if (collision.gameObject.name == "indestructable(Clone)")
        {
            foreach (GameObject ballobj in balls)
            {
                ballobj.SendMessage("indestructable", 0.5f, SendMessageOptions.RequireReceiver);
            }
        }

        else if (collision.gameObject.name == "multiBall(Clone)")
        {
            foreach (GameObject ballObj in balls)
            {
                Instantiate(ballObj);
                balls = GameObject.FindGameObjectsWithTag("ball");
            }
            Debug.Log(balls.Length);
        }
        
        else if (collision.gameObject.name == "instaKill(Clone)")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.name == "metalBall")
        {
            Debug.Log(collision.gameObject.name);
            ballCollision = collision.gameObject.GetComponent<Collider2D>();
            ballCollision.isTrigger = false;
        }
    }




    //Functions modifying the Paddle
    void shrinkPaddle()
    {
        if (GetComponent<Transform>().localScale.x >= 0.47f)
        {
            GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x / 2, .488f);
        }
        Invoke("normalizePaddleSize", 7);
    }

    void enlargePaddle()
    {
        if (GetComponent<Transform>().localScale.x <= 0.47f)
        {
            GetComponent<Transform>().localScale = new Vector2(GetComponent<Transform>().localScale.x * 2, .488f);
        }
        Invoke("normalizePaddleSize", 7);
    }

    void normalizePaddleSize()
    {
        GetComponent<Transform>().localScale = new Vector2(0.47f, 0.488f);
    }

    void speedPaddleUp()
    {
        if (paddleSpeed <= 13)
        {
            paddleSpeed = paddleSpeed * 2.0f;
        }
        Invoke("normalizePaddleSpeed", 7);
    }

    void slowPaddleDown()
    {
        if (paddleSpeed >= 13)
        {
            paddleSpeed = paddleSpeed / 2.0f;
        }
        Invoke("normalizePaddleSpeed", 7);
    }

    void normalizePaddleSpeed()
    {
        paddleSpeed = 13;
    }



    //Functions modifying the ball
    void slowBallsDown()
    {
        foreach (GameObject ballobj in balls)
        {
            ballobj.SendMessage("decreaseSpeed", 0.5f, SendMessageOptions.RequireReceiver);
        }
        Invoke("normalizeBallSpeeds", 5);
    }

    void normalizeBallSpeeds()
    {
        foreach (GameObject ballobj in balls)
        {
            ballobj.SendMessage("normalizeBallSpeed", 0.5f, SendMessageOptions.RequireReceiver);
        }
    }
}

