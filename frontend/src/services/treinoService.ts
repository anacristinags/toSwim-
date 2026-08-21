import api from './api'

export interface SerieTreinoResponse {
  codSerieTreino: number
  codTreino: number
  codSerieFicha?: number | null
  codMeta?: number | null
  ordem: number
  tipoNado: number
  quantidadeRepeticoesPlanejada: number
  distanciaPlanejadaM: number
  tempoPausaSeg: number
  tempoTotalSeg?: number | null
  distanciaTotalM?: number | null
  paceMedioSeg?: number | null
  observacoes?: string | null
  createdAt: string
  updatedAt: string
}

export interface TreinoResponse {
  codTreino: number
  codUsuario: number
  codFicha: number
  tituloTreino: string
  observacao?: string | null
  distanciaTotalM: number
  duracaoTotalSeg: number
  paceMedioSeg?: number | null
  tamanhoPiscinaM: number
  dataTreino: string
  status: number
  createdAt: string
  updatedAt: string
  seriesTreino: SerieTreinoResponse[]
}

export interface IniciarTreinoPayload {
  codFicha: number
  tituloTreino?: string
  observacao?: string
  tamanhoPiscinaM: number
}

export interface SerieTreinoPayload {
  codSerieFicha?: number | null
  codMeta?: number | null
  ordem: number
  tipoNado: number
  quantidadeRepeticoesPlanejada: number
  distanciaPlanejadaM: number
  tempoPausaSeg: number
  tempoTotalSeg?: number | null
  distanciaTotalM?: number | null
  observacoes?: string | null
}

export interface RepeticaoPayload {
  numeroRepeticao: number
  distanciaRealM: number
  duracaoSeg: number
}

export interface RepeticaoResponse {
  codRepeticaoSerieTreino: number
  codSerieTreino: number
  numeroRepeticao: number
  distanciaRealM: number
  duracaoSeg: number
  paceSeg?: number | null
  createdAt: string
}

const iniciar = async (dados: IniciarTreinoPayload): Promise<TreinoResponse> => {
  const response = await api.post<TreinoResponse>('/treinos', dados)
  return response.data
}

const listar = async (status?: number): Promise<TreinoResponse[]> => {
  const response = await api.get<TreinoResponse[]>('/treinos', {
    params: { status, pagina: 1, tamanhoPagina: 50 }
  })
  return response.data
}

const obter = async (id: number): Promise<TreinoResponse> => {
  const response = await api.get<TreinoResponse>(`/treinos/${id}`)
  return response.data
}

const atualizar = async (
  id: number,
  dados: { tituloTreino: string; observacao?: string; tamanhoPiscinaM: number }
): Promise<TreinoResponse> => {
  const response = await api.put<TreinoResponse>(`/treinos/${id}`, dados)
  return response.data
}

const finalizar = async (id: number): Promise<TreinoResponse> => {
  const response = await api.put<TreinoResponse>(`/treinos/${id}/finalizar`)
  return response.data
}

const cancelar = async (id: number): Promise<TreinoResponse> => {
  const response = await api.put<TreinoResponse>(`/treinos/${id}/cancelar`)
  return response.data
}

const atualizarSerie = async (
  idTreino: number,
  idSerie: number,
  dados: SerieTreinoPayload
): Promise<SerieTreinoResponse> => {
  const response = await api.put<SerieTreinoResponse>(
    `/treinos/${idTreino}/series/${idSerie}`,
    dados
  )
  return response.data
}

const listarRepeticoes = async (
  idTreino: number,
  idSerie: number
): Promise<RepeticaoResponse[]> => {
  const response = await api.get<RepeticaoResponse[]>(
    `/treinos/${idTreino}/series/${idSerie}/repeticoes`
  )
  return response.data
}

const registrarRepeticao = async (
  idTreino: number,
  idSerie: number,
  dados: RepeticaoPayload
): Promise<RepeticaoResponse> => {
  const response = await api.post<RepeticaoResponse>(
    `/treinos/${idTreino}/series/${idSerie}/repeticoes`,
    dados
  )
  return response.data
}

const atualizarRepeticao = async (
  idTreino: number,
  idSerie: number,
  idRepeticao: number,
  dados: RepeticaoPayload
): Promise<RepeticaoResponse> => {
  const response = await api.put<RepeticaoResponse>(
    `/treinos/${idTreino}/series/${idSerie}/repeticoes/${idRepeticao}`,
    dados
  )
  return response.data
}

export default {
  iniciar,
  listar,
  obter,
  atualizar,
  finalizar,
  cancelar,
  atualizarSerie,
  listarRepeticoes,
  registrarRepeticao,
  atualizarRepeticao
}
