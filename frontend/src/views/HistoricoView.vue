<template>
  <v-container>
    <div class="mb-6">
      <h1 class="text-h5 font-weight-bold text-primary mb-1">
        <v-icon icon="mdi-history" class="mr-2"></v-icon>Historico de Treinos
      </h1>
      <p class="text-body-2 text-medium-emphasis">
        Consulte todas as suas sessoes de natacao concluidas e seus tempos acumulados.
      </p>
    </div>

    <v-alert v-if="mensagem" :type="mensagem.tipo" variant="tonal" class="mb-4" density="compact">
      {{ mensagem.texto }}
    </v-alert>

    <div v-if="carregando" class="text-center py-12">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </div>

    <v-alert
      v-else-if="historicoTreinos.length === 0"
      type="info"
      variant="tonal"
    >
      Nenhum treino concluido ainda. Finalize um treino na tela de Execucao.
    </v-alert>

    <v-card v-else class="rounded-lg elevation-2">
      <v-table hover>
        <thead>
          <tr>
            <th class="text-left font-weight-bold">Data</th>
            <th class="text-left font-weight-bold">Treino</th>
            <th class="text-center font-weight-bold">Distancia Total</th>
            <th class="text-center font-weight-bold">Tempo Total</th>
            <th class="text-center font-weight-bold">Pace Medio</th>
            <th class="text-right font-weight-bold">Acoes</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in historicoTreinos" :key="item.codTreino">
            <td class="text-body-2 font-weight-medium">{{ formatarData(item.dataTreino) }}</td>
            <td class="font-weight-bold text-primary">{{ item.tituloTreino }}</td>
            <td class="text-center">
              <v-chip size="small" color="info" variant="tonal">
                {{ item.distanciaTotalM }}m
              </v-chip>
            </td>
            <td class="text-center font-weight-medium">
              {{ formatarDuracao(item.duracaoTotalSeg) }}
            </td>
            <td class="text-center">
              <v-chip size="small" color="primary" variant="tonal">
                {{ formatarPace(item.paceMedioSeg) }} /100m
              </v-chip>
            </td>
            <td class="text-right">
              <v-btn
                icon="mdi-eye-outline"
                variant="text"
                color="primary"
                density="comfortable"
                :loading="carregandoDetalhe === item.codTreino"
                @click="abrirDetalhes(item.codTreino)"
              ></v-btn>
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-card>

    <v-dialog v-model="dialogDetalhes" max-width="600">
      <v-card v-if="treinoSelecionado" class="pa-6 rounded-lg">
        <v-card-title class="font-weight-bold text-primary px-0">
          {{ treinoSelecionado.tituloTreino }}
        </v-card-title>
        <v-card-subtitle class="px-0 mb-4">
          Realizado em {{ formatarData(treinoSelecionado.dataTreino) }}
        </v-card-subtitle>

        <v-divider class="mb-4"></v-divider>

        <v-card-text class="px-0">
          <h3 class="text-subtitle-2 font-weight-bold mb-3 text-medium-emphasis">
            Series Executadas
          </h3>
          <v-list density="compact" class="bg-surface rounded-lg">
            <v-list-item
              v-for="(s, idx) in treinoSelecionado.seriesTreino"
              :key="s.codSerieTreino"
            >
              <template v-slot:prepend>
                <v-avatar color="primary" size="24" class="text-caption mr-2">
                  {{ Number(idx) + 1 }}
                </v-avatar>
              </template>
              <v-list-item-title class="font-weight-bold">
                {{ s.quantidadeRepeticoesPlanejada }}x {{ s.distanciaPlanejadaM }}m
                {{ nomeNado(s.tipoNado) }}
              </v-list-item-title>
              <v-list-item-subtitle>
                Tempo acumulado: {{ formatarDuracao(s.tempoTotalSeg) }}
              </v-list-item-subtitle>
            </v-list-item>
          </v-list>
        </v-card-text>

        <v-card-actions class="justify-end px-0 mt-4">
          <v-btn color="primary" variant="tonal" @click="dialogDetalhes = false">Fechar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import historicoService from '@/services/historicoService'
import type { TreinoResponse } from '@/services/treinoService'
import { extrairMensagemErro } from '@/services/erros'
import { formatarData, formatarDuracao, formatarPace, nomeNado } from '@/services/formatacao'

const dialogDetalhes = ref(false)
const treinoSelecionado = ref<TreinoResponse | null>(null)
const historicoTreinos = ref<TreinoResponse[]>([])
const carregando = ref(false)
const carregandoDetalhe = ref<number | null>(null)
const mensagem = ref<{ tipo: 'success' | 'error' | 'info'; texto: string } | null>(null)

const carregarHistorico = async () => {
  carregando.value = true
  mensagem.value = null

  try {
    historicoTreinos.value = await historicoService.listar()
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel carregar o historico.')
    }
  } finally {
    carregando.value = false
  }
}

const abrirDetalhes = async (codTreino: number) => {
  carregandoDetalhe.value = codTreino
  mensagem.value = null

  try {
    treinoSelecionado.value = await historicoService.obter(codTreino)
    dialogDetalhes.value = true
  } catch (error) {
    mensagem.value = {
      tipo: 'error',
      texto: extrairMensagemErro(error, 'Nao foi possivel abrir o detalhe do treino.')
    }
  } finally {
    carregandoDetalhe.value = null
  }
}

onMounted(carregarHistorico)
</script>
