<template>
  <v-container>
    <div class="d-flex flex-column flex-sm-row justify-space-between align-start align-sm-center mb-6 ga-4">
      <div>
        <h1 class="text-h5 font-weight-bold text-primary mb-1">
          <v-icon icon="mdi-target" class="mr-2"></v-icon>Metas de Tempo
        </h1>
        <p class="text-body-2 text-medium-emphasis">
          Acompanhe e desafie seus limites com metas por distancia e ritmo.
        </p>
      </div>

      <v-btn
        color="primary"
        prepend-icon="mdi-plus"
        size="large"
        :disabled="opcoesSeries.length === 0"
        @click="abrirNovaMeta"
      >
        Nova Meta
      </v-btn>
    </div>

    <v-alert v-if="mensagem" :type="mensagem.tipo" variant="tonal" class="mb-4" density="compact">
      {{ mensagem.texto }}
    </v-alert>

    <v-tabs v-model="filtroStatus" color="primary" class="mb-6">
      <v-tab :value="null">Todas</v-tab>
      <v-tab :value="0">Ativas</v-tab>
      <v-tab :value="1">Concluidas</v-tab>
    </v-tabs>

    <div v-if="carregando" class="text-center py-12">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </div>

    <v-alert
      v-else-if="metasFiltradas.length === 0"
      type="info"
      variant="tonal"
    >
      Nenhuma meta encontrada para este filtro.
    </v-alert>

    <v-row v-else>
      <v-col v-for="meta in metasFiltradas" :key="meta.codMeta" cols="12" md="6">
        <v-card class="pa-6 rounded-lg elevation-2">
          <div class="d-flex justify-space-between align-start mb-3">
            <div>
              <v-chip
                :color="meta.status === 1 ? 'success' : meta.status === 2 ? 'error' : 'primary'"
                size="small"
                class="mb-2"
              >
                {{ rotuloStatus(meta.status) }}
              </v-chip>
              <h2 class="text-h6 font-weight-bold">{{ meta.tituloMeta }}</h2>
            </div>
            <v-chip variant="outlined" color="info">
              {{ nomeNado(meta.tipoNado) }} - {{ meta.distanciaAlvoM }}m
            </v-chip>
          </div>

          <v-divider class="my-4"></v-divider>

          <v-row density="compact" class="text-center my-2">
            <v-col cols="4">
              <div class="text-caption text-medium-emphasis">Tempo Alvo</div>
              <div class="text-subtitle-1 font-weight-bold text-primary">
                {{ formatarPace(meta.tempoAlvoSeg) }}
              </div>
            </v-col>

            <v-col cols="4">
              <div class="text-caption text-medium-emphasis">Pace Alvo</div>
              <div class="text-subtitle-1 font-weight-bold text-info">
                {{ formatarPace(meta.paceAlvoSeg) }} /100m
              </div>
            </v-col>

            <v-col cols="4">
              <div class="text-caption text-medium-emphasis">Melhor Pace</div>
              <div class="text-subtitle-1 font-weight-bold">
                {{ formatarPace(meta.melhorPace) }}
              </div>
            </v-col>
          </v-row>

          <div class="mt-4">
            <div class="d-flex justify-space-between text-caption mb-1">
              <span>Progresso ate a meta</span>
              <span class="font-weight-bold">{{ meta.progresso }}%</span>
            </div>
            <v-progress-linear
              :model-value="meta.progresso"
              color="primary"
              height="8"
              rounded
            ></v-progress-linear>
            <div class="text-caption text-medium-emphasis mt-2">
              {{
                meta.tentativas > 0
                  ? `${meta.tentativas} tentativa(s) no historico de treinos`
                  : 'Nenhum treino compativel no historico ainda'
              }}
              <span v-if="meta.progresso >= 100 && meta.status === 1">
                · Meta atingida e concluida automaticamente
              </span>
            </div>
          </div>

          <div class="d-flex justify-end ga-2 mt-4">
            <v-btn
              v-if="meta.status === 0"
              size="small"
              variant="tonal"
              color="success"
              @click="marcarConcluida(meta.codMeta)"
            >
              Concluir
            </v-btn>
            <v-btn
              size="small"
              variant="text"
              color="error"
              @click="excluirMeta(meta.codMeta)"
            >
              Excluir
            </v-btn>
          </div>
        </v-card>
      </v-col>
    </v-row>

    <v-dialog v-model="dialogNovaMeta" max-width="560">
      <v-card class="pa-4 rounded-lg">
        <v-card-title tag="h2" class="font-weight-bold text-primary">Criar Meta de Tempo</v-card-title>
        <v-card-text>
          <v-select
            v-model="novaMeta.codSerieFicha"
            label="Serie da Ficha"
            :items="opcoesSeries"
            item-title="label"
            item-value="codSerieFicha"
            variant="outlined"
            density="comfortable"
            class="mb-3 mt-2"
            @update:model-value="aoSelecionarSerie"
          ></v-select>

          <v-text-field
            v-model="novaMeta.tituloMeta"
            label="Titulo da Meta"
            variant="outlined"
            density="comfortable"
            class="mb-3"
          ></v-text-field>

          <v-select
            v-model="novaMeta.tipoNado"
            label="Tipo de Nado"
            :items="TIPOS_NADO"
            item-title="title"
            item-value="value"
            variant="outlined"
            density="comfortable"
            class="mb-3"
          ></v-select>

          <v-row>
            <v-col cols="6">
              <v-text-field
                v-model.number="novaMeta.distanciaAlvoM"
                label="Distancia (Metros)"
                type="number"
                variant="outlined"
                density="comfortable"
              ></v-text-field>
            </v-col>

            <v-col cols="6">
              <v-text-field
                v-model.number="novaMeta.tempoAlvoSeg"
                label="Tempo Alvo (Segundos)"
                type="number"
                variant="outlined"
                density="comfortable"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="dialogNovaMeta = false">Cancelar</v-btn>
          <v-btn color="primary" :loading="salvando" @click="salvarMeta">Criar Meta</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import fichaService from '@/services/fichaService'
import metaService, { type MetaResponse } from '@/services/metaService'
import { extrairMensagemErro } from '@/services/erros'
import { TIPOS_NADO, formatarPace, nomeNado } from '@/services/formatacao'

interface MetaExibicao extends MetaResponse {
  progresso: number
  melhorPace?: number | null
  tentativas: number
}

interface OpcaoSerie {
  codSerieFicha: number
  label: string
  tipoNado: number
  distanciaM: number
  tituloFicha: string
}

const filtroStatus = ref<number | null>(null)
const dialogNovaMeta = ref(false)
const carregando = ref(false)
const salvando = ref(false)
const mensagem = ref<{ tipo: 'success' | 'error' | 'info'; texto: string } | null>(null)

const metas = ref<MetaExibicao[]>([])
const opcoesSeries = ref<OpcaoSerie[]>([])

const novaMeta = ref({
  codSerieFicha: null as number | null,
  tituloMeta: '',
  tipoNado: 0,
  distanciaAlvoM: 100,
  tempoAlvoSeg: 75
})

const metasFiltradas = computed(() => {
  if (filtroStatus.value === null) return metas.value
  return metas.value.filter((m) => m.status === filtroStatus.value)
})

const rotuloStatus = (status: number) => {
  if (status === 1) return 'Concluida'
  if (status === 2) return 'Cancelada'
  return 'Em Andamento'
}

const carregarSeries = async () => {
  const fichas = await fichaService.listar()
  opcoesSeries.value = fichas.flatMap((ficha) =>
    ficha.series.map((serie) => ({
      codSerieFicha: serie.codSerieFicha,
      tipoNado: serie.tipoNado,
      distanciaM: serie.distanciaM,
      tituloFicha: ficha.tituloFicha,
      label: `${ficha.tituloFicha} · ${serie.ordem}. ${serie.quantidadeRepeticoes}x${serie.distanciaM}m ${nomeNado(serie.tipoNado)}`
    }))
  )
}

const carregarMetas = async () => {
  carregando.value = true
  mensagem.value = null

  try {
    await carregarSeries()
    const lista = await metaService.listar()

    const comProgresso = await Promise.all(
      lista.map(async (meta) => {
        try {
          const progresso = await metaService.obterProgresso(meta.codMeta)
          return {
            ...meta,
            progresso: Math.round(progresso.percentualAtingimento ?? 0),
            melhorPace: progresso.melhorPaceRealizadoSeg,
            tentativas: progresso.historicoTentativas?.length ?? 0,
            status: progresso.status ?? meta.status
          }
        } catch {
          return {
            ...meta,
            progresso: 0,
            melhorPace: null,
            tentativas: 0
          }
        }
      })
    )

    metas.value = comProgresso
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel carregar as metas.')
    }
  } finally {
    carregando.value = false
  }
}

const abrirNovaMeta = () => {
  novaMeta.value = {
    codSerieFicha: opcoesSeries.value[0]?.codSerieFicha ?? null,
    tituloMeta: '',
    tipoNado: opcoesSeries.value[0]?.tipoNado ?? 0,
    distanciaAlvoM: opcoesSeries.value[0]?.distanciaM ?? 100,
    tempoAlvoSeg: 75
  }
  dialogNovaMeta.value = true
}

const aoSelecionarSerie = (codSerieFicha: number) => {
  const serie = opcoesSeries.value.find((s) => s.codSerieFicha === codSerieFicha)
  if (!serie) return

  novaMeta.value.tipoNado = serie.tipoNado
  novaMeta.value.distanciaAlvoM = serie.distanciaM
  if (!novaMeta.value.tituloMeta.trim()) {
    novaMeta.value.tituloMeta = `Meta ${serie.distanciaM}m ${nomeNado(serie.tipoNado)}`
  }
}

const salvarMeta = async () => {
  if (!novaMeta.value.codSerieFicha || !novaMeta.value.tituloMeta.trim()) {
    mensagem.value = {
      tipo: 'error',
      texto: 'Informe a serie e o titulo da meta.'
    }
    return
  }

  salvando.value = true
  mensagem.value = null

  try {
    await metaService.criar({
      codSerieFicha: novaMeta.value.codSerieFicha,
      tituloMeta: novaMeta.value.tituloMeta.trim(),
      tipoNado: novaMeta.value.tipoNado,
      distanciaAlvoM: novaMeta.value.distanciaAlvoM,
      tempoAlvoSeg: novaMeta.value.tempoAlvoSeg,
      modoAvaliacao: 0
    })

    dialogNovaMeta.value = false
    await carregarMetas()
    mensagem.value = { tipo: 'success', texto: 'Meta criada com sucesso.' }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel criar a meta.')
    }
  } finally {
    salvando.value = false
  }
}

const marcarConcluida = async (id: number) => {
  try {
    await metaService.alterarStatus(id, 1)
    await carregarMetas()
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel concluir a meta.')
    }
  }
}

const excluirMeta = async (id: number) => {
  if (!confirm('Deseja excluir esta meta?')) return

  try {
    await metaService.excluir(id)
    await carregarMetas()
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel excluir a meta.')
    }
  }
}

onMounted(carregarMetas)
</script>
