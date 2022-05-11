using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootButBetter : MonoBehaviour
{
    public float chargeTime; // How long it takes to charge the gun to full charge from 0
    public float drainTime; // How long it would take to drain to 0 from full charge

    public Transform gunEnd;
    public float weaponRange = 50f;
    public float hitForce = 100f;

    private float chargeProgress; // 0-1 how much the gun is charged up
    private bool hasFiredOnThisCharge; // If the player is holding the button, whether the gun has shot yet

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private LineRenderer laserLine;
    //private AudioSource gunAudio;


    // Start is called before the first frame update
    void Start()
    {
        chargeProgress = 0f;
        hasFiredOnThisCharge = false;

        laserLine = GetComponent<LineRenderer>();
        fpsCam = GetComponentInParent<Camera>();
        //gunAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Charge Progress" + chargeProgress);

        if (Input.GetButton("Fire1"))
        {
            // Charge up weapon
            chargeProgress = Mathf.Clamp01(chargeProgress + (Time.deltaTime / chargeTime));

            // When fully charged and not fired on this charge yet
            if (chargeProgress >= 1f && hasFiredOnThisCharge == false)
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

    void Shoot()
    {
        // Stop the gun from shooting multiple times on one charge
        hasFiredOnThisCharge = true;
        Debug.Log("We fired the gun!");

        StartCoroutine(ShotEffect());

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }

    }

    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}