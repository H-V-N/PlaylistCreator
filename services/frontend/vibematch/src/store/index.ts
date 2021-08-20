import Vue from 'vue';
import Vuex from 'vuex';
import StatisticsModule from './statistics';

Vue.use(Vuex);

const store = new Vuex.Store({
  modules: {
    statistics: StatisticsModule
  }
});

export default store;
