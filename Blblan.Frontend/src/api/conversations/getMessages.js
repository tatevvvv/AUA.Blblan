import { apiClient } from "..";

export const sendMessage = async (userId, data) => {
    const response = await apiClient.post(`/Context/SendMessage?userId=${userId}`, data);
    console.log(response.data)
    return response.data;
};