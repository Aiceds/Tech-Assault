using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float projectileLifetime;
    private float duration;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }

        duration = duration * Time.deltaTime;

        if (duration >= projectileLifetime)
        {
            Destroy(gameObject);
        }
    }
}
