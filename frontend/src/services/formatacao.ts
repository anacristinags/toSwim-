export const TIPOS_NADO = [
  { title: 'Crawl', value: 0 },
  { title: 'Costas', value: 1 },
  { title: 'Borboleta', value: 2 },
  { title: 'Peito', value: 3 }
] as const

const NOMES_NADO: Record<number, string> = {
  0: 'Crawl',
  1: 'Costas',
  2: 'Borboleta',
  3: 'Peito'
}

export const nomeNado = (tipoNado: number): string =>
  NOMES_NADO[tipoNado] ?? `Nado ${tipoNado}`

/** Formata segundos como MM:SS (ex.: 75 -> 01:15). */
export const formatarPace = (segundos: number | null | undefined): string => {
  if (segundos == null || Number.isNaN(Number(segundos))) return '--'

  const total = Math.max(0, Math.round(Number(segundos)))
  const minutos = Math.floor(total / 60)
  const resto = total % 60
  return `${String(minutos).padStart(2, '0')}:${String(resto).padStart(2, '0')}`
}

/** Formata duração longa (ex.: 2910 -> 48m 30s). */
export const formatarDuracao = (segundos: number | null | undefined): string => {
  if (segundos == null || Number.isNaN(Number(segundos))) return '--'

  const total = Math.max(0, Math.round(Number(segundos)))
  const minutos = Math.floor(total / 60)
  const resto = total % 60

  if (minutos === 0) return `${resto}s`
  if (resto === 0) return `${minutos}m`
  return `${minutos}m ${resto}s`
}

export const formatarData = (iso: string | Date): string => {
  const data = typeof iso === 'string' ? new Date(iso) : iso
  if (Number.isNaN(data.getTime())) return '--'

  return data.toLocaleDateString('pt-BR')
}
