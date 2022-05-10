using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //float currentStraifeSpeed = 1;
    //float initialStraifeSpeed = 1;
    //float maxStraifeSpeed = 3;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //if(Input.GetAxis("Horizontal") == 0)
        //{
        //    Debug.Log("not moving");
        //    currentStraifeSpeed = initialStraifeSpeed;
        //}
        //else
        //{
        //    Debug.Log("moving");
        //    if (currentStraifeSpeed < maxStraifeSpeed)
        //    {

        //        currentStraifeSpeed = currentStraifeSpeed + Time.deltaTime * 5;
        //        Debug.Log("currentStraifeSpeed: " + currentStraifeSpeed);
        //    }
        //}

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);


        #region Jump Stuff

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        #endregion
    }
}
