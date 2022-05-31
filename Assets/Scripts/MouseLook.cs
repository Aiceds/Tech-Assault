using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float defaultMouseSensitivity = 100f;

    public Slider senseSlider;

    public Transform playerBody;

    float xRotation = 0f;

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        senseSlider.value = defaultMouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().isTyping == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * senseSlider.value * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * senseSlider.value * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
