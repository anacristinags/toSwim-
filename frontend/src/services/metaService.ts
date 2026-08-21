import api from './api'

export interface MetaResponse {
  codMeta: number
  codUsuario: number
  codSerieFicha: number
  tituloMeta: string
  tipoNado: number
  distanciaAlvoM: number
  tempoAlvoSeg: number
  paceAlvoSeg: number
  modoAvaliacao: number
  status: number
  dataInicio: string
  dataFimPrevista?: string | null
  observacoes?: string | null
  createdAt: string
  updatedAt: string
}

export interface MetaPayload {
  codSerieFicha: number
  tituloMeta: string
  tipoNado: number
  distanciaAlvoM: number
  tempoAlvoSeg: number
  modoAvaliacao: number
  dataInicio?: string
  dataFimPrevista?: string | null
  observacoes?: string
}

export interface ProgressoMetaResponse {
  codMeta: number
  tituloMeta: string
  distanciaAlvoM: number
  tempoAlvoSeg: number
  paceAlvoSeg: number
  melhorTempoRealizadoSeg?: number | null
  melhorPaceRealizadoSeg?: number | null
  percentualAtingimento: number
  status?: number
  historicoTentativas?: Array<{
    data: string
    tituloTreino: string
    tipoNado: number
    distanciaM: number
    paceSeg: number
  }>
}

const listar = async (status?: number | null): Promise<MetaResponse[]> => {
  const response = await api.get<MetaResponse[]>('/metas', {
    params: status === null || status === undefined ? undefined : { status }
  })
  return response.data
}

const criar = async (dados: MetaPayload): Promise<MetaResponse> => {
  const response = await api.post<MetaResponse>('/metas', dados)
  return response.data
}

const obterProgresso = async (id: number): Promise<ProgressoMetaResponse> => {
  const response = await api.get<ProgressoMetaResponse>(`/metas/${id}/progresso`)
  return response.data
}

const alterarStatus = async (id: number, status: number): Promise<MetaResponse> => {
  const response = await api.put<MetaResponse>(`/metas/${id}/status`, null, {
    params: { status }
  })
  return response.data
}

const excluir = async (id: number): Promise<void> => {
  await api.delete(`/metas/${id}`)
}

export default {
  listar,
  criar,
  obterProgresso,
  alterarStatus,
  excluir
}
