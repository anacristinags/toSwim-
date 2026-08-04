import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            redirect: '/fichas'
        },
        {
            path: '/cadastro',
            name: 'cadastro',
            component: () => import('../views/RegisterView.vue')
        },
        {
            path: '/piscina',
            name: 'piscina',
            component: () => import('../views/PiscinaConfigView.vue')
        },
        {
            path: '/fichas',
            name: 'fichas',
            component: () => import('../views/FichasView.vue')
        },
        {
            path: '/treino-execucao',
            name: 'treino-execucao',
            component: () => import('../views/TreinoExecucaoView.vue')
        },
        {
            path: '/metas',
            name: 'metas',
            component: () => import('../views/MetasView.vue')
        },
        {
            path: '/historico',
            name: 'historico',
            component: () => import('../views/HistoricoView.vue')
        }
    ]
})

export default router