import { test, expect } from '@playwright/test'
import { gerarEmailUnico, registrarAtleta } from '../../utils/auth'

test.describe('POST /auth/registro', () => {
  test('registra um novo atleta e retorna token JWT', async ({ request }) => {
    const email = gerarEmailUnico()

    const response = await request.post('/auth/registro', {
      data: { nome: 'Atleta de Teste', email, senha: 'senha123' },
    })

    expect(response.status()).toBe(201)

    const body = await response.json()
    expect(body).toMatchObject({ nome: 'Atleta de Teste', email })
    expect(body.token ?? body.Token).toBeTruthy()
  })

  test('rejeita cadastro com e-mail inválido', async ({ request }) => {
    const response = await request.post('/auth/registro', {
      data: { nome: 'Atleta Inválido', email: 'nao-e-um-email', senha: 'senha123' },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita cadastro com senha curta', async ({ request }) => {
    const response = await request.post('/auth/registro', {
      data: { nome: 'Atleta Inválido', email: gerarEmailUnico(), senha: '123' },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita nome composto apenas por espacos', async ({ request }) => {
    // [Required] no ASP.NET Core faz Trim() antes de validar o tamanho da string.
    const response = await request.post('/auth/registro', {
      data: { nome: '   ', email: gerarEmailUnico(), senha: 'senha123' },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita e-mail duplicado', async ({ request }) => {
    const email = gerarEmailUnico()
    await registrarAtleta(request, { email })

    const response = await request.post('/auth/registro', {
      data: { nome: 'Outro Atleta', email, senha: 'senha123' },
    })

    expect(response.status()).toBe(400)
  })
})

test.describe('POST /auth/login', () => {
  test('autentica um atleta previamente cadastrado', async ({ request }) => {
    const { email, senha } = await registrarAtleta(request)

    const response = await request.post('/auth/login', {
      data: { email, senha },
    })

    expect(response.status()).toBe(200)
    const body = await response.json()
    expect(body.token ?? body.Token).toBeTruthy()
  })

  test('rejeita login com senha incorreta', async ({ request }) => {
    const { email } = await registrarAtleta(request)

    const response = await request.post('/auth/login', {
      data: { email, senha: 'senha-errada' },
    })

    expect(response.status()).toBe(401)
  })

  test('rejeita login de e-mail não cadastrado', async ({ request }) => {
    const response = await request.post('/auth/login', {
      data: { email: gerarEmailUnico('inexistente'), senha: 'senha123' },
    })

    expect(response.status()).toBe(401)
  })
})
