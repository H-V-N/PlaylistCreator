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
  }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

export default router;
