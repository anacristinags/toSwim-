<template>
  <v-container>
    <!-- Cabeçalho -->
    <div class="mb-6">
      <h1 class="text-h5 font-weight-bold text-primary mb-1">
        <v-icon icon="mdi-history" class="mr-2"></v-icon>Historico de Treinos
      </h1>
      <p class="text-body-2 text-medium-emphasis">
        Consulte todas as suas sessoes de natacao concluidas e seus tempos acumulados.
      </p>
    </div>

    <!-- Tabela / Lista de Treinos Concluídos -->
    <v-card class="rounded-lg elevation-2">
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
          <tr v-for="item in historicoTreinos" :key="item.id">
            <td class="text-body-2 font-weight-medium">{{ item.data }}</td>
            <td class="font-weight-bold text-primary">{{ item.titulo }}</td>
            <td class="text-center">
              <v-chip size="small" color="info" variant="tonal">
                {{ item.distanciaM }}m
              </v-chip>
            </td>
            <td class="text-center font-weight-medium">{{ item.tempoTotal }}</td>
            <td class="text-center">
              <v-chip size="small" color="accent" variant="tonal">
                {{ item.paceMedio }} /100m
              </v-chip>
            </td>
            <td class="text-right">
              <v-btn
                icon="mdi-eye-outline"
                variant="text"
                color="primary"
                density="comfortable"
                @click="abrirDetalhes(item)"
              ></v-btn>
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-card>

    <!-- Modal Detalhes do Treino -->
    <v-dialog v-model="dialogDetalhes" max-width="600">
      <v-card v-if="treinoSelecionado" class="pa-6 rounded-lg">
        <v-card-title class="font-weight-bold text-primary px-0">
          {{ treinoSelecionado.titulo }}
        </v-card-title>
        <v-card-subtitle class="px-0 mb-4">
          Realizado em {{ treinoSelecionado.data }}
        </v-card-subtitle>

        <v-divider class="mb-4"></v-divider>

        <v-card-text class="px-0">
          <h3 class="text-subtitle-2 font-weight-bold mb-3 text-medium-emphasis">Series Executadas</h3>
          <v-list density="compact" class="bg-surface rounded-lg">
            <v-list-item v-for="(s, idx) in treinoSelecionado.series" :key="idx">
              <template v-slot:prepend>
                <v-avatar color="primary" size="24" class="text-caption mr-2">
                  {{ idx + 1 }}
                </v-avatar>
              </template>
              <v-list-item-title class="font-weight-bold">
                {{ s.repeticoes }}x {{ s.distanciaM }}m {{ s.nado }}
              </v-list-item-title>
              <v-list-item-subtitle>
                Tempo acumulado: {{ s.tempoSerie }}
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
import { ref } from 'vue'

const dialogDetalhes = ref(false)
const treinoSelecionado = ref<any>(null)

const historicoTreinos = ref([
  {
    id: 101,
    data: '03/08/2026',
    titulo: 'Treino A - Resistencia Crawl',
    distanciaM: 2000,
    tempoTotal: '48m 30s',
    paceMedio: '01:24',
    series: [
      { repeticoes: 4, distanciaM: 200, nado: 'Crawl', tempoSerie: '12m 40s' },
      { repeticoes: 8, distanciaM: 50, nado: 'Crawl', tempoSerie: '06m 10s' }
    ]
  },
  {
    id: 102,
    data: '01/08/2026',
    titulo: 'Treino B - Tecnica & Medley',
    distanciaM: 1500,
    tempoTotal: '38m 15s',
    paceMedio: '01:32',
    series: [
      { repeticoes: 4, distanciaM: 100, nado: 'Medley', tempoSerie: '08m 20s' }
    ]
  }
])

const abrirDetalhes = (treino: any) => {
  treinoSelecionado.value = treino
  dialogDetalhes.value = true
}
</script>
