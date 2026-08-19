import { createRouter, createWebHistory } from 'vue-router'
import { estaAutenticado } from '@/services/tokenStorage'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            redirect: '/fichas'
        },
        {
            path: '/login',
            name: 'login',
            component: () => import('../views/LoginView.vue'),
            meta: { publica: true }
        },
        {
            path: '/cadastro',
            name: 'cadastro',
            component: () => import('../views/RegisterView.vue'),
            meta: { publica: true }
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

router.beforeEach((to) => {
    if (to.meta.publica) return true

    if (!estaAutenticado()) {
        return { name: 'login', query: { redirect: to.fullPath } }
    }

    return true
})

export default router
