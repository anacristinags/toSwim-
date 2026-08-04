<template>
  <v-container>
    <!-- Cabeçalho -->
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
        @click="dialogNovaMeta = true"
      >
        Nova Meta
      </v-btn>
    </div>

    <!-- Filtro de Status -->
    <v-tabs v-model="filtroStatus" color="primary" class="mb-6">
      <v-tab :value="null">Todas</v-tab>
      <v-tab :value="0">Ativas</v-tab>
      <v-tab :value="1">Concluidas</v-tab>
    </v-tabs>

    <!-- Cards de Metas -->
    <v-row>
      <v-col v-for="meta in metasFiltradas" :key="meta.id" cols="12" md="6">
        <v-card class="pa-6 rounded-lg elevation-2 border-primary">
          <div class="d-flex justify-space-between align-start mb-3">
            <div>
              <v-chip
                :color="meta.status === 1 ? 'success' : 'primary'"
                size="small"
                class="mb-2"
              >
                {{ meta.status === 1 ? 'Concluida' : 'Em Andamento' }}
              </v-chip>
              <h2 class="text-h6 font-weight-bold">{{ meta.titulo }}</h2>
            </div>
            <v-chip variant="outlined" color="info">
              {{ meta.nado }} - {{ meta.distanciaM }}m
            </v-chip>
          </div>

          <v-divider class="my-4"></v-divider>

          <!-- Métricas da Meta -->
          <v-row density="compact" class="text-center my-2">
            <v-col cols="4">
              <div class="text-caption text-medium-emphasis">Tempo Alvo</div>
              <div class="text-subtitle-1 font-weight-bold text-primary">{{ meta.tempoAlvo }}</div>
            </v-col>

            <v-col cols="4">
              <div class="text-caption text-medium-emphasis">Pace Alvo</div>
              <div class="text-subtitle-1 font-weight-bold text-info">{{ meta.paceAlvo }} /100m</div>
            </v-col>

            <v-col cols="4">
              <div class="text-caption text-medium-emphasis">Melhor Pace</div>
              <div class="text-subtitle-1 font-weight-bold text-accent">{{ meta.melhorPace || '--' }}</div>
            </v-col>
          </v-row>

          <!-- Barra de Progresso Visual -->
          <div class="mt-4">
            <div class="d-flex justify-space-between text-caption mb-1">
              <span>Progresso ate a meta</span>
              <span class="font-weight-bold text-accent">{{ meta.progresso }}%</span>
            </div>
            <v-progress-linear
              :model-value="meta.progresso"
              color="accent"
              height="8"
              rounded
            ></v-progress-linear>
          </div>
        </v-card>
      </v-col>
    </v-row>

    <!-- Modal Criar Nova Meta -->
    <v-dialog v-model="dialogNovaMeta" max-width="500">
      <v-card class="pa-4 rounded-lg">
        <v-card-title class="font-weight-bold text-primary">Criar Meta de Tempo</v-card-title>
        <v-card-text>
          <v-text-field
            v-model="novaMeta.titulo"
            label="Titulo da Meta (Ex: Baixar de 1m20s nos 100m Crawl)"
            variant="outlined"
            density="comfortable"
            class="mb-3 mt-2"
          ></v-text-field>

          <v-select
            v-model="novaMeta.nado"
            label="Tipo de Nado"
            :items="['Crawl', 'Costas', 'Peito', 'Borboleta', 'Medley']"
            variant="outlined"
            density="comfortable"
            class="mb-3"
          ></v-select>

          <v-row>
            <v-col cols="6">
              <v-text-field
                v-model.number="novaMeta.distanciaM"
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
          <v-btn color="primary" @click="salvarMeta">Criar Meta</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'

const filtroStatus = ref<number | null>(null)
const dialogNovaMeta = ref(false)

const metas = ref([
  {
    id: 1,
    titulo: 'Baixar de 1m15s nos 100m Crawl',
    nado: 'Crawl',
    distanciaM: 100,
    tempoAlvo: '01:15',
    paceAlvo: '01:15',
    melhorPace: '01:18',
    progresso: 85,
    status: 0
  },
  {
    id: 2,
    titulo: 'Meta 400m Livre Sub 6 Minutos',
    nado: 'Crawl',
    distanciaM: 400,
    tempoAlvo: '05:50',
    paceAlvo: '01:27',
    melhorPace: '05:48',
    progresso: 100,
    status: 1
  }
])

const novaMeta = ref({
  titulo: '',
  nado: 'Crawl',
  distanciaM: 100,
  tempoAlvoSeg: 75
})

const metasFiltradas = computed(() => {
  if (filtroStatus.value === null) return metas.value
  return metas.value.filter(m => m.status === filtroStatus.value)
})

const salvarMeta = () => {
  if (novaMeta.value.titulo) {
    metas.value.push({
      id: Date.now(),
      titulo: novaMeta.value.titulo,
      nado: novaMeta.value.nado,
      distanciaM: novaMeta.value.distanciaM,
      tempoAlvo: '01:15',
      paceAlvo: '01:15',
      melhorPace: '--',
      progresso: 0,
      status: 0
    })
    dialogNovaMeta.value = false
  }
}
</script>
