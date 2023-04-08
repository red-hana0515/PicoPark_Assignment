using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class PlayerMovementTest : NetworkBehaviour
{
    private Camera mainCamera;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;

    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    #region server
    [Command]
    private void CmdMove(Vector2 position)
    {
        rb.velocity = position;
    }

    private void CmdJump()
    {
        //if (Input.GetButtonDown("Jump") && IsGrounded())
        //{
        //   rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        //}

        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    [Command]
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //[Command]
    //private bool IsGrounded()
    //
    //    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    //}

    #endregion

    #region client

    //start method for the client who owns the object
    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }

    [ClientCallback] //created as a client only update function (all clients will call it not the server)
    private void Update()
    {
        if (!isOwned)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");
        CmdJump();
        Flip();
    }

    [ClientCallback]
    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontal * speed, rb.velocity.y);
        CmdMove(movement);
    }

    #endregion
}



