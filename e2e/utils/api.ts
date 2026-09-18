import type { APIRequestContext } from '@playwright/test'
import { API_BASE, cabecalhoAuth } from './auth'

async function jsonOk(response: { ok: () => boolean; status: () => number; text: () => Promise<string>; json: () => Promise<any> }) {
  if (!response.ok()) {
    throw new Error(`Falha na API de teste: ${response.status()} ${await response.text()}`)
  }
  return response.json()
}

export async function criarConfigPiscina(
  request: APIRequestContext,
  token: string,
  dados: { tamanhoM?: number; formaContagem?: number } = {},
) {
  const response = await request.post(`${API_BASE}/piscina-configuracao`, {
    headers: cabecalhoAuth(token),
    data: {
      tamanhoM: dados.tamanhoM ?? 25,
      formaContagem: dados.formaContagem ?? 0,
    },
  })
  return jsonOk(response)
}

export async function criarFicha(
  request: APIRequestContext,
  token: string,
  tituloFicha: string,
  tipoFicha = 0,
  tamanhoPiscinaM = 25,
) {
  const response = await request.post(`${API_BASE}/fichas-base`, {
    headers: cabecalhoAuth(token),
    data: { tituloFicha, tipoFicha, tamanhoPiscinaM },
  })
  return jsonOk(response)
}

export async function adicionarSerie(
  request: APIRequestContext,
  token: string,
  idFicha: number,
  dados: {
    tipoNado?: number
    quantidadeRepeticoes?: number
    distanciaM?: number
    tempoPausaSeg?: number
    observacoes?: string
  } = {},
) {
  const response = await request.post(`${API_BASE}/fichas-base/${idFicha}/series`, {
    headers: cabecalhoAuth(token),
    data: {
      tipoNado: dados.tipoNado ?? 0,
      quantidadeRepeticoes: dados.quantidadeRepeticoes ?? 4,
      distanciaM: dados.distanciaM ?? 100,
      tempoPausaSeg: dados.tempoPausaSeg ?? 20,
      isGoalSeries: false,
      observacoes: dados.observacoes,
    },
  })
  return jsonOk(response)
}

export async function criarFichaComSerie(
  request: APIRequestContext,
  token: string,
  tituloFicha: string,
  tamanhoPiscinaM = 25,
) {
  const ficha = await criarFicha(request, token, tituloFicha, 0, tamanhoPiscinaM)
  const idFicha = ficha.codFicha ?? ficha.CodFicha
  const serie = await adicionarSerie(request, token, idFicha)
  return { ficha, serie, idFicha, idSerie: serie.codSerieFicha ?? serie.CodSerieFicha }
}

export async function criarMeta(
  request: APIRequestContext,
  token: string,
  dados: {
    codSerieFicha: number
    tituloMeta: string
    tipoNado?: number
    distanciaAlvoM?: number
    tempoAlvoSeg?: number
    modoAvaliacao?: number
  },
) {
  const response = await request.post(`${API_BASE}/metas`, {
    headers: cabecalhoAuth(token),
    data: {
      tipoNado: 0,
      distanciaAlvoM: 100,
      tempoAlvoSeg: 75,
      modoAvaliacao: 0,
      ...dados,
    },
  })
  return jsonOk(response)
}

export async function iniciarTreino(
  request: APIRequestContext,
  token: string,
  dados: { codFicha: number; tituloTreino: string; tamanhoPiscinaM?: number; observacao?: string },
) {
  const response = await request.post(`${API_BASE}/treinos`, {
    headers: cabecalhoAuth(token),
    data: {
      tamanhoPiscinaM: 25,
      ...dados,
    },
  })
  return jsonOk(response)
}

export function seriesDoTreino(treino: any): any[] {
  return treino.seriesTreino ?? treino.SeriesTreino ?? []
}

export async function concluirTreinoComPiscina(
  request: APIRequestContext,
  token: string,
  tituloTreino: string,
  tamanhoPiscinaM: number,
) {
  const { idFicha } = await criarFichaComSerie(request, token, `Ficha ${tituloTreino}`, tamanhoPiscinaM)
  const treino = await iniciarTreino(request, token, { codFicha: idFicha, tituloTreino })
  const idTreino = treino.codTreino ?? treino.CodTreino
  const serie = seriesDoTreino(treino)[0]
  const idSerie = serie.codSerieTreino ?? serie.CodSerieTreino
  const distancia = serie.distanciaPlanejadaM ?? serie.DistanciaPlanejadaM
  const duracaoSeg = 70

  await jsonOk(
    await request.post(`${API_BASE}/treinos/${idTreino}/series/${idSerie}/repeticoes`, {
      headers: cabecalhoAuth(token),
      data: { numeroRepeticao: 1, distanciaRealM: distancia, duracaoSeg },
    }),
  )

  await jsonOk(
    await request.put(`${API_BASE}/treinos/${idTreino}/series/${idSerie}`, {
      headers: cabecalhoAuth(token),
      data: {
        codSerieFicha: serie.codSerieFicha ?? serie.CodSerieFicha,
        ordem: serie.ordem ?? serie.Ordem,
        tipoNado: serie.tipoNado ?? serie.TipoNado,
        quantidadeRepeticoesPlanejada:
          serie.quantidadeRepeticoesPlanejada ?? serie.QuantidadeRepeticoesPlanejada,
        distanciaPlanejadaM: distancia,
        tempoPausaSeg: serie.tempoPausaSeg ?? serie.TempoPausaSeg,
        tempoTotalSeg: duracaoSeg,
        distanciaTotalM: distancia,
        observacoes: serie.observacoes ?? serie.Observacoes,
      },
    }),
  )

  const finalizar = await request.put(`${API_BASE}/treinos/${idTreino}/finalizar`, {
    headers: cabecalhoAuth(token),
  })
  return jsonOk(finalizar)
}

export async function concluirTreinoComTempo(
  request: APIRequestContext,
  token: string,
  tituloTreino: string,
) {
  await criarConfigPiscina(request, token)
  const { idFicha } = await criarFichaComSerie(request, token, `Ficha ${tituloTreino}`)
  const treino = await iniciarTreino(request, token, { codFicha: idFicha, tituloTreino })
  const idTreino = treino.codTreino ?? treino.CodTreino
  const serie = seriesDoTreino(treino)[0]
  const idSerie = serie.codSerieTreino ?? serie.CodSerieTreino
  const distancia = serie.distanciaPlanejadaM ?? serie.DistanciaPlanejadaM
  const duracaoSeg = 70

  const repeticao = await request.post(
    `${API_BASE}/treinos/${idTreino}/series/${idSerie}/repeticoes`,
    {
      headers: cabecalhoAuth(token),
      data: { numeroRepeticao: 1, distanciaRealM: distancia, duracaoSeg },
    },
  )
  await jsonOk(repeticao)

  const atualizarSerie = await request.put(`${API_BASE}/treinos/${idTreino}/series/${idSerie}`, {
    headers: cabecalhoAuth(token),
    data: {
      codSerieFicha: serie.codSerieFicha ?? serie.CodSerieFicha,
      ordem: serie.ordem ?? serie.Ordem,
      tipoNado: serie.tipoNado ?? serie.TipoNado,
      quantidadeRepeticoesPlanejada:
        serie.quantidadeRepeticoesPlanejada ?? serie.QuantidadeRepeticoesPlanejada,
      distanciaPlanejadaM: distancia,
      tempoPausaSeg: serie.tempoPausaSeg ?? serie.TempoPausaSeg,
      tempoTotalSeg: duracaoSeg,
      distanciaTotalM: distancia,
      observacoes: serie.observacoes ?? serie.Observacoes,
    },
  })
  await jsonOk(atualizarSerie)

  const finalizar = await request.put(`${API_BASE}/treinos/${idTreino}/finalizar`, {
    headers: cabecalhoAuth(token),
  })
  return jsonOk(finalizar)
}
