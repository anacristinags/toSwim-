<template>
  <v-container max-width="900">
    <div class="mb-6">
      <h1 class="text-h5 font-weight-bold text-primary mb-1">
        <v-icon icon="mdi-play-circle-outline" class="mr-2"></v-icon>Execucao do Treino do Dia
      </h1>
      <p class="text-body-2 text-medium-emphasis">
        Registre seus tempos e tiros diretamente na borda da piscina.
      </p>
    </div>

    <v-alert v-if="mensagem" :type="mensagem.tipo" variant="tonal" class="mb-4" density="compact">
      {{ mensagem.texto }}
    </v-alert>

    <div v-if="carregando" class="text-center py-12">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </div>

    <template v-else>
      <v-card v-if="!treinoEmAndamento" class="pa-6 rounded-lg elevation-2 mb-6">
        <h2 class="text-subtitle-1 font-weight-bold text-primary mb-4">
          Escolha a Ficha para Treinar Hoje
        </h2>

        <v-alert
          v-if="!tamanhoPiscinaM"
          type="warning"
          variant="tonal"
          class="mb-4"
          density="compact"
        >
          Configure o tamanho da piscina antes de iniciar um treino.
          <v-btn variant="text" color="primary" density="comfortable" to="/piscina" class="ml-1">
            Ir para configuracao
          </v-btn>
        </v-alert>

        <v-alert
          v-else-if="opcoesFichas.length === 0"
          type="info"
          variant="tonal"
          class="mb-4"
          density="compact"
        >
          Nenhuma ficha disponivel. Crie uma ficha base para comecar a treinar.
        </v-alert>

        <v-select
          v-model="fichaSelecionadaId"
          label="Selecione uma Ficha Base"
          :items="opcoesFichas"
          item-title="tituloFicha"
          item-value="codFicha"
          variant="outlined"
          density="comfortable"
          class="mb-4"
          :disabled="opcoesFichas.length === 0"
          hint="Ate 5 fichas ativas"
          persistent-hint
          @update:model-value="aoSelecionarFicha"
        ></v-select>

        <v-text-field
          v-model="tituloTreino"
          label="Titulo do treino do dia"
          placeholder="Ex: Treino regenerativo"
          variant="outlined"
          density="comfortable"
          class="mb-4"
          hint="Esse nome aparece no historico"
          persistent-hint
          :disabled="!fichaSelecionadaId"
        ></v-text-field>

        <v-textarea
          v-model="observacaoTreino"
          label="Comentario (opcional)"
          placeholder="Ex: piscina cheia, foquei na pernada"
          variant="outlined"
          density="comfortable"
          rows="2"
          auto-grow
          class="mb-4"
          :disabled="!fichaSelecionadaId"
        ></v-textarea>

        <v-btn
          color="primary"
          size="large"
          block
          prepend-icon="mdi-swim"
          :disabled="!fichaSelecionadaId || !tamanhoPiscinaM"
          :loading="salvando"
          @click="iniciarTreino"
        >
          Iniciar Treino na Piscina
        </v-btn>
      </v-card>

      <div v-else-if="treinoAtivo">
        <v-card color="primary" variant="tonal" class="pa-6 rounded-lg mb-6">
          <div class="d-flex justify-space-between align-start flex-wrap ga-4">
            <div class="flex-grow-1" style="min-width: 260px;">
              <div class="text-caption text-uppercase font-weight-bold mb-2">Treino em Andamento</div>
              <v-text-field
                v-model="treinoAtivo.titulo"
                label="Titulo do treino do dia"
                variant="outlined"
                density="compact"
                hide-details
                class="mt-2"
                bg-color="surface"
              ></v-text-field>
              <v-textarea
                v-model="treinoAtivo.observacao"
                label="Comentario (opcional)"
                variant="outlined"
                density="compact"
                rows="2"
                auto-grow
                hide-details
                class="mt-3"
                bg-color="surface"
              ></v-textarea>
              <div class="text-caption mt-2">Piscina {{ treinoAtivo.tamanhoPiscinaM }}m</div>
            </div>

            <div class="d-flex ga-3">
              <v-btn color="error" variant="outlined" :loading="salvando" @click="cancelarTreino">
                Cancelar
              </v-btn>
              <v-btn
                color="success"
                prepend-icon="mdi-check-circle"
                :loading="salvando"
                @click="finalizarTreino"
              >
                Finalizar Treino
              </v-btn>
            </div>
          </div>
        </v-card>

        <v-card
          v-for="(serie, sIdx) in treinoAtivo.series"
          :key="serie.codSerieTreino"
          class="mb-6 pa-4 rounded-lg elevation-2"
        >
          <v-card-title class="d-flex justify-space-between align-center">
            <span class="text-subtitle-1 font-weight-bold text-primary">
              Serie {{ Number(sIdx) + 1 }}:
              {{ serie.repeticoes }}x {{ serie.distanciaM }}m - {{ nomeNado(serie.tipoNado) }}
            </span>
            <v-chip size="small" color="info">Pausa: {{ serie.pausaSeg }}s</v-chip>
          </v-card-title>

          <v-card-text>
            <v-row class="mt-2">
              <v-col
                v-for="rIdx in serie.repeticoes"
                :key="`${serie.codSerieTreino}-${rIdx}`"
                cols="12"
                sm="6"
                md="3"
              >
                <v-card variant="outlined" class="pa-3 text-center rounded-lg">
                  <div class="text-caption text-medium-emphasis mb-1">
                    Tiro {{ rIdx }} ({{ serie.distanciaM }}m)
                  </div>
                  <v-text-field
                    v-model.number="serie.tiros[rIdx - 1]"
                    label="Tempo (seg)"
                    type="number"
                    variant="filled"
                    density="compact"
                    hide-details
                    suffix="s"
                    min="0"
                  ></v-text-field>
                </v-card>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </div>
    </template>
  </v-container>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import fichaService, { type FichaBaseResponse } from '@/services/fichaService'
import piscinaService from '@/services/piscinaService'
import treinoService, { type TreinoResponse } from '@/services/treinoService'
import { extrairMensagemErro } from '@/services/erros'
import { nomeNado } from '@/services/formatacao'

interface SerieExecucao {
  codSerieTreino: number
  codSerieFicha?: number | null
  ordem: number
  tipoNado: number
  repeticoes: number
  distanciaM: number
  pausaSeg: number
  observacoes?: string | null
  tiros: number[]
}

interface TreinoExecucao {
  codTreino: number
  titulo: string
  observacao: string
  tamanhoPiscinaM: number
  series: SerieExecucao[]
}

const carregando = ref(false)
const salvando = ref(false)
const mensagem = ref<{ tipo: 'success' | 'error' | 'warning' | 'info'; texto: string } | null>(null)

const opcoesFichas = ref<FichaBaseResponse[]>([])
const fichaSelecionadaId = ref<number | null>(null)
const tituloTreino = ref('')
const observacaoTreino = ref('')
const tamanhoPiscinaM = ref<number | null>(null)
const treinoAtivo = ref<TreinoExecucao | null>(null)

const treinoEmAndamento = computed(() => !!treinoAtivo.value)

const mapearTreino = async (treino: TreinoResponse): Promise<TreinoExecucao> => {
  const seriesOrdenadas = [...(treino.seriesTreino ?? [])].sort((a, b) => a.ordem - b.ordem)

  const series = await Promise.all(
    seriesOrdenadas.map(async (serie) => {
      const tiros = Array.from({ length: serie.quantidadeRepeticoesPlanejada }, () => 0)

      try {
        const repeticoes = await treinoService.listarRepeticoes(
          treino.codTreino,
          serie.codSerieTreino
        )

        for (const repeticao of repeticoes) {
          const index = repeticao.numeroRepeticao - 1
          if (index >= 0 && index < tiros.length) {
            tiros[index] = Number(repeticao.duracaoSeg)
          }
        }
      } catch {
        // Se ainda nao houver repeticoes, mantem os campos vazios.
      }

      return {
        codSerieTreino: serie.codSerieTreino,
        codSerieFicha: serie.codSerieFicha,
        ordem: serie.ordem,
        tipoNado: serie.tipoNado,
        repeticoes: serie.quantidadeRepeticoesPlanejada,
        distanciaM: serie.distanciaPlanejadaM,
        pausaSeg: Number(serie.tempoPausaSeg),
        observacoes: serie.observacoes,
        tiros
      }
    })
  )

  return {
    codTreino: treino.codTreino,
    titulo: treino.tituloTreino,
    observacao: treino.observacao ?? '',
    tamanhoPiscinaM: treino.tamanhoPiscinaM,
    series
  }
}

const carregarTela = async () => {
  carregando.value = true
  mensagem.value = null

  try {
    const [fichas, config, emAndamento] = await Promise.all([
      fichaService.listar(),
      piscinaService.obterConfiguracao(),
      treinoService.listar(0)
    ])

    opcoesFichas.value = fichas
    tamanhoPiscinaM.value = config?.tamanhoM ?? null

    if (emAndamento.length > 0) {
      const detalhe = await treinoService.obter(emAndamento[0].codTreino)
      treinoAtivo.value = await mapearTreino(detalhe)
      mensagem.value = {
        tipo: 'info',
        texto: 'Ha um treino em andamento. Continue de onde parou.'
      }
    }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel carregar a tela de treino.')
    }
  } finally {
    carregando.value = false
  }
}

const aoSelecionarFicha = (id: number | null) => {
  if (id == null) return
  const ficha = opcoesFichas.value.find((item) => item.codFicha === id)
  if (!ficha) return
  tituloTreino.value = ficha.tituloFicha
}

const iniciarTreino = async () => {
  if (!fichaSelecionadaId.value || !tamanhoPiscinaM.value) return

  salvando.value = true
  mensagem.value = null

  try {
    const treino = await treinoService.iniciar({
      codFicha: fichaSelecionadaId.value,
      tituloTreino: tituloTreino.value.trim() || undefined,
      observacao: observacaoTreino.value.trim() || undefined,
      tamanhoPiscinaM: tamanhoPiscinaM.value
    })
    treinoAtivo.value = await mapearTreino(treino)
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel iniciar o treino.')
    }
  } finally {
    salvando.value = false
  }
}

const finalizarTreino = async () => {
  if (!treinoAtivo.value) return

  const temTempo = treinoAtivo.value.series.some((serie) =>
    serie.tiros.some((tiro) => Number(tiro) > 0)
  )

  if (!temTempo) {
    mensagem.value = {
      tipo: 'warning',
      texto: 'Informe ao menos um tempo antes de finalizar o treino.'
    }
    return
  }

  salvando.value = true
  mensagem.value = null

  try {
    const { codTreino, series } = treinoAtivo.value

    for (const serie of series) {
      const temposValidos = serie.tiros
        .map((tiro, index) => ({ numero: index + 1, duracao: Number(tiro) }))
        .filter((item) => item.duracao > 0)

      const existentes = await treinoService.listarRepeticoes(codTreino, serie.codSerieTreino)

      for (const item of temposValidos) {
        const payload = {
          numeroRepeticao: item.numero,
          distanciaRealM: serie.distanciaM,
          duracaoSeg: item.duracao
        }

        const existente = existentes.find((r) => r.numeroRepeticao === item.numero)

        if (existente) {
          await treinoService.atualizarRepeticao(
            codTreino,
            serie.codSerieTreino,
            existente.codRepeticaoSerieTreino,
            payload
          )
        } else {
          await treinoService.registrarRepeticao(codTreino, serie.codSerieTreino, payload)
        }
      }

      // Series sem tempo informado sao salvas sem totais (o banco exige NULL ou > 0).
      const serieCronometrada = temposValidos.length > 0
      const tempoTotalSeg = serieCronometrada
        ? temposValidos.reduce((acc, item) => acc + item.duracao, 0)
        : null
      const distanciaTotalM = serieCronometrada ? temposValidos.length * serie.distanciaM : null

      await treinoService.atualizarSerie(codTreino, serie.codSerieTreino, {
        codSerieFicha: serie.codSerieFicha,
        ordem: serie.ordem,
        tipoNado: serie.tipoNado,
        quantidadeRepeticoesPlanejada: serie.repeticoes,
        distanciaPlanejadaM: serie.distanciaM,
        tempoPausaSeg: serie.pausaSeg,
        tempoTotalSeg,
        distanciaTotalM,
        observacoes: serie.observacoes
      })
    }

    await treinoService.atualizar(codTreino, {
      tituloTreino: treinoAtivo.value.titulo.trim(),
      observacao: treinoAtivo.value.observacao.trim() || undefined,
      tamanhoPiscinaM: treinoAtivo.value.tamanhoPiscinaM
    })

    await treinoService.finalizar(codTreino)

    treinoAtivo.value = null
    fichaSelecionadaId.value = null
    tituloTreino.value = ''
    observacaoTreino.value = ''
    mensagem.value = {
      tipo: 'success',
      texto: 'Treino finalizado! Confira o resultado no Historico.'
    }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel finalizar o treino.')
    }
  } finally {
    salvando.value = false
  }
}

const cancelarTreino = async () => {
  if (!treinoAtivo.value) return
  if (!confirm('Deseja realmente cancelar a execucao do treino?')) return

  salvando.value = true
  mensagem.value = null

  try {
    await treinoService.cancelar(treinoAtivo.value.codTreino)
    treinoAtivo.value = null
    fichaSelecionadaId.value = null
    tituloTreino.value = ''
    observacaoTreino.value = ''
    mensagem.value = { tipo: 'info', texto: 'Treino cancelado.' }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel cancelar o treino.')
    }
  } finally {
    salvando.value = false
  }
}

onMounted(carregarTela)
</script>
