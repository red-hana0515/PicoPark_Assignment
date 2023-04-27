using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class PlayerMovementTest : NetworkBehaviour
{
    private Camera mainCamera;

    [SyncVar]
    private float horizontal;

    [SyncVar]
    private Vector2 movement;

    [SyncVar]
    private Vector3 localScale;

    [SyncVar]
    private Vector3 playerNameScale;

    private float speed = 8f;
    private float jumpingPower = 16f;

    [SyncVar]
    public bool isFacingRight = true;

    [SyncVar]
    public bool isGrounded;

    public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Canvas playerName;
    [SerializeField] private PlayerTimer timerBool;

    #region server
    [Command]
    private void CmdMove(Vector2 position)
    {
        rb.velocity = position;
    }

    private void CmdJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    [Command]
    public void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            playerNameScale = playerName.transform.localScale;
            playerNameScale.x *= -1f;
            playerName.transform.localScale = playerNameScale;
            Debug.Log("Flipped");
        }
    }
    #endregion

    #region client

    //start method for the client who owns the object
    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }

    public override void OnStartLocalPlayer()
    {
        mainCamera.GetComponent<CameraFollow>().setTarget(gameObject.transform);
    }

    [ClientCallback] //created as a client only update function (all clients will call it not the server)
    private void Update()
    {
        if (!isOwned)
            return;


        horizontal = Input.GetAxisRaw("Horizontal");
        CmdJump();
        Flip();

        if (timerBool.mustStop)
        {
            if (rb.velocity.x != 0 || rb.velocity.y != 0)
            {
                gameObject.transform.position = new Vector2(0, 0);
            }
        }
    }

    [ClientCallback]
    private void FixedUpdate()
    {
        if (!isOwned)
            return;

        movement = new Vector2(horizontal * speed, rb.velocity.y);
        CmdMove(movement);
    }



    #endregion
}



