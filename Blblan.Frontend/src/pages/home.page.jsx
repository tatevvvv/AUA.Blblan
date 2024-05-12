import { useState } from "react";
import ChatSidebarItem from "../components/molecules/ChatSidebarItem";
import ChatPage from "./chat.page";
import Sidebar from "../components/organisms/Sidebar";
import { useQuery } from "@tanstack/react-query";
import { useMutation } from "@tanstack/react-query";
import { getConversations } from "../api/conversations/getConversations"
import { createNewConversation } from "../api/conversations/createNewConversation";
import { apiQueryClient } from "../api/index"


export default function Home() {
  const { data: conversations, isLoading }  = useQuery({ queryKey: ['conversations'], queryFn: () => getConversations(1) })
  const [selectedConversationId, setSelectedConversation] = useState();

  const newConversation = useMutation({ mutationFn: () => createNewConversation(1),
  onSuccess: (data) => {
    apiQueryClient.setQueryData(['conversations'], (oldConversations) => ([...oldConversations, data]))
  } })

  if (isLoading) {
    return <div>Loading...</div>;
  }
  return (
    <div className="chat-content">
      <Sidebar>
        <ChatSidebarItem
          iconLeft={
            <img
              style={{ width: "18.66px", height: "18.66px" }}
              src="/icons/plus-svgrepo-com.svg"
              onClick={() => newConversation.mutate()}
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
          onClick={() => {setSelectedConversation(item.id); console.log(selectedConversationId)}}
        />
        ))}
      </Sidebar>
      <ChatPage createConversation={createNewConversation} conversationId={selectedConversationId}></ChatPage>
    </div>
  );
}
