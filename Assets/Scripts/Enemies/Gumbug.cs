using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class Gumbug : MonoBehaviour
{
    public float speed = 10f;

    public float wallCheckDistance = 3;

    public Transform ledgeCheck;

    public int facingDirection = 1;

    public Transform groundCheck;
    public float groundCheckRadius;

    public LayerMask whatIsGround;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (CheckIfTouchingWall() || (!CheckIfNotNearLedge() && CheckIfGrounded())) Flip();
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }

    public bool CheckIfNotNearLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, 1, whatIsGround);
    }

    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        if (CheckIfGrounded())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (CheckIfTouchingWall())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (wallCheckDistance * facingDirection), transform.position.y));
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (wallCheckDistance * facingDirection), transform.position.y));
        }

        if (CheckIfNotNearLedge())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ledgeCheck.position, new Vector2(ledgeCheck.position.x, ledgeCheck.position.y - 1));
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(ledgeCheck.position, new Vector2(ledgeCheck.position.x, ledgeCheck.position.y - 1));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Damage");
            CoreManager.DamagePlayer();
            CoreManager.KnockbackPlayer(facingDirection);

        }
        Flip();
    }

    public void Die()
    {

    }
}
