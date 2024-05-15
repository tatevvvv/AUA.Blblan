import { useState, useEffect } from "react";
import ControlledInput from "../components/atoms/ControlledInput";
import { sendMessage } from "../api/conversations/sendMessage";
import { getMessages } from "../api/conversations/getMessages";
import { useMutation, useQuery } from "@tanstack/react-query";
import { createNewConversation } from "../api/conversations/createNewConversation";
import { apiQueryClient } from "../api/index";
import { FormControl, InputLabel, Select, MenuItem } from "@mui/material";

export default function ChatPage({ conversationId, setSelectedConversation }) {
  const user = JSON.parse(localStorage.getItem('user'));
  const [message, setMessage] = useState("");
  const [model, setModel] = useState(1);

  // Clear messages when conversationId changes
  useEffect(() => {
    if (conversationId) {
      apiQueryClient.invalidateQueries(['messages', conversationId]);
    }
  }, [conversationId]);

  const sendMessageRequest = useMutation({
    mutationFn: sendMessage,
    onSuccess: (data, variables) => {
      apiQueryClient.setQueryData(['messages', variables.contextId], (oldMessages) => [
        ...oldMessages.slice(0, -1), // Remove the temporary message
        { question: variables.message, answer: data.content }
      ]);
      setMessage("");
    },
    onMutate: (variables) => {
      apiQueryClient.setQueryData(['messages', variables.contextId], (oldMessages) => [
        ...oldMessages ?? [],
        { question: variables.message, answer: '...' }
      ]);
    },
  });

  const { data: messages } = useQuery({
    queryKey: ['messages', conversationId],
    queryFn: () => getMessages(user.id, conversationId),
    enabled: !!conversationId,
  });

  const newConversation = useMutation({
    mutationFn: () => createNewConversation(user.id),
    onSuccess: (data) => {
      apiQueryClient.setQueryData(['conversations', user.id], (oldConversations) => [
        ...oldConversations,
        data
      ]);
      setSelectedConversation(data.id);
      apiQueryClient.setQueryData(['messages', data.id], []);
      sendMessageRequest.mutate({
        userId: user.id,
        contextId: data.id,
        message: message,
        modelType: model,
      });
    },
  });

  const onSelectModel = (e) => {
    setModel(e.target.value);
  };

  const onMessageSend = () => {
    if (!message.trim()) return;
    if (!conversationId) {
      newConversation.mutate();
    } else {
      sendMessageRequest.mutate({
        userId: user.id,
        contextId: conversationId,
        message: message,
        modelType: model,
      });
    }
  };

  const handleChange = (event) => {
    setMessage(event.target.value);
  };

  return (
    <div className="content">
      <div className="output">
        {messages &&
          messages.map((message, index) => (
            <div key={index} className="message">
              <div className="user-message">{message.question}</div>
              <div className="bot-message">{message.answer}</div>
            </div>
          ))}
        {sendMessageRequest.isPending ? '...' : ''}
      </div>
      <div className="input-customized">
        <div className="input-wrapper">
          <i
            className={`fa-solid fa-arrow-up-long arrow-send ${!message ? "disabled" : ""}`}
            onClick={onMessageSend}
          />
          <ControlledInput
            value={message}
            changeHandler={handleChange}
            placeholder="Message the AI"
            className={`chat-input`}
            onKeyDown={(e) => e.key === 'Enter' && onMessageSend()}
          />
        </div>
        <FormControl sx={{ minWidth: 120 }}>
          <InputLabel
            id="model-select-label"
            sx={{ color: 'primary.main' }}
          >
            Model
          </InputLabel>
          <Select
            labelId="model-select-label"
            id="model-select"
            value={model}
            label="Choose your companion"
            onChange={onSelectModel}
            sx={{
              color: 'secondary.main',
              '.MuiOutlinedInput-notchedOutline': {
                borderColor: 'primary.main',
              },
              '&:hover .MuiOutlinedInput-notchedOutline': {
                borderColor: 'secondary.main',
              },
              '.MuiSelect-icon': {
                color: 'secondary.main',
              },
            }}
          >
            <MenuItem value={1} sx={{ color: 'primary.main' }}>Basic</MenuItem>
            <MenuItem value={2} sx={{ color: 'primary.main' }}>Premium</MenuItem>
          </Select>
        </FormControl>
      </div>
    </div>
  );
}
  