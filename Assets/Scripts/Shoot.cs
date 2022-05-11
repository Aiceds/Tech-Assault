using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    private bool buttonHeld;
    public float Power;
    public float chargeUpTime = 0.5f;
    private float minChargeUp = 0f;
    public float maxChargeUp = 1f;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    //private AudioSource gunAudio;
    private LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        buttonHeld = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Power = Mathf.Clamp(Power, minChargeUp, maxChargeUp);
        Debug.Log("power level:" + Power);

        if (Input.GetButton("Fire1"))
        {
            Power += Time.deltaTime * chargeUpTime;
            buttonHeld = true;
        }

        if (Input.GetButton("Fire1") == false)
        {
            Power -= Time.deltaTime * chargeUpTime;
            buttonHeld = false;
        }

        if (buttonHeld == true && Power == maxChargeUp)
        {
            Fire();
        }
    }

    void Fire()
    {
        StartCoroutine(ShotEffect());
        Power = 0f;

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
