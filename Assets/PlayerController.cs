using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float mouseSenesitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField] GameObject cameraHolder;

    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    Vector3 velocity;

    Rigidbody rb;

    PhotonView PV;

    public CharacterController controller;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(!PV.IsMine) return;

        //ControllerMove();
        LooK();
        Move();
        Jump();
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);


        float moveSpeed = 3f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is on the right half of the screen
            if (touch.position.x > Screen.width / 2)
            {
                transform.position += moveDir * moveSpeed * Time.deltaTime;
                return; // Exit the Update function early to avoid moving twice in a single frame
            }
        }


    }
/*
    void ControllerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (Camera.main.transform.right * x) + (Camera.main.transform.forward * z);

        controller.Move(move * walkSpeed * Time.deltaTime);

        //|| Input.GetButtonDown("joystick 1 button 0")

        if ((Input.GetButtonDown("Jump")) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }

        velocity.y += -9.81f * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
*/
    void Jump()
    {

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void LooK()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSenesitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSenesitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);


        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    public void SetGroundState(bool _grounded)
    {
        grounded = _grounded;
    }

    void FixedUpdate()
    {
        if (!PV.IsMine) return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

}
