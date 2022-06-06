using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraShake;

public class ShootButBetter : MonoBehaviour
{
    public float chargeTime; // How long it takes to charge the gun to full charge from 0
    public float drainTime; // How long it would take to drain to 0 from full charge

    public GameObject player;
    Vector3 lastPos;
    private bool isNotMoving;

    public Transform gunEnd;
    public float weaponRange = 50f;
    public float hitForce = 100f;

    public LayerMask hitLayers;

    private float chargeProgress; // 0-1 how much the gun is charged up
    private bool hasFiredOnThisCharge; // If the player is holding the button, whether the gun has shot yet

    //private bool fireLetGo;

    public Slider chargeSlider;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private LineRenderer laserLine;

    public AudioSource weaponShotAudio;
    //public AudioSource chargeAudio;
    //public int startingPitch = 4;

    public GameObject sparks;

    // Start is called before the first frame update
    void Start()
    {
        chargeProgress = 0f;
        hasFiredOnThisCharge = false;
        //fireLetGo = true;

        laserLine = GetComponent<LineRenderer>();
        fpsCam = GetComponentInParent<Camera>();

        //chargeAudio.pitch = startingPitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFiredOnThisCharge == false)
        {
            if (Input.GetButton("Fire1"))
            {
                // Charge up weapon
                chargeProgress = Mathf.Clamp01(chargeProgress + (Time.deltaTime / chargeTime));

                // When fully charged and not fired on this charge yet
                if (chargeProgress >= 1f && isNotMoving)
                {
                    // Shoot the gun
                    Shoot();
                    chargeProgress = 0f;
                }
            }
            else
            {
                // Drain weapon charge
                chargeProgress = Mathf.Clamp01(chargeProgress - (Time.deltaTime / drainTime));

                // After firing and releasing, set charge back to 0 and allow more shooting
                if (hasFiredOnThisCharge)
                {
                    chargeProgress = 0f;
                    hasFiredOnThisCharge = false;
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            hasFiredOnThisCharge = false;
        }

        if (player.transform.position != lastPos)
        {
            isNotMoving = false;
        }
        else
        {
            isNotMoving = true;
        }
        lastPos = player.transform.position;

        chargeSlider.value = chargeProgress;
    }

    void Shoot()
    {
        // Stop the gun from shooting multiple times on one charge
        hasFiredOnThisCharge = true;

        StartCoroutine(ShotEffect());

        CameraShaker.Presets.ShortShake3D();

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, hitLayers))
        {
            laserLine.SetPosition(1, hit.point);

            // Find the line from the gun to the point that was clicked.
            Vector3 incomingVec = hit.point - rayOrigin;
            // Use the point's normal to calculate the reflection vector.
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            // Create sparks looking in the reflected direction
            Instantiate(sparks, hit.point, Quaternion.LookRotation(reflectVec));


            EnemyAI enemyScript = hit.collider.GetComponentInParent<EnemyAI>();

            // If there is an enemy script exists on the perent object that was hit
            if (enemyScript != null)
            {
                enemyScript.TakeDamage();
            }
        }
        else
        {
            // Shoots a laser even when it doesn't hit a object
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }

    }

    private IEnumerator ShotEffect()
    {
        weaponShotAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}