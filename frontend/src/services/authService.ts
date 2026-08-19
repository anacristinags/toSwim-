import api from './api'
import { removerToken, salvarToken, estaAutenticado } from './tokenStorage'

export interface CadastroPayload {
  nome: string
  email: string
  senha: string
}

export interface LoginPayload {
  email: string
  senha: string
}

export interface AuthResponse {
  token: string
  nome: string
  email: string
  expiraEm: string
}

const cadastrar = async (dados: CadastroPayload): Promise<AuthResponse> => {
  const response = await api.post<AuthResponse>('/auth/registro', dados)

  if (response.data?.token) {
    salvarToken(response.data.token)
  }

  return response.data
}

const login = async (dados: LoginPayload): Promise<AuthResponse> => {
  const response = await api.post<AuthResponse>('/auth/login', dados)

  if (response.data?.token) {
    salvarToken(response.data.token)
  }

  return response.data
}

const logout = (): void => removerToken()

export default {
  cadastrar,
  login,
  logout,
  estaAutenticado
}
