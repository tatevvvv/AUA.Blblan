import { apiClient } from "..";

export const createNewConversation = async () => {
    try {
        const response = await apiClient.post(`/Context/CrateNewConversation`);
        return response.data;
    } catch (err) {
        throw Error(err.response.data)
    }
};