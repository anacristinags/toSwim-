import type { APIRequestContext } from '@playwright/test'

export interface AtletaRegistrado {
  nome: string
  email: string
  senha: string
  token: string
}

/**
 * Gera um e-mail único por execução para evitar colisão de registros
 * (o campo é UNIQUE no banco) entre execuções e workers em paralelo.
 */
export function gerarEmailUnico(prefixo = 'atleta'): string {
  const sufixo = `${Date.now()}-${Math.floor(Math.random() * 100000)}`
  return `${prefixo}.${sufixo}@toswim.test`
}

/**
 * Registra um novo atleta via API e retorna os dados usados + o token JWT
 * emitido, para reaproveitar em testes que dependem de um usuário autenticado.
 */
export async function registrarAtleta(
  request: APIRequestContext,
  overrides: Partial<{ nome: string; email: string; senha: string }> = {},
): Promise<AtletaRegistrado> {
  const nome = overrides.nome ?? 'Atleta de Teste'
  const email = overrides.email ?? gerarEmailUnico()
  const senha = overrides.senha ?? 'senha123'

  const response = await request.post('/auth/registro', {
    data: { nome, email, senha },
  })

  if (!response.ok()) {
    throw new Error(`Falha ao registrar atleta de teste: ${response.status()} ${await response.text()}`)
  }

  const body = await response.json()
  return { nome, email, senha, token: body.token ?? body.Token }
}
