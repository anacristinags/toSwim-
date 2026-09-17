import { test, expect } from '@playwright/test'
import { cabecalhoAuth, registrarAtleta } from '../../utils/auth'

test.describe('GET /piscina-configuracao/me', () => {
  test('retorna 401 sem token de autenticação', async ({ request }) => {
    const response = await request.get('/piscina-configuracao/me')
    expect(response.status()).toBe(401)
  })

  test('retorna 404 quando o atleta ainda não configurou a piscina', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.get('/piscina-configuracao/me', {
      headers: cabecalhoAuth(token),
    })

    expect(response.status()).toBe(404)
    const body = await response.json()
    expect(body.erro).toBeTruthy()
  })
})

test.describe('POST /piscina-configuracao', () => {
  test('cria a configuração de piscina do atleta autenticado', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.post('/piscina-configuracao', {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 25, formaContagem: 0 },
    })

    expect(response.status()).toBe(201)
    const body = await response.json()
    expect(body).toMatchObject({
      tamanhoM: 25,
      formaContagem: 0,
      status: 1,
    })
    expect(body.codPiscina ?? body.CodPiscina).toBeTruthy()
  })

  test('rejeita tamanho de piscina inválido', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.post('/piscina-configuracao', {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 30, formaContagem: 0 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita forma de contagem fora do range permitido (0-1)', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.post('/piscina-configuracao', {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 25, formaContagem: 5 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita segunda configuração para o mesmo atleta', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    await request.post('/piscina-configuracao', {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 25, formaContagem: 0 },
    })

    const response = await request.post('/piscina-configuracao', {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 50, formaContagem: 1 },
    })

    expect(response.status()).toBe(400)
  })
})

test.describe('PUT /piscina-configuracao/me', () => {
  test('atualiza a configuração existente', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    await request.post('/piscina-configuracao', {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 25, formaContagem: 0 },
    })

    const response = await request.put('/piscina-configuracao/me', {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 50, formaContagem: 1 },
    })

    expect(response.status()).toBe(200)
    const body = await response.json()
    expect(body).toMatchObject({ tamanhoM: 50, formaContagem: 1 })
  })
})
