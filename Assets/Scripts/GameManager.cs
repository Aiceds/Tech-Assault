using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int maxMessages = 3;

    public GameObject chatPanel, textObject;
    public InputField chatBox;
    GameObject player;
    public GameObject enemy;

    public bool isTyping = false;

    public float chargeTime;
    public float cooldownReset = 10f;
    public bool abilitiesCharged;
    public float cooldownProgress;

    private bool startTimer = false;
    private float speedyTimer;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        abilitiesCharged = true;
        cooldownProgress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        #region Chat Box
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (chatBox.text == " ")
                {
                    chatBox.text = "";
                    isTyping = false;
                }
                else
                {
                    SendMessageToChat("> " + chatBox.text);

                    //Activate ability after correct input
                    if (chatBox.text.ToLower() == "/blink")
                    {
                        player.GetComponent<Blink>().startTeleport();
                    }

                    if (chatBox.text.ToLower() == "/speed")
                    {
                        player.GetComponent<PlayerMovement>().ActivateMove(); 
                    }

                    if (chatBox.text.ToLower() == "/walls")
                    {
                        WallsAbility();
                    }

                    if (chatBox.text.ToLower() == "/r")
                    {
                        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
                    }

                    chatBox.text = "";
                    isTyping = false;
                }
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
                isTyping = true;
                chatBox.text = " ";
            }
        }
        #endregion

        #region Abilities Cooldown

        // Abilities cooldown
        cooldownProgress = Mathf.Clamp01(cooldownProgress - (Time.deltaTime / chargeTime));

        if (cooldownProgress <= 0f)
        {
            abilitiesCharged = true;
        }
        #endregion

        #region Walls Ability timer
        if (startTimer == true)
        {
            speedyTimer += Time.deltaTime;

            // Resets speed after timer hits 5 seconds
            if (speedyTimer >= 8)
            {
                enemy.GetComponentInChildren<Outline>().enabled = false;
                speedyTimer = 0;
                startTimer = false;
            }
        }
    }

    void WallsAbility()
    {
        enemy.GetComponentInChildren<Outline>().enabled = true;
        startTimer = true;
    }
    #endregion

    public void SendMessageToChat(string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }

    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;
    }
    
}
