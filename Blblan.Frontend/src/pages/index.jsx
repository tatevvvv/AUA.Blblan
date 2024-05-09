import { useState } from "react";
import ControlledInput from "../components/atoms/ControlledInput";
import ChatSidebarItem from "../components/molecules/ChatSidebarItem";
import Sidebar from "../components/organisms/Sidebar";
import { useQuery } from "@tanstack/react-query";
import { getConversations } from "../api/conversations/getConversations"

export default function Home() {
  const [inputValue, setInputValue] = useState("");
  const { data: conversations, isLoading }  = useQuery({ queryKey: ['conversations'], queryFn: () => getConversations(1) })

  const handleChange = (event) => {
    setInputValue(event.target.value);
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }
  console.log(conversations)
  return (
    <div className="chat-content">
      <Sidebar>
        <ChatSidebarItem
          iconLeft={
            <img
              style={{ width: "18.66px", height: "18.66px" }}
              src="/icons/gpt.svg"
            />
          }
          iconRight={
            <img
              src="/icons/pen-note.svg"
              style={{ width: "18.66px", height: "18.66px" }}
            />
          }
          text="New chat"
        />
        {conversations && conversations.map(item => (
        <ChatSidebarItem
          key={item.id}
          text={item.name}
          onClick={() => console.log(item.id)}
        />
        ))}
      </Sidebar>
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
            />
          </div>
        </div>
      </div>
    </div>
  );
}
