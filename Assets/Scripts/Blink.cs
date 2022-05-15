using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public float tpDistance = 5.0f;

    public float chargeTime; // How long it takes to charge the ability to full charge from max charge
    public float cooldownReset = 10f;

    private float cooldownProgress; // 0-1 how much the ability is charged up
    private bool abilityCharged; // true or false

    // Start is called before the first frame update
    void Start()
    {
        abilityCharged = true;
        cooldownProgress = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        // cooldown
        cooldownProgress = Mathf.Clamp01(cooldownProgress - (Time.deltaTime / chargeTime));

        if (cooldownProgress <= 0f)
        {

            Debug.Log("Ability Charged? " + abilityCharged);
            abilityCharged = true;
        }

        if (Input.GetKeyDown("e"))
        {
            // When fully charged and ability charged
            if (cooldownProgress <= 0f && abilityCharged == true)
            {
                // Use ability
                Teleport();
                cooldownProgress = cooldownReset;
            }
        }
        
    }

    void Teleport()
    {
        Debug.Log("Teepeed");

        cooldownProgress = cooldownReset;
        abilityCharged = false;

        #region Teleport
        RaycastHit hit;
        Vector3 destination = transform.position + transform.forward * tpDistance;

        // obsticle found to be intersecting
        if (Physics.Linecast(transform.position, destination, out hit))
        {
            destination = transform.position + transform.forward * (hit.distance + 2f);
        }

        // No obstacles found
        if (Physics.Raycast(destination, -Vector3.up, out hit))
        {
            destination = hit.point;
            transform.position = destination;
        }
        #endregion
    }
}
