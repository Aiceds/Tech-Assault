using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCollider : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Lost game");
            gameManager.LoseGame();
        }
    }
}
