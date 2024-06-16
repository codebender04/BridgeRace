using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform raycastPoint;
    private const float PLAYER_HEIGHT = 6f;
    private bool isPausing;
    private void Start()
    {
        ChangeColor(ColorType.Green);
    }

    private void Update()
    {
        if (!isPausing)
        {
            joystick = FindObjectOfType<FloatingJoystick>();
            HandleMovement();
        }
    }
    protected override void GameManager_OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Menu)
        {
            isPausing = true;
        }
        else if (newState == GameState.Playing)
        {
            isPausing = false;
        }
    }
    private void HandleMovement()
    {
        if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
        {
            rb.velocity = new Vector3(speed * joystick.Horizontal, - Mathf.Abs(rb.velocity.y), speed * joystick.Vertical);
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            ChangeAnimation(Constants.ANIM_RUN);
        }
        else if (IsGrounded())
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            ChangeAnimation(Constants.ANIM_IDLE);
        }
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0f, PLAYER_HEIGHT / 2, 0f), -transform.up, PLAYER_HEIGHT / 2 + 0.4f, ground);
    }
}
