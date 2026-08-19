import { computed, ref } from 'vue'

const CHAVE_TOKEN = 'user_token'

const token = ref<string | null>(localStorage.getItem(CHAVE_TOKEN))

export const autenticado = computed(() => !!token.value)

export const obterToken = (): string | null => token.value

export const estaAutenticado = (): boolean => !!token.value

export const salvarToken = (novoToken: string): void => {
    token.value = novoToken
    localStorage.setItem(CHAVE_TOKEN, novoToken)
}

export const removerToken = (): void => {
    token.value = null
    localStorage.removeItem(CHAVE_TOKEN)
}
