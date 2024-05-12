import { useState } from "react";
import ControlledInput from "../components/atoms/ControlledInput";
import { sendMessage } from "../api/conversations/sendMessage"; 
import { getMessages } from "../api/conversations/getMessages"; 
import { useMutation } from "@tanstack/react-query";
import { useQuery } from "@tanstack/react-query";

export default function ChatPage({ createConversation, conversationId }) {
  const [inputValue, setInputValue] = useState("");
  const sendMessageRequest = useMutation({ mutationFn: () => sendMessage(conversationId) })
  const { data: messages }  = useQuery({ queryKey: ['messages'], queryFn: () => getMessages(1, conversationId) })

  const onMessageSend = () => {
    if (!conversationId) {
      conversationId = createConversation.mutate(1)
    }
    sendMessageRequest.mutate(conversationId)
  }

  const handleChange = (event) => {
    setInputValue(event.target.value);
  };

  return (
    <div className="content">
    <div className="output"></div>
    <div className="input-customized">
      <div className="output">
          {messages && messages.map(message => (
          <div key={message.id} className={`message ${message.sender === 'user' ? 'user-message' : 'bot-message'}`}>
          {message.text}
          </div>
        ))}
      </div>
        <div className="input-wrapper">
        <i
            className={`fa-solid fa-arrow-up-long arrow-send ${
            !inputValue ? "disabled" : ""
            }`}
            onClick={onMessageSend}
        />
        <ControlledInput
            value={inputValue}
            changeHandler={handleChange}
            placeholder="Message ChatGPT"
            className={`chat-input`}
        />
        </div>
    </div>
    </div>
  );
}
