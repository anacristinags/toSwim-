import { test, expect } from '@playwright/test'
import { registrarAtleta } from '../../utils/auth'

test.describe('GET /users/me', () => {
  test('retorna 401 sem token de autenticação', async ({ request }) => {
    const response = await request.get('/users/me')
    expect(response.status()).toBe(401)
  })

  test('retorna os dados do atleta autenticado', async ({ request }) => {
    const { nome, email, token } = await registrarAtleta(request)

    const response = await request.get('/users/me', {
      headers: { Authorization: `Bearer ${token}` },
    })

    expect(response.status()).toBe(200)
    const body = await response.json()
    expect(body).toMatchObject({ nome, email })
  })
})

test.describe('PUT /users/{id}/senha', () => {
  test('atleta A nao altera a senha do atleta B (isolamento)', async ({ request }) => {
    const atletaA = await registrarAtleta(request)
    const atletaB = await registrarAtleta(request)

    const dadosB = await request.get('/users/me', {
      headers: { Authorization: `Bearer ${atletaB.token}` },
    })
    const idB = (await dadosB.json()).codUsuario

    const response = await request.put(`/users/${idB}/senha`, {
      headers: { Authorization: `Bearer ${atletaA.token}` },
      data: {
        senhaAtual: atletaB.senha,
        novaSenha: 'senhaInvasora123',
        confirmarNovaSenha: 'senhaInvasora123',
      },
    })
    expect(response.status()).toBe(403)

    // A senha original do atleta B continua valida
    const loginB = await request.post('/auth/login', {
      data: { email: atletaB.email, senha: atletaB.senha },
    })
    expect(loginB.status()).toBe(200)
  })
})
