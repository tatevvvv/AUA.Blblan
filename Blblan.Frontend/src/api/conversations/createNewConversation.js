import { apiClient } from "..";

export const createNewConversation = async (id) => {
    try {
        const response = await apiClient.post(`/Context/CrateNewConversation?userId=${id}`);
        return response.data;
    } catch (err) {
        throw Error(err.response.data)
    }
};