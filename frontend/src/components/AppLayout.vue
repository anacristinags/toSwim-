<template>
  <v-app>
    <!-- Navigation Drawer (Menu Lateral) -->
    <v-navigation-drawer v-model="drawer" persistent elevation="2">
      <v-list-item
        prepend-icon="mdi-waves"
        title="toSwim"
        subtitle="Natação & Performance"
        class="py-4 text-primary"
      ></v-list-item>

      <v-divider></v-divider>

      <v-list density="compact" nav>
        <v-list-item
          prepend-icon="mdi-clipboard-text-outline"
          title="Fichas de Treino"
          value="fichas"
          to="/fichas"
        ></v-list-item>

        <v-list-item
          prepend-icon="mdi-play-circle-outline"
          title="Executar Treino"
          value="treino-execucao"
          to="/treino-execucao"
        ></v-list-item>

        <v-list-item
          prepend-icon="mdi-target"
          title="Metas de Tempo"
          value="metas"
          to="/metas"
        ></v-list-item>

        <v-list-item
          prepend-icon="mdi-history"
          title="Histórico"
          value="historico"
          to="/historico"
        ></v-list-item>

        <v-divider class="my-2"></v-divider>

        <v-list-item
          prepend-icon="mdi-pool"
          title="Configuração Piscina"
          value="piscina"
          to="/piscina"
        ></v-list-item>

        <template v-if="autenticado">
          <v-list-item
            prepend-icon="mdi-logout-variant"
            title="Sair"
            value="sair"
            @click="sair"
          ></v-list-item>
        </template>

        <template v-else>
          <v-list-item
            prepend-icon="mdi-login-variant"
            title="Entrar"
            value="login"
            to="/login"
          ></v-list-item>

          <v-list-item
            prepend-icon="mdi-account-plus-outline"
            title="Criar Conta"
            value="cadastro"
            to="/cadastro"
          ></v-list-item>
        </template>
      </v-list>
    </v-navigation-drawer>

    <!-- Top Bar Principal -->
    <v-app-bar elevation="1">
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title class="text-subtitle-1 text-md-h6 font-weight-bold">
        toSwim App
      </v-toolbar-title>

      <v-spacer></v-spacer>

      <v-chip color="primary" variant="outlined" class="mr-4">
        <v-icon start icon="mdi-pool"></v-icon>
        Piscina: 25m
      </v-chip>
    </v-app-bar>

    <!-- Conteúdo Principal Dinâmico -->
    <v-main>
      <v-container fluid class="pa-4 pa-md-6">
        <slot></slot>
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import authService from '@/services/authService'
import { autenticado } from '@/services/tokenStorage'

const router = useRouter()
const drawer = ref(true)

const sair = () => {
  authService.logout()
  router.push({ name: 'login' })
}
</script>
