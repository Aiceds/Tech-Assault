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

    public bool isTyping = false;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
                    if (chatBox.text == "/dash")
                    {
                        player.GetComponent<Blink>().startTeleport();
                    }

                    if (chatBox.text == "/move")
                    {
                        player.GetComponent<PlayerMovement>().ActivateMove();
                    }

                    if (chatBox.text == "/r")
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
    }

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
    #endregion
}
