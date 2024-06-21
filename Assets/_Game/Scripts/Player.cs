using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Transform tf;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask ground;

    private bool isPausing;

    FloatingJoystick Joystick
    {
        get
        {
            if (joystick == null)
            {
                joystick = FindObjectOfType<FloatingJoystick>();
            }
            return joystick;
        }
    }


    public void Initialize()
    {
        meshRenderer.enabled = true;
        rb.position = Vector3.zero;
        StopMovement();
        ClearBrick();
    }
    
    private void Update()
    {
        HandleMovement();
    }
    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }
    protected override void GameManager_OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Pausing)
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
        if (Joystick == null) return;
        if (Mathf.Abs(Joystick.Horizontal) > 0.1f || Mathf.Abs(Joystick.Vertical) > 0.1f)
        {
            rb.velocity = new Vector3(speed * Joystick.Horizontal, -Mathf.Abs(rb.velocity.y), speed * Joystick.Vertical);
            tf.rotation = Quaternion.LookRotation(rb.velocity);
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
        return Physics.Raycast(transform.position + new Vector3(0f, Constants.CHARACTER_HEIGHT / 2, 0f), -transform.up, Constants.CHARACTER_HEIGHT / 2 + 0.4f, ground);
    }
}
