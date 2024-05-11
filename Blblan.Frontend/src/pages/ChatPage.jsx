import { useState } from "react";
import ControlledInput from "../components/atoms/ControlledInput";
import { useMutation } from "@tanstack/react-query";

export default function ChatPage({ createConversation, conversation }) {
  const [inputValue, setInputValue] = useState("");
  // const sendMessage = useMutation({ mutationFn: sendMessage })

  const onMessageSend = () => {
    if (!conversation) {
      createConversation()
    } else {
      // sendMessage(conversation)
    }
  }

  const handleChange = (event) => {
    setInputValue(event.target.value);
  };

  return (
    <div className="content">
    <div className="output"></div>
    <div className="input-customized">
        <div className="input-wrapper">
        <i
            className={`fa-solid fa-arrow-up-long arrow-send ${
            !inputValue ? "disabled" : ""
            }`}
        />
        <ControlledInput
            value={inputValue}
            changeHandler={handleChange}
            placeholder="Message ChatGPT"
            className={`chat-input`}
            onClick={onMessageSend}
        />
        </div>
    </div>
    </div>
  );
}
