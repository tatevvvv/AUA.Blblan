import { apiClient } from "..";

export const getConversations = async (id) => {
    const response = await apiClient.get(`/Context/GetAllConversations?userId=${id}`);
    console.log(response.data)
    return response.data;
};