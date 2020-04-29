﻿using UnityEngine;
using Assets.Scripts.Enums;
using TMPro;

public class PlayerMovementHandler : MonoBehaviour
{
    private readonly float maxSpeed = 2;
    private readonly float jumpForce = 4;
    private bool isGrounded;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    public PlayerStateEnum playerState;
    private bool canDoubleJump;
    private GameObject jumpEffectPrefab;
    private int score = 0;
    private TextMeshProUGUI scoreText;

    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Gets the Score Text from player 1
        scoreText = GameObject.Find("Player1 Name & Score").GetComponent<TextMeshProUGUI>();

        scoreText.text = "P1 - " + PlayerPrefs.GetString("p1Selection") + ": " + score.ToString();

        // Gets the jump effect at runtime
        jumpEffectPrefab = Resources.Load<GameObject>("Prefabs/pfDoubleJumpEffect");

        // Default State
        playerState = PlayerStateEnum.Idle;
    }
    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float movimento = Input.GetAxisRaw("Horizontal");
        if (movimento < 0)
        {
            spriteRenderer.flipX = true;

            // Just play the walking animation when the player is grounded
            if (isGrounded)
                playerState = PlayerStateEnum.Walking;
        }
        else if (movimento > 0)
        {
            spriteRenderer.flipX = false;

            // Just play the walking animation when the player is grounded
            if (isGrounded)
                playerState = PlayerStateEnum.Walking;
        }
        else if (movimento == 0 && isGrounded)
        {
            playerState = PlayerStateEnum.Idle;
        }

        rigidbody.velocity = new Vector2(movimento * maxSpeed, rigidbody.velocity.y);

        if (Input.GetButton("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rigidbody.velocity = Vector2.up * jumpForce;
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (canDoubleJump)
                {
                    rigidbody.velocity = Vector2.up * (jumpForce + 2);
                    Instantiate(jumpEffectPrefab, new Vector3(transform.position.x, transform.position.y, -9), Quaternion.identity);
                    canDoubleJump = false;
                }
            }
        }

        playerState = PlayerStateEnum.Jumping;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Grounds"))
        {
            isGrounded = true;
            canDoubleJump = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Grounds"))
        {
            isGrounded = false;
            canDoubleJump = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Collectable"))
        {
            score += 10;
            scoreText.text = "P1 - " + PlayerPrefs.GetString("p1Selection") + ": " + score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }


}
