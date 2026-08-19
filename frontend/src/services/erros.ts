import axios from 'axios'

export const extrairMensagemErro = (error: unknown, padrao: string): string => {
    if (axios.isAxiosError(error)) {
        const dados = error.response?.data as { erro?: string } | undefined
        return dados?.erro ?? padrao
    }

    return padrao
}
