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

    <!-- Selecionar Ficha Base para Iniciar -->
    <v-card v-if="!treinoEmAndamento" class="pa-6 rounded-lg elevation-2 mb-6">
      <h2 class="text-subtitle-1 font-weight-bold text-primary mb-4">Escolha a Ficha para Treinar Hoje</h2>
      <v-select
        v-model="fichaSelecionada"
        label="Selecione uma Ficha Base"
        :items="opcoesFichas"
        item-title="titulo"
        item-value="id"
        variant="outlined"
        density="comfortable"
        class="mb-4"
        return-object
      ></v-select>

      <v-btn
        color="primary"
        size="large"
        block
        prepend-icon="mdi-swim"
        :disabled="!fichaSelecionada"
        @click="iniciarTreino"
      >
        Iniciar Treino na Piscina
      </v-btn>
    </v-card>

    <!-- Painel do Treino em Andamento -->
    <div v-else>
      <v-card color="primary" variant="tonal" class="pa-6 rounded-lg mb-6">
        <div class="d-flex justify-space-between align-center flex-wrap ga-4">
          <div>
            <div class="text-caption text-uppercase font-weight-bold">Treino em Andamento</div>
            <h2 class="text-h5 font-weight-bold">{{ treinoAtivo.titulo }}</h2>
          </div>

          <div class="d-flex ga-3">
            <v-btn color="error" variant="outlined" @click="cancelarTreino">
              Cancelar
            </v-btn>
            <v-btn color="success" prepend-icon="mdi-check-circle" @click="finalizarTreino">
              Finalizar Treino
            </v-btn>
          </div>
        </div>
      </v-card>

      <!-- Execução das Séries -->
      <v-card v-for="(serie, sIdx) in treinoAtivo.series" :key="sIdx" class="mb-6 pa-4 rounded-lg elevation-2">
        <v-card-title class="d-flex justify-space-between align-center">
          <span class="text-subtitle-1 font-weight-bold text-primary">
            Serie {{ sIdx + 1 }}: {{ serie.repeticoes }}x {{ serie.distanciaM }}m - {{ serie.nado }}
          </span>
          <v-chip size="small" color="info">Pausa: {{ serie.pausaSeg }}s</v-chip>
        </v-card-title>

        <v-card-text>
          <!-- Tiros/Voltas da Série -->
          <v-row class="mt-2">
            <v-col v-for="rIdx in serie.repeticoes" :key="rIdx" cols="12" sm="6" md="3">
              <v-card variant="outlined" class="pa-3 text-center rounded-lg">
                <div class="text-caption text-medium-emphasis mb-1">Tiro {{ rIdx }} ({{ serie.distanciaM }}m)</div>
                <v-text-field
                  v-model.number="serie.tiros[rIdx - 1]"
                  label="Tempo (seg)"
                  type="number"
                  variant="filled"
                  density="compact"
                  hide-details
                  suffix="s"
                ></v-text-field>
              </v-card>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </div>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const treinoEmAndamento = ref(false)
const fichaSelecionada = ref<any>(null)

const opcoesFichas = ref([
  {
    id: 1,
    titulo: 'Treino A - Resistencia Crawl',
    series: [
      { repeticoes: 4, distanciaM: 200, nado: 'Crawl', pausaSeg: 30, tiros: [160, 162, 165, 161] },
      { repeticoes: 4, distanciaM: 50, nado: 'Crawl', pausaSeg: 15, tiros: [38, 39, 40, 37] }
    ]
  }
])

const treinoAtivo = ref<any>(null)

const iniciarTreino = () => {
  if (fichaSelecionada.value) {
    treinoAtivo.value = JSON.parse(JSON.stringify(fichaSelecionada.value))
    treinoEmAndamento.value = true
  }
}

const finalizarTreino = () => {
  alert('Treino finalizado com sucesso! Dados e médias consolidados.')
  treinoEmAndamento.value = false
  fichaSelecionada.value = null
}

const cancelarTreino = () => {
  if (confirm('Deseja realmente cancelar a execucao do treino?')) {
    treinoEmAndamento.value = false
    fichaSelecionada.value = null
  }
}
</script>
