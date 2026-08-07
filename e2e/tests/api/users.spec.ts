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
