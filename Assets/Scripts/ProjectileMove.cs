using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ProjectileMove : MonoBehaviour
{
    public GameManager gameManager;

    public float speed;
    public GameObject sparks;

    //Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }

        //if (transform.position != lastPos)
        //{
        //    lastpos = new Vector3 (transform.position);
        //}

        //lastPos = transform.position;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // Calculating the instantiate rotation for sparks
            // Find the line from the gun to the point that was clicked.
            //Vector3 incomingVec = transform.position - lastPos;
            // Use the point's normal to calculate the reflection vector.
            //Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            // Create sparks looking in the reflected direction
            //Instantiate(sparks, transform.position, Quaternion.LookRotation(reflectVec));

            Destroy(gameObject);
        }
    }
}
