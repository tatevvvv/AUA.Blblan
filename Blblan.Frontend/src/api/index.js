import {
    QueryClient,
    QueryClientProvider
} from '@tanstack/react-query'
import axios from 'axios'

export const apiClient = axios.create({
    baseURL: 'http://localhost:5198/api/',
    responseType: 'json',
    timeout: 5000,
})

export const apiQueryClient = new QueryClient()

export const ApiProvider = ({ children }) => <QueryClientProvider client={apiQueryClient}>
    {children}
</QueryClientProvider>