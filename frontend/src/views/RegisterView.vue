<template>
  <v-container class="fill-height justify-center">
    <v-responsive max-width="480">
      <v-card class="pa-6 pa-md-8 elevation-4 rounded-lg">
        <div class="text-center mb-6">
          <v-icon icon="mdi-account-plus" size="48" color="primary" class="mb-2"></v-icon>
          <h1 class="text-h5 font-weight-bold text-primary">Criar Conta no toSwim</h1>
          <p class="text-body-2 text-medium-emphasis">Cadastre-se para acompanhar sua performance na natação</p>
        </div>

        <v-form v-model="formValido" @submit.prevent="cadastrar">
          <v-text-field
            v-model="nome"
            label="Nome Completo"
            prepend-inner-icon="mdi-account-outline"
            variant="outlined"
            density="comfortable"
            class="mb-3"
            :rules="[v => !!v || 'Nome é obrigatório']"
            required
          ></v-text-field>

          <v-text-field
            v-model="email"
            label="E-mail"
            prepend-inner-icon="mdi-email-outline"
            type="email"
            variant="outlined"
            density="comfortable"
            class="mb-3"
            :rules="[
              v => !!v || 'E-mail é obrigatório',
              v => /.+@.+\..+/.test(v) || 'E-mail deve ser válido'
            ]"
            required
          ></v-text-field>

          <v-text-field
            v-model="senha"
            label="Senha"
            prepend-inner-icon="mdi-lock-outline"
            :type="exibirSenha ? 'text' : 'password'"
            :append-inner-icon="exibirSenha ? 'mdi-eye-off' : 'mdi-eye'"
            @click:append-inner="exibirSenha = !exibirSenha"
            variant="outlined"
            density="comfortable"
            class="mb-3"
            :rules="[
              v => !!v || 'Senha é obrigatória',
              v => v.length >= 6 || 'Mínimo de 6 caracteres'
            ]"
            required
          ></v-text-field>

          <v-text-field
            v-model="confirmarSenha"
            label="Confirmar Senha"
            prepend-inner-icon="mdi-lock-check-outline"
            :type="exibirSenha ? 'text' : 'password'"
            variant="outlined"
            density="comfortable"
            class="mb-4"
            :rules="[
              v => !!v || 'Confirmação de senha é obrigatória',
              v => v === senha || 'As senhas não coincidem'
            ]"
            required
          ></v-text-field>

          <v-btn
            type="submit"
            color="primary"
            block
            size="large"
            elevation="2"
            :disabled="!formValido"
          >
            Cadastrar Atleta
          </v-btn>
        </v-form>
      </v-card>
    </v-responsive>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const formValido = ref(false)
const nome = ref('')
const email = ref('')
const senha = ref('')
const confirmarSenha = ref('')
const exibirSenha = ref(false)

const cadastrar = () => {
  if (formValido.value) {
    alert('Atleta cadastrado estaticamente com sucesso!')
  }
}
</script>
