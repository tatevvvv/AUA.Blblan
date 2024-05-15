import { useState } from "react";
import ChatSidebarItem from "../components/molecules/ChatSidebarItem";
import ChatPage from "./chat.page";
import Sidebar from "../components/organisms/Sidebar";
import { useQuery } from "@tanstack/react-query";
import { useMutation } from "@tanstack/react-query";
import { getConversations } from "../api/conversations/getConversations";
import { createNewConversation } from "../api/conversations/createNewConversation";
import { apiQueryClient } from "../api/index";
import { useNavigate } from "react-router-dom";

export default function Home() {
  const user = JSON.parse(localStorage.getItem('user'));
  const navigate = useNavigate();
  const [selectedConversationId, setSelectedConversation] = useState();
  const { data: conversations, isLoading } = useQuery({
    queryKey: ['conversations', user.id],
    queryFn: () => getConversations(user.id),
  });

  const newConversation = useMutation({
    mutationFn: () => createNewConversation(user.id),
    onSuccess: (data) => {
      apiQueryClient.setQueryData(['conversations', user.id], (oldConversations) => [
        ...oldConversations, data,
      ]);
      setSelectedConversation(data.id)
    },
  });

  const logOut = () => {
    localStorage.removeItem('accessToken');
    navigate('/login');
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="chat-content">
      <Sidebar>
        <div>
          <ChatSidebarItem
            text="New chat"
            iconLeft={
              <img
                alt="plus-icon"
                style={{ width: "18.66px", height: "18.66px" }}
                src="/icons/plus-svgrepo-com.svg"
                onClick={() => newConversation.mutate()}
              />
            }
            iconRight={
              <img
                alt="new-page-icon"
                src="/icons/pen-note.svg"
                style={{ width: "18.66px", height: "18.66px" }}
              />
            }
          />
          {conversations && conversations.map(item => (
            <ChatSidebarItem
              key={item.id}
              text={item.name}
              isSelected={item.id === selectedConversationId}
              onClick={() => { setSelectedConversation(item.id); }}
            />
          ))}
        </div>
        <div>
          <ChatSidebarItem
            text={`Log out: ${JSON.parse(localStorage.getItem('user')).userName}`}
            onClick={logOut}
            iconLeft={
              <img
                alt="logout-icon"
                src="/icons/logout-svgrepo-com.svg"
                style={{ width: "18.66px", height: "18.66px" }}
              />
            }
          />
        </div>
      </Sidebar>
      <ChatPage conversationId={selectedConversationId} setSelectedConversation={setSelectedConversation} />
    </div>
  );
}