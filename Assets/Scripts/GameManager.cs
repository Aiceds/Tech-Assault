using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                    //check if its an ability
                    if(chatBox.text == "test")
                    {
                        Debug.Log("test typed");
                        player.GetComponent<Blink>().startTeleport();
                    }
                    else
                    {
                        Debug.Log("not typed");
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
