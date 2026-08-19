import api from './api'

export interface SerieFichaResponse {
  codSerieFicha: number
  codFicha: number
  ordem: number
  tipoNado: number
  quantidadeRepeticoes: number
  distanciaM: number
  tempoPausaSeg: number
  isGoalSeries: boolean
  observacoes?: string | null
  createdAt: string
  updatedAt: string
}

export interface FichaBaseResponse {
  codFicha: number
  codUsuario: number
  tituloFicha: string
  tipoFicha: number
  status: number
  fichaCopiada?: number | null
  createdAt: string
  updatedAt: string
  series: SerieFichaResponse[]
}

export interface FichaBasePayload {
  tituloFicha: string
  tipoFicha: number
}

export interface SerieFichaPayload {
  ordem: number
  tipoNado: number
  quantidadeRepeticoes: number
  distanciaM: number
  tempoPausaSeg: number
  isGoalSeries?: boolean
  observacoes?: string
}

const listar = async (): Promise<FichaBaseResponse[]> => {
  const response = await api.get<FichaBaseResponse[]>('/fichas-base')
  return response.data
}

const obter = async (id: number): Promise<FichaBaseResponse> => {
  const response = await api.get<FichaBaseResponse>(`/fichas-base/${id}`)
  return response.data
}

const criar = async (dados: FichaBasePayload): Promise<FichaBaseResponse> => {
  const response = await api.post<FichaBaseResponse>('/fichas-base', dados)
  return response.data
}

const duplicar = async (id: number): Promise<FichaBaseResponse> => {
  const response = await api.post<FichaBaseResponse>(`/fichas-base/${id}/duplicar`)
  return response.data
}

const excluir = async (id: number): Promise<void> => {
  await api.delete(`/fichas-base/${id}`)
}

const adicionarSerie = async (
  idFicha: number,
  dados: SerieFichaPayload
): Promise<SerieFichaResponse> => {
  const response = await api.post<SerieFichaResponse>(
    `/fichas-base/${idFicha}/series`,
    dados
  )
  return response.data
}

const excluirSerie = async (idSerie: number): Promise<void> => {
  await api.delete(`/fichas-base/series/${idSerie}`)
}

export default {
  listar,
  obter,
  criar,
  duplicar,
  excluir,
  adicionarSerie,
  excluirSerie
}
