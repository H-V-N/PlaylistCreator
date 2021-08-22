import { LoginManager } from '@/utils/login-manager';
import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Init',
    component: () => import(/* webpackChunkName: "init" */ '../views/Init.vue')
  },
  {
    path: '/callback',
    name: 'Callback',
    component: () =>
      import(/* webpackChunkName: "callback" */ '../views/Callback.vue')
  },
  {
    path: '/search',
    name: 'Search',
    component: () =>
      import(/* webpackChunkName: "about" */ '../views/Search.vue')
  },
  {
    path: '/playlist/:id',
    name: 'Playlist',
    component: () =>
      import(/* webpackChunkName: "playlist" */ '../views/Playlist.vue'),
    props: true
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: () =>
      import(/* webpackChunkName: "dashboard" */ '../views/Dashboard.vue')
  }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  if (to.name === 'Callback' || to.name === 'Init') return next();
  LoginManager.checkAccessToken()
    .then(() => next())
    .catch(() => next({ name: 'Init' }));
});

export default router;
