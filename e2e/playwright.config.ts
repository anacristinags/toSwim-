import { defineConfig, devices } from '@playwright/test'
import path from 'node:path'
import { fileURLToPath } from 'node:url'

// Carrega variáveis de e2e/.env (se existir), sem depender de dotenv como dependência.
try {
  const dotenv = await import('node:fs')
  const envPath = path.join(path.dirname(fileURLToPath(import.meta.url)), '.env')
  if (dotenv.existsSync(envPath)) {
    for (const line of dotenv.readFileSync(envPath, 'utf-8').split('\n')) {
      const match = line.match(/^\s*([\w.-]+)\s*=\s*(.*)?\s*$/)
      if (match && !process.env[match[1]]) process.env[match[1]] = match[2] ?? ''
    }
  }
} catch {
  // Sem .env definido — segue com os defaults abaixo.
}

const FRONTEND_URL = process.env.FRONTEND_URL ?? 'http://localhost:5173'
const API_URL = process.env.API_URL ?? 'http://localhost:5064'
const AUTOSTART_WEB = process.env.WEB_SERVER_AUTOSTART !== 'false'

export default defineConfig({
  testDir: './tests',
  fullyParallel: true,
  forbidOnly: !!process.env.CI,
  retries: process.env.CI ? 2 : 0,
  workers: process.env.CI ? 1 : undefined,
  reporter: process.env.CI ? [['html'], ['github']] : [['html'], ['list']],

  use: {
    trace: 'on-first-retry',
    screenshot: 'only-on-failure',
    video: 'retain-on-failure',
  },

  projects: [
    {
      name: 'web',
      testDir: './tests/ui',
      use: {
        ...devices['Desktop Chrome'],
        baseURL: FRONTEND_URL,
      },
    },
    {
      name: 'api',
      testDir: './tests/api',
      use: {
        baseURL: API_URL,
      },
    },
  ],

  // Inicia automaticamente o frontend (Vite) antes dos testes de UI.
  // A API (.NET + Postgres) precisa estar de pé manualmente — ver e2e/README.md.
  webServer: AUTOSTART_WEB
    ? {
        command: 'npm run dev',
        cwd: path.join(path.dirname(fileURLToPath(import.meta.url)), '..', 'frontend'),
        url: FRONTEND_URL,
        reuseExistingServer: true,
        timeout: 60_000,
      }
    : undefined,
})
