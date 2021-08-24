import Vue from 'vue';
import Vuex from 'vuex';
import StatisticsModule from './statistics';

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    error: ''
  },
  mutations: {
    setError(state, payload: string) {
      state.error = payload;
    }
  },
  modules: {
    statistics: StatisticsModule
  }
});

export default store;
