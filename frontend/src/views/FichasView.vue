<template>
  <v-container>
    <!-- Cabeçalho -->
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
          :disabled="fichas.length >= 5"
          @click="dialogNovaFicha = true"
        >
          Nova Ficha
        </v-btn>
      </div>
    </div>

    <!-- Lista de Fichas (Cards) -->
    <v-row>
      <v-col v-for="ficha in fichas" :key="ficha.id" cols="12" md="6">
        <v-card class="elevation-2 rounded-lg fill-height d-flex flex-column">
          <v-card-item class="bg-surface">
            <template v-slot:prepend>
              <v-icon icon="mdi-swim" color="primary" class="mr-2"></v-icon>
            </template>
            <v-card-title class="font-weight-bold">{{ ficha.titulo }}</v-card-title>
            <v-card-subtitle>{{ ficha.series.length }} series cadastradas</v-card-subtitle>
            
            <template v-slot:append>
              <v-btn
                icon="mdi-content-copy"
                variant="text"
                density="comfortable"
                color="info"
                title="Duplicar Ficha"
                @click="duplicarFicha(ficha)"
              ></v-btn>
              <v-btn
                icon="mdi-delete-outline"
                variant="text"
                density="comfortable"
                color="error"
                title="Excluir Ficha"
                @click="excluirFicha(ficha.id)"
              ></v-btn>
            </template>
          </v-card-item>

          <v-divider></v-divider>

          <!-- Tabela/Lista de Séries da Ficha -->
          <v-card-text class="flex-grow-1">
            <v-list density="compact" class="bg-transparent">
              <v-list-item
                v-for="(serie, index) in ficha.series"
                :key="index"
                class="px-0"
              >
                <template v-slot:prepend>
                  <v-avatar color="primary" size="28" class="text-caption font-weight-bold mr-3">
                    {{ index + 1 }}
                  </v-avatar>
                </template>

                <v-list-item-title class="font-weight-medium">
                  {{ serie.repeticoes }}x {{ serie.distanciaM }}m - {{ serie.nado }}
                </v-list-item-title>
                <v-list-item-subtitle>
                  Pausa: {{ serie.pausaSeg }}s | {{ serie.obs || 'Sem observacoes' }}
                </v-list-item-subtitle>
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
              @click="abrirAdicionarSerie(ficha.id)"
            >
              Adicionar Serie
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

    <!-- Modal Nova Ficha -->
    <v-dialog v-model="dialogNovaFicha" max-width="500">
      <v-card class="pa-4 rounded-lg">
        <v-card-title class="font-weight-bold text-primary">Criar Nova Ficha Base</v-card-title>
        <v-card-text>
          <v-text-field
            v-model="novaFichaTitulo"
            label="Titulo da Ficha (Ex: Treino de Velocidade 1500m)"
            variant="outlined"
            density="comfortable"
            class="mt-2"
          ></v-text-field>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="dialogNovaFicha = false">Cancelar</v-btn>
          <v-btn color="primary" @click="salvarNovaFicha">Criar Ficha</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Modal Adicionar Série -->
    <v-dialog v-model="dialogNovaSerie" max-width="500">
      <v-card class="pa-4 rounded-lg">
        <v-card-title class="font-weight-bold text-primary">Adicionar Serie ao Treino</v-card-title>
        <v-card-text>
          <v-select
            v-model="novaSerie.nado"
            label="Tipo de Nado"
            :items="['Crawl', 'Costas', 'Peito', 'Borboleta', 'Medley']"
            variant="outlined"
            density="comfortable"
            class="mb-3 mt-2"
          ></v-select>

          <v-row>
            <v-col cols="6">
              <v-text-field
                v-model.number="novaSerie.repeticoes"
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
            v-model.number="novaSerie.pausaSeg"
            label="Tempo de Pausa (Segundos)"
            type="number"
            variant="outlined"
            density="comfortable"
            class="mb-3"
          ></v-text-field>

          <v-text-field
            v-model="novaSerie.obs"
            label="Observacoes (Opcional)"
            variant="outlined"
            density="comfortable"
          ></v-text-field>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="dialogNovaSerie = false">Cancelar</v-btn>
          <v-btn color="primary" @click="salvarNovaSerie">Adicionar Serie</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'

interface Serie {
  repeticoes: number
  distanciaM: number
  nado: string
  pausaSeg: number
  obs?: string
}

interface Ficha {
  id: number
  titulo: string
  series: Serie[]
}

const fichas = ref<Ficha[]>([
  {
    id: 1,
    titulo: 'Treino A - Resistencia Crawl',
    series: [
      { repeticoes: 4, distanciaM: 200, nado: 'Crawl', pausaSeg: 30, obs: 'Ritmo moderado' },
      { repeticoes: 8, distanciaM: 50, nado: 'Crawl', pausaSeg: 15, obs: 'Tiros fortes' }
    ]
  },
  {
    id: 2,
    titulo: 'Treino B - Tecnica & Medley',
    series: [
      { repeticoes: 4, distanciaM: 100, nado: 'Medley', pausaSeg: 20, obs: 'Educativo' }
    ]
  }
])

const dialogNovaFicha = ref(false)
const novaFichaTitulo = ref('')

const dialogNovaSerie = ref(false)
const fichaSelecionadaId = ref<number | null>(null)
const novaSerie = ref<Serie>({
  repeticoes: 4,
  distanciaM: 100,
  nado: 'Crawl',
  pausaSeg: 20,
  obs: ''
})

const salvarNovaFicha = () => {
  if (novaFichaTitulo.value.trim() && fichas.value.length < 5) {
    fichas.value.push({
      id: Date.now(),
      titulo: novaFichaTitulo.value,
      series: []
    })
    novaFichaTitulo.value = ''
    dialogNovaFicha.value = false
  }
}

const duplicarFicha = (ficha: Ficha) => {
  if (fichas.value.length >= 5) {
    alert('Limite de 5 fichas ativas atingido!')
    return
  }
  fichas.value.push({
    id: Date.now(),
    titulo: ficha.titulo + ' (Copia)',
    series: JSON.parse(JSON.stringify(ficha.series))
  })
}

const excluirFicha = (id: number) => {
  fichas.value = fichas.value.filter(f => f.id !== id)
}

const abrirAdicionarSerie = (fichaId: number) => {
  fichaSelecionadaId.value = fichaId
  dialogNovaSerie.value = true
}

const salvarNovaSerie = () => {
  const ficha = fichas.value.find(f => f.id === fichaSelecionadaId.value)
  if (ficha) {
    ficha.series.push({ ...novaSerie.value })
    dialogNovaSerie.value = false
  }
}
</script>
