import { useState } from "react";
import ControlledInput from "../components/atoms/ControlledInput";
import { sendMessage } from "../api/conversations/sendMessage";
import { getMessages } from "../api/conversations/getMessages";
import { useMutation } from "@tanstack/react-query";
import { createNewConversation } from "../api/conversations/createNewConversation";
import { useQuery } from "@tanstack/react-query";
import { apiQueryClient } from "../api/index"
import { FormControl, InputLabel, Select, MenuItem } from "@mui/material";


const user = JSON.parse(localStorage.getItem('user'))

export default function ChatPage({ conversationId, setSelectedConversation }) {
  const [message, setMessage] = useState("");
  const [model, setModel] = useState(1)
  const sendMessageRequest = useMutation({
    mutationFn: sendMessage, onSuccess: (data) => {
      apiQueryClient.setQueryData(['messages', conversationId], (oldMessages) => ([...oldMessages, { question: '', answer: data.content }]))
    }, onMutate: (data) => {
      apiQueryClient.setQueryData(['messages', conversationId], (oldMessages) => ([...oldMessages, { question: data.message }]))
    }
  })
  const { data: messages } = useQuery({ queryKey: ['messages', conversationId], queryFn: () => getMessages(user.id, conversationId), enabled: !!conversationId })

  const newConversation = useMutation({
    mutationFn: () => createNewConversation(user.id),
    onSuccess: (data) => {
      apiQueryClient.setQueryData(['conversations', user.id], (oldConversations) => ([...oldConversations, data]))
      setSelectedConversation(data.id)
      sendMessageRequest.mutate({ userId: user.id, contextId: data.id, message: message, modelType: model })
    }
  })

  const onSelectModel = (e) => {
    setModel(e.target.value)
  }

  const onMessageSend = () => {
    if (!conversationId) {
      newConversation.mutate(user.id)
    } else {
      sendMessageRequest.mutate({ userId: user.id, contextId: conversationId, message: message, modelType: model })
      setMessage('')
    }
  }

  const handleChange = (event) => {
    console.log('event :>> ', event);
    if (event.key === 'Enter') {
      onMessageSend()
    }
    setMessage(event.target.value);
  };

  return (
    <div className="content">
      <div className="output">
        {
          messages && messages.map(message => (
            <>
              <div key={message.timestamp} className="message user-message">
                {message.question}
              </div>
              <div key={message.timestamp} className="message bot-message">
                {message.answer}
              </div>
            </>
          ))
        }
        {sendMessageRequest.isPending ? '...' : ''}
      </div>
      <div className="input-customized">
        <div className="input-wrapper">
          <i
            className={`fa-solid fa-arrow-up-long arrow-send ${!message ? "disabled" : ""
              }`}
            onClick={onMessageSend}
          />
          <ControlledInput
            value={message}
            changeHandler={handleChange}
            placeholder="Message the AI"
            className={`chat-input`}
            onKeyDown={e => e.key === 'Enter' ? onMessageSend() : null}
          />
        </div>
        <FormControl>
          <InputLabel id="demo-simple-select-label">Model</InputLabel>
          <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            value={model}
            label="Choose your companion"
            onChange={onSelectModel}
          >
            <MenuItem value={1}>Fast</MenuItem>
            <MenuItem value={2}>Wise</MenuItem>
          </Select>
        </FormControl>
      </div>
    </div>
  );
}
