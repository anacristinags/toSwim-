<template>
  <v-container>
    <div class="d-flex flex-column flex-sm-row justify-space-between align-start align-sm-center mb-6 ga-4">
      <div>
        <h1 class="text-h5 font-weight-bold text-primary mb-1">
          <v-icon icon="mdi-clipboard-text-outline" class="mr-2"></v-icon>Fichas de Treino Base
        </h1>
        <p class="text-body-2 text-medium-emphasis">
          Crie seus gabaritos de treino reutilizaveis.
        </p>
      </div>

      <div class="d-flex align-center ga-3">
        <v-chip color="info" variant="tonal" size="large">
          <v-icon start icon="mdi-counter"></v-icon>
          Fichas: {{ fichas.length }} / 5 Ativas
        </v-chip>

        <v-btn
          color="primary"
          prepend-icon="mdi-plus"
          size="large"
          :disabled="fichas.length >= 5 || carregando"
          @click="abrirNovaFicha"
        >
          Nova Ficha
        </v-btn>
      </div>
    </div>

    <v-alert v-if="mensagem" :type="mensagem.tipo" variant="tonal" class="mb-4" density="compact">
      {{ mensagem.texto }}
    </v-alert>

    <v-alert
      v-if="!carregando && fichas.length >= 5"
      type="warning"
      variant="tonal"
      class="mb-4"
      density="compact"
    >
      Você atingiu o limite de 5 fichas ativas. Exclua uma ficha para criar outra.
    </v-alert>

    <div v-if="carregando" class="text-center py-12">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </div>

    <v-alert
      v-else-if="fichas.length === 0"
      type="info"
      variant="tonal"
      class="mb-4"
    >
      Nenhuma ficha ativa ainda. Crie a primeira para montar seus treinos.
    </v-alert>

    <v-row v-else>
      <v-col v-for="ficha in fichas" :key="ficha.codFicha" cols="12" md="6">
        <v-card class="elevation-2 rounded-lg fill-height d-flex flex-column">
          <v-card-item class="bg-surface">
            <template v-slot:prepend>
              <v-icon icon="mdi-swim" color="primary" class="mr-2"></v-icon>
            </template>
            <v-card-title class="font-weight-bold">{{ ficha.tituloFicha }}</v-card-title>
            <v-card-subtitle>
              {{ seriesDaFicha(ficha).length }} series cadastradas
              <v-chip size="small" color="primary" variant="flat" class="ml-2">
                Piscina: {{ ficha.tamanhoPiscinaM }}m
              </v-chip>
            </v-card-subtitle>

            <template v-slot:append>
              <v-btn
                icon="mdi-content-copy"
                variant="text"
                density="comfortable"
                color="info"
                title="Duplicar Ficha"
                :disabled="fichas.length >= 5"
                @click="duplicarFicha(ficha.codFicha)"
              ></v-btn>
              <v-btn
                icon="mdi-delete-outline"
                variant="text"
                density="comfortable"
                color="error"
                title="Excluir Ficha"
                @click="excluirFicha(ficha.codFicha)"
              ></v-btn>
            </template>
          </v-card-item>

          <v-divider></v-divider>

          <v-card-text class="flex-grow-1">
            <v-list density="compact" class="bg-transparent">
              <v-list-item
                v-for="serie in seriesDaFicha(ficha)"
                :key="serie.codSerieFicha"
                class="px-0"
              >
                <template v-slot:prepend>
                  <v-avatar color="primary" size="28" class="text-caption font-weight-bold mr-3">
                    {{ serie.ordem }}
                  </v-avatar>
                </template>

                <v-list-item-title class="font-weight-medium">
                  {{ serie.quantidadeRepeticoes }}x {{ serie.distanciaM }}m - {{ nomeNado(serie.tipoNado) }}
                </v-list-item-title>
                <v-list-item-subtitle>
                  Pausa: {{ serie.tempoPausaSeg }}s | {{ serie.observacoes || 'Sem observacoes' }}
                </v-list-item-subtitle>

                <template v-slot:append>
                  <v-btn
                    icon="mdi-content-copy"
                    variant="text"
                    density="compact"
                    color="info"
                    title="Duplicar serie"
                    @click="duplicarSerie(serie.codSerieFicha)"
                  ></v-btn>
                  <v-btn
                    icon="mdi-delete-outline"
                    variant="text"
                    density="compact"
                    color="error"
                    title="Remover serie"
                    @click="excluirSerie(serie.codSerieFicha)"
                  ></v-btn>
                </template>
              </v-list-item>
            </v-list>
          </v-card-text>

          <v-divider></v-divider>

          <v-card-actions class="pa-4 bg-surface">
            <v-btn
              color="primary"
              variant="tonal"
              block
              prepend-icon="mdi-plus-circle-outline"
              @click="abrirAdicionarSerie(ficha)"
            >
              Adicionar Serie
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

    <v-dialog v-model="dialogNovaFicha" max-width="500">
      <v-card class="pa-4 rounded-lg">
        <v-card-title tag="h2" class="font-weight-bold text-primary">Criar Nova Ficha Base</v-card-title>
        <v-card-text>
          <v-text-field
            v-model="novaFichaTitulo"
            label="Titulo da Ficha (Ex: Treino de Velocidade 1500m)"
            variant="outlined"
            density="comfortable"
            class="mt-2 mb-3"
          ></v-text-field>

          <v-select
            v-model="novaFichaTamanhoPiscina"
            label="Tamanho da Piscina"
            :items="[{ title: '25 metros', value: 25 }, { title: '50 metros', value: 50 }]"
            item-title="title"
            item-value="value"
            variant="outlined"
            density="comfortable"
          ></v-select>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="dialogNovaFicha = false">Cancelar</v-btn>
          <v-btn color="primary" :loading="salvando" @click="salvarNovaFicha">Criar Ficha</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog v-model="dialogNovaSerie" max-width="500">
      <v-card class="pa-4 rounded-lg">
        <v-card-title tag="h2" class="font-weight-bold text-primary">Adicionar Serie ao Treino</v-card-title>
        <v-card-text>
          <v-select
            v-model="novaSerie.tipoNado"
            label="Tipo de Nado"
            :items="TIPOS_NADO"
            item-title="title"
            item-value="value"
            variant="outlined"
            density="comfortable"
            class="mb-3 mt-2"
          ></v-select>

          <v-row>
            <v-col cols="6">
              <v-text-field
                v-model.number="novaSerie.quantidadeRepeticoes"
                label="Quantidade Repeticoes"
                type="number"
                variant="outlined"
                density="comfortable"
              ></v-text-field>
            </v-col>
            <v-col cols="6">
              <v-text-field
                v-model.number="novaSerie.distanciaM"
                label="Distancia (Metros)"
                type="number"
                variant="outlined"
                density="comfortable"
              ></v-text-field>
            </v-col>
          </v-row>

          <v-text-field
            v-model.number="novaSerie.tempoPausaSeg"
            label="Tempo de Pausa (Segundos)"
            type="number"
            variant="outlined"
            density="comfortable"
            class="mb-3"
          ></v-text-field>

          <v-text-field
            v-model="novaSerie.observacoes"
            label="Observacoes (Opcional)"
            variant="outlined"
            density="comfortable"
            class="mb-2"
          ></v-text-field>

          <v-checkbox
            v-model="novaSerie.ignorarNoPace"
            label="Não considerar esta série no pace médio"
            density="comfortable"
            hide-details
            aria-label="Não considerar esta série no pace médio"
          ></v-checkbox>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="dialogNovaSerie = false">Cancelar</v-btn>
          <v-btn color="primary" :loading="salvando" @click="salvarNovaSerie">Adicionar Serie</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import fichaService, {
  type FichaBaseResponse,
  type SerieFichaPayload
} from '@/services/fichaService'
import { extrairMensagemErro } from '@/services/erros'
import { TIPOS_NADO, nomeNado } from '@/services/formatacao'
import { usePiscinaStore } from '@/stores/piscina'

const piscinaStore = usePiscinaStore()

const fichas = ref<FichaBaseResponse[]>([])
const carregando = ref(false)
const salvando = ref(false)
const mensagem = ref<{ tipo: 'success' | 'error' | 'info'; texto: string } | null>(null)

const dialogNovaFicha = ref(false)
const novaFichaTitulo = ref('')
const novaFichaTamanhoPiscina = ref(25)

const dialogNovaSerie = ref(false)
const fichaSelecionadaId = ref<number | null>(null)
const novaSerie = ref({
  tipoNado: 0,
  quantidadeRepeticoes: 4,
  distanciaM: 100,
  tempoPausaSeg: 20,
  observacoes: '',
  ignorarNoPace: false
})

const seriesDaFicha = (ficha: FichaBaseResponse) =>
  [...(ficha.series ?? [])].sort((a, b) => a.ordem - b.ordem)

const carregarFichas = async (silencioso = false) => {
  if (!silencioso) {
    carregando.value = true
    mensagem.value = null
  }

  try {
    fichas.value = await fichaService.listar()
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel carregar as fichas.')
    }
  } finally {
    if (!silencioso) carregando.value = false
  }
}

const abrirNovaFicha = async () => {
  novaFichaTitulo.value = ''
  if (!piscinaStore.carregado) {
    try {
      await piscinaStore.carregar()
    } catch {
      // mantem o default de 25m caso a config nao possa ser carregada
    }
  }
  novaFichaTamanhoPiscina.value = piscinaStore.tamanhoM
  dialogNovaFicha.value = true
}

const salvarNovaFicha = async () => {
  if (!novaFichaTitulo.value.trim()) return
  if (novaFichaTamanhoPiscina.value !== 25 && novaFichaTamanhoPiscina.value !== 50) return

  salvando.value = true
  mensagem.value = null

  try {
    await fichaService.criar({
      tituloFicha: novaFichaTitulo.value.trim(),
      tipoFicha: 0,
      tamanhoPiscinaM: novaFichaTamanhoPiscina.value
    })
    dialogNovaFicha.value = false
    await carregarFichas()
    mensagem.value = { tipo: 'success', texto: 'Ficha criada com sucesso.' }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel criar a ficha.')
    }
  } finally {
    salvando.value = false
  }
}

const duplicarFicha = async (id: number) => {
  mensagem.value = null

  try {
    await fichaService.duplicar(id)
    await carregarFichas()
    mensagem.value = { tipo: 'success', texto: 'Ficha duplicada com sucesso.' }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel duplicar a ficha.')
    }
  }
}

const excluirFicha = async (id: number) => {
  if (!confirm('Deseja excluir esta ficha?')) return

  mensagem.value = null

  try {
    await fichaService.excluir(id)
    await carregarFichas()
    mensagem.value = { tipo: 'success', texto: 'Ficha excluida com sucesso.' }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel excluir a ficha.')
    }
  }
}

const abrirAdicionarSerie = (ficha: FichaBaseResponse) => {
  fichaSelecionadaId.value = ficha.codFicha
  novaSerie.value = {
    tipoNado: 0,
    quantidadeRepeticoes: 4,
    distanciaM: 100,
    tempoPausaSeg: 20,
    observacoes: '',
    ignorarNoPace: false
  }
  dialogNovaSerie.value = true
}

const salvarNovaSerie = async () => {
  if (fichaSelecionadaId.value == null) return

  const payload: SerieFichaPayload = {
    tipoNado: novaSerie.value.tipoNado,
    quantidadeRepeticoes: novaSerie.value.quantidadeRepeticoes,
    distanciaM: novaSerie.value.distanciaM,
    tempoPausaSeg: novaSerie.value.tempoPausaSeg,
    isGoalSeries: false,
    ignorarNoPace: novaSerie.value.ignorarNoPace,
    observacoes: novaSerie.value.observacoes || undefined
  }

  salvando.value = true
  mensagem.value = null

  try {
    await fichaService.adicionarSerie(fichaSelecionadaId.value, payload)
    dialogNovaSerie.value = false
    await carregarFichas(true)
    mensagem.value = { tipo: 'success', texto: 'Serie adicionada com sucesso.' }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel adicionar a serie.')
    }
  } finally {
    salvando.value = false
  }
}

const duplicarSerie = async (idSerie: number) => {
  mensagem.value = null

  try {
    await fichaService.duplicarSerie(idSerie)
    await carregarFichas(true)
    mensagem.value = { tipo: 'success', texto: 'Serie duplicada com sucesso.' }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel duplicar a serie.')
    }
  }
}

const excluirSerie = async (idSerie: number) => {
  if (!confirm('Deseja remover esta serie?')) return

  mensagem.value = null

  try {
    await fichaService.excluirSerie(idSerie)
    await carregarFichas(true)
    mensagem.value = { tipo: 'success', texto: 'Serie removida com sucesso.' }
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel remover a serie.')
    }
  }
}

onMounted(carregarFichas)
</script>
