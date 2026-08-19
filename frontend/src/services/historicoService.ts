import api from './api'
import type { TreinoResponse } from './treinoService'

const listar = async (): Promise<TreinoResponse[]> => {
  const response = await api.get<TreinoResponse[]>('/historico/treinos', {
    params: { pagina: 1, tamanhoPagina: 50 }
  })
  return response.data
}

const obter = async (id: number): Promise<TreinoResponse> => {
  const response = await api.get<TreinoResponse>(`/historico/treinos/${id}`)
  return response.data
}

export default {
  listar,
  obter
}
