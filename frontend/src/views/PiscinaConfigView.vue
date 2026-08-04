<template>
  <v-container max-width="800">
    <div class="mb-6">
      <h1 class="text-h5 font-weight-bold text-primary mb-1">
        <v-icon icon="mdi-pool" class="mr-2"></v-icon>Configuracao da Piscina
      </h1>
      <p class="text-body-2 text-medium-emphasis">
        Defina os parametros da sua piscina padrao para o calculo correto dos treinos e metricas.
      </p>
    </div>

    <v-card class="pa-6 rounded-lg elevation-2 mb-6">
      <v-form @submit.prevent="salvarConfig">
        <h2 class="text-subtitle-1 font-weight-bold mb-4 text-primary">Tamanho da Piscina</h2>
        <v-radio-group v-model="tamanhoPiscinaM" inline class="mb-6">
          <v-radio
            label="Piscina Curta (25 Metros)"
            :value="25"
            color="primary"
            class="mr-6"
          ></v-radio>
          <v-radio
            label="Piscina Olimpica (50 Metros)"
            :value="50"
            color="primary"
          ></v-radio>
        </v-radio-group>

        <v-divider class="my-6"></v-divider>

        <h2 class="text-subtitle-1 font-weight-bold mb-4 text-primary">Modo de Contagem Padrao</h2>
        <v-radio-group v-model="formaContagem" class="mb-6">
          <v-radio
            label="Por Distancia Absoluta em Metros (Ex: 100m, 200m)"
            :value="0"
            color="primary"
            class="mb-2"
          ></v-radio>
          <v-radio
            label="Por Numero de Voltas/Piscinas (Ex: 4 voltas, 8 voltas)"
            :value="1"
            color="primary"
          ></v-radio>
        </v-radio-group>

        <div class="d-flex justify-end">
          <v-btn
            type="submit"
            color="primary"
            size="large"
            prepend-icon="mdi-content-save"
          >
            Salvar Configuracoes
          </v-btn>
        </div>
      </v-form>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const tamanhoPiscinaM = ref<number>(25)
const formaContagem = ref<number>(0)

const salvarConfig = () => {
  alert('Configuracoes salvas estaticamente para piscina de ' + tamanhoPiscinaM.value + 'm!')
}
</script>
