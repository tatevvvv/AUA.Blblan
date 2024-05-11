import { apiClient } from "..";

export const signUp = async (data) => {
    try {
        const response = await apiClient.post(`/auth/register`, data);
        return response.data;
    } catch (err) {
        throw Error(err.response.data)
    }
};