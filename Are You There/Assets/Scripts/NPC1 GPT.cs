using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

using PlayerScore;
using testFlaskClientCS;



namespace OpenAI
{
   
    public class NPC1GPT : MonoBehaviour
    {
    
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        public UnityEvent OnReplyReceived;

        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "You are a male NPC cashier working at a restaurant, also introducing yourself as a language instructor. You ask the user to specify their proficiency level in a target language (e.g., Hindi). Based on the user's response (beginner, intermediate, advanced), tailor the language instruction accordingly, focusing on relevant restaurant scenarios and vocabulary. If user is a beginner, Reply using only English script. Give pronounciation of hindi words using english script. If you receive input in Hindi script, give the correct pronunciation in English script and include the Hindi script in brackets. If user is intermediate, use a mix of both hindi and english script to reply. If user is advanced, use only Hindi Scripts. Correct all user grammatical and pronunciation errors in every reply. Limit your responses to 100 words. Maintain character throughout the interaction and refrain from mentioning that you are an AI model.";

        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()
        {
            
    
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = Hindi.output
            };  

            if (string.IsNullOrEmpty(Hindi.output))
            {
                newMessage = new ChatMessage()
                {
                    Role = "user",
                    Content = inputField.text
                };
            }
            Hindi.output = "";
            
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0125",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                OnReplyReceived.Invoke();
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}