import { apiClient } from "..";

export const getMessages = async (userId, conversationId) => {
    const response = await apiClient.get(`Context/GetConversationAllMessagesByUserId=?userId=${userId}&conversationId=${conversationId}`);
    console.log(response.data)
    return response.data;
};