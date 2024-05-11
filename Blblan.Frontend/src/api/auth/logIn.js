import { apiClient } from "..";

export const logIn = async (data) => {
    try {
        const response = await apiClient.post(`/auth/login`, data);
        return response.data;
    } catch (err) {
        throw Error(err.response.data)
    }
};