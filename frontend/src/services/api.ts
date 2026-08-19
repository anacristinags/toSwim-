import axios from 'axios'
import router from '@/router'
import { obterToken, removerToken } from './tokenStorage'

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    headers: {
        'Content-Type': 'application/json'
    }
})

api.interceptors.request.use((config) => {
    const token = obterToken()

    if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`
    }

    return config
})

api.interceptors.response.use(
    (response) => response,
    (error) => {
        const naoAutenticado = error.response?.status === 401

        if (naoAutenticado && router.currentRoute.value.name !== 'login') {
            removerToken()
            router.push({ name: 'login' })
        }

        return Promise.reject(error)
    }
)

export default api
