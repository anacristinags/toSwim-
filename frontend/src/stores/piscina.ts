import { defineStore } from 'pinia'
import piscinaService, { type ConfigPiscinaPayload } from '@/services/piscinaService'

export const usePiscinaStore = defineStore('piscina', {
    state: () => ({
        tamanhoM: 25,
        formaContagem: 0,
        carregado: false,
        possuiConfiguracao: false
    }),
    actions: {
        async carregar() {
            const config = await piscinaService.obterConfiguracao()

            if (config) {
                this.tamanhoM = config.tamanhoM
                this.formaContagem = config.formaContagem
                this.possuiConfiguracao = true
            }

            this.carregado = true
        },
        async salvar(dados: ConfigPiscinaPayload) {
            const config = this.possuiConfiguracao
                ? await piscinaService.atualizarConfiguracao(dados)
                : await piscinaService.criarConfiguracao(dados)

            this.tamanhoM = config.tamanhoM
            this.formaContagem = config.formaContagem
            this.carregado = true
            this.possuiConfiguracao = true

            return config
        }
    }
})
