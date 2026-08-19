<template>
  <v-container class="fill-height justify-center">
    <v-responsive max-width="480">
      <v-card class="pa-6 pa-md-8 elevation-4 rounded-lg">
        <div class="text-center mb-6">
          <v-icon icon="mdi-login-variant" size="48" color="primary" class="mb-2"></v-icon>
          <h1 class="text-h5 font-weight-bold text-primary">Entrar no toSwim</h1>
          <p class="text-body-2 text-medium-emphasis">Acesse sua conta para continuar seus treinos</p>
        </div>

        <v-alert
          v-if="mensagemErro"
          type="error"
          variant="tonal"
          density="compact"
          class="mb-4"
        >
          {{ mensagemErro }}
        </v-alert>

        <v-form v-model="formValido" @submit.prevent="entrar">
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
            class="mb-4"
            :rules="[v => !!v || 'Senha é obrigatória']"
            required
          ></v-text-field>

          <v-btn
            type="submit"
            color="primary"
            block
            size="large"
            elevation="2"
            :disabled="!formValido"
            :loading="carregando"
          >
            Entrar
          </v-btn>
        </v-form>

        <div class="text-center mt-6">
          <span class="text-body-2 text-medium-emphasis">Ainda não tem conta?</span>
          <v-btn variant="text" color="primary" density="comfortable" to="/cadastro">
            Criar conta
          </v-btn>
        </div>
      </v-card>
    </v-responsive>
  </v-container>
</template>

<script setup lang="ts">
    import { ref } from 'vue'
    import { useRoute, useRouter } from 'vue-router'
    import authService from '@/services/authService'
    import { extrairMensagemErro } from '@/services/erros'

    const router = useRouter()
    const route = useRoute()

    const formValido = ref(false)
    const email = ref('')
    const senha = ref('')
    const exibirSenha = ref(false)
    const carregando = ref(false)
    const mensagemErro = ref('')

    const entrar = async () => {
        if (!formValido.value) return

        carregando.value = true
        mensagemErro.value = ''

        try {
            await authService.login({
                email: email.value,
                senha: senha.value
            })

            const destino = typeof route.query.redirect === 'string' ? route.query.redirect : '/fichas'
            router.push(destino)
        } catch (error) {
            mensagemErro.value = extrairMensagemErro(error, 'E-mail ou senha inválidos.')
        } finally {
            carregando.value = false
        }
    }
</script>
