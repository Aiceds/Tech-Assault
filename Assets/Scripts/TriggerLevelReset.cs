using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLevelReset : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);        }
    }
}
