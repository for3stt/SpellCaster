using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //[Range(1, 3)]
    //[SerializeField] int enemyType = 1;
    [Header("If applicable")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashTime = 0.25f;

    Rigidbody body;
    //Animator animator;
    GameObject player;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player");
        
    }

    void ActivateHitbox()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    void DeactivateHitbox()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Whoop() //function to test animation event
    {
        Debug.Log("WHOOP");
    }

    void LeapToPlayer()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            body.velocity = new Vector3(direction.x, direction.y, direction.z) * dashSpeed;

            yield return null;
        }
    }
}
