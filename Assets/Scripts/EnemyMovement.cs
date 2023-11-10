using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int detectRange = 5; // range in which enemy can detect the player
    bool hasDetected = false;
    float distanceToPlayer = 50f;
    Rigidbody body;
    GameObject player;
    Rigidbody playerBody;

    Animator animator;

    bool facingRight = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        }
        if (distanceToPlayer < detectRange)
        {
            hasDetected = true;
        }
        FollowPlayer();
        IsRunning();
        HandleFlipScale();
    }

    void FollowPlayer()
    {
        if (player && hasDetected)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            if (distanceToPlayer > 1f)
            {
                body.velocity = new Vector3(direction.x, direction.y, direction.z) * moveSpeed;
            }
            
        }
    }

    void IsRunning()
    {
        if (Mathf.Abs(body.velocity.x) > 1 || Mathf.Abs(body.velocity.z) > 1)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void HandleFlipScale()
    {
        if (body.velocity.x < 0)
        {
            if (!facingRight)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
            }
            
        } else if (body.velocity.x > 0)
        {
            if (facingRight)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                facingRight = false;
            }
            
        }
    }
}
