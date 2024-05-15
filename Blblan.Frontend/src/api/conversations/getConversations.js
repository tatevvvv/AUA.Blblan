import { apiClient } from "..";

export const getConversations = async (id) => {
    const response = await apiClient.get(`/Context/GetAllConversations?userId=${id}`);
    return response.data;
};