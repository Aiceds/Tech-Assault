using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClosePlayer : MonoBehaviour
{
    public EnemyAI EnemyAI;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.tag == "Player")
        {
            EnemyAI.ChasePlayer();
        }
    }
}
