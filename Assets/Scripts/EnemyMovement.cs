using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody body;
    GameObject player;
    Rigidbody playerBody;
    
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody>();

        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Vector3 direction = (player.transform.position - transform.position).normalized;
            if (distance > 1f)
            {
                float speed = moveSpeed * Time.deltaTime;
                body.velocity = new Vector3(direction.x, direction.y, direction.z) * speed;
            }
            
        }
    }
}
