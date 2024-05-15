import { apiClient } from "..";

export const createNewConversation = async (userId) => {
    try {
        const response = await apiClient.post(`/Context/CrateNewConversation?userId=${userId}`);
        return response.data;
    } catch (err) {
        throw Error(err.response.data)
    }
};