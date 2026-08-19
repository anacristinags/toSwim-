import axios from 'axios'
import api from './api'

export interface ConfigPiscinaPayload {
    tamanhoM: number
    formaContagem: number
}

export interface ConfigPiscinaResponse {
    codPiscina: number
    codUsuario: number
    tamanhoM: number
    formaContagem: number
    status: number
    createdAt: string
    updatedAt: string
}

const criarConfiguracao = async (dados: ConfigPiscinaPayload): Promise<ConfigPiscinaResponse> => {
    const response = await api.post<ConfigPiscinaResponse>('/piscina-configuracao', dados)
    return response.data
}

const atualizarConfiguracao = async (dados: ConfigPiscinaPayload): Promise<ConfigPiscinaResponse> => {
    const response = await api.put<ConfigPiscinaResponse>('/piscina-configuracao/me', dados)
    return response.data
}

// Retorna null quando o atleta ainda nao possui configuracao cadastrada.
const obterConfiguracao = async (): Promise<ConfigPiscinaResponse | null> => {
    try {
        const response = await api.get<ConfigPiscinaResponse>('/piscina-configuracao/me')
        return response.data
    } catch (error) {
        if (axios.isAxiosError(error) && error.response?.status === 404) {
            return null
        }

        throw error
    }
}

export default {
    criarConfiguracao,
    atualizarConfiguracao,
    obterConfiguracao
}
