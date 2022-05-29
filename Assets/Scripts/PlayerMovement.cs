using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public GameManager gameManager;

    public float speed;
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

    public float chargeTime;
    public float cooldownReset = 10f;

    private float cooldownProgress;
    private bool abilityCharged;

    public float defaultSpeed = 3.5f;
    public float abilitySpeed;
    private float speedyTimer;
    private bool startTimer = false;

    public Camera fpsCam;
    public float maxFOV = 105f;
    public float minFOV = 90f;
    public float t = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        abilityCharged = true;
        cooldownProgress = 0f;
        speedyTimer = 0f;

        fpsCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (gameManager.isTyping == false)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Strafe lurping but at home
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
        }

        #region Jump Stuff

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        #endregion


        #region Speed Ability

        //if (Input.GetKeyDown("q"))
        //{
        //    startTimer = true;

        //    // When fully charged and ability charged
        //    if (cooldownProgress <= 0f && abilityCharged == true)
        //    {
        //        // Use ability
        //        SpeedAbility();
        //        cooldownProgress = cooldownReset;
        //    }
        //}

        // Starts the timer for how long the player has speed
        if (startTimer == true)
        {
            speedyTimer += Time.deltaTime;

            // Resets speed after timer hits 5 seconds
            if (speedyTimer >= 5)
            {
                speed = defaultSpeed;
                speedyTimer = 0;
                startTimer = false;

                fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, minFOV, t);
            }
        }
    }

    // Called from text script to activate speed
    public void ActivateMove()
    {
        // When cooldown goes to 0 and ability is charged
        if (gameManager.cooldownProgress <= 0f && gameManager.abilitiesCharged == true)
        {
            // Calls to increase speed
            SpeedAbility();
            gameManager.cooldownProgress = gameManager.cooldownReset;
            gameManager.abilitiesCharged = false;
        }
    }

    void SpeedAbility()
    {
        // Increase speed and reset timer and set charge to false
        startTimer = true;
        speed = abilitySpeed;

        fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, maxFOV, t);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, groundDistance);
    }

}
