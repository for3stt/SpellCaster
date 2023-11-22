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
    //[SerializeField] LayerMask rayCastLayer;
    int layerNumber = 7;
    int rayCastLayer;
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
        rayCastLayer = 1 << layerNumber;

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

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, rayCastLayer))
        {
            mouseCast.transform.position = raycastHit.point;
            Vector3 lookDir = new Vector3(mouseCast.transform.position.x, pointer.transform.position.y, mouseCast.transform.position.z);
            pointer.transform.LookAt(lookDir, new Vector3(0, 1, 0));
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
