using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody body;
    GameObject pointer;
    GameObject mouseCast;
    [SerializeField] LayerMask rayCastLayer;
    [SerializeField] Camera cam;
    Vector2 moveInput;
    float moveSpeed = 5f;
    bool facingRight = true;
    Vector2 mousePos;


    /*[field: SerializeField]
    public PlayerElement Elem1 {get; private set;}

    [field: SerializeField]
    public PlayerElement Elem2 {get; private set;} 

    [field: SerializeField]
    public PlayerElement Elem3 {get; private set;}*/

    void Start()
    {
        body = GetComponent<Rigidbody>();
        pointer = transform.GetChild(0).gameObject;
        mouseCast = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Run();

        Aim();
    }

    void Aim()
    {
        /*mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bodyPosV2 = new Vector2(body.position.x, body.position.z);
        Vector2 lookDir = mousePos - bodyPosV2;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        pointer.GetComponent<Rigidbody>().rotation = angle;*/

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, rayCastLayer))
        {
            //Vector2 bodyPosV2 = new Vector2(body.position.x, body.position.z);
            Vector2 lookDir = raycastHit.point - body.position;
            //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            //pointer.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(lookDir, new Vector3(0, 0, 1));
            mouseCast.transform.position = raycastHit.point;
            pointer.transform.LookAt(mouseCast.transform, new Vector3(0, 1, 0));
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        body.velocity = new Vector3((moveInput.x * moveSpeed), body.velocity.y, (moveInput.y * moveSpeed));
        
        if (body.velocity.x > 0)
        {
            if (!facingRight)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
            }
            
        } else if (body.velocity.x < 0)
        {
            if (facingRight)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                facingRight = false;
            }
            
        }
        
        //gameObject.transform.localScale = new Vector3((Mathf.Abs(body.velocity.x)/body.velocity.x), 1, 1);
    }


}
