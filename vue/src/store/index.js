import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if (currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    events: [
      {
        id: 1,
        name: 'Bass Party',
        date: '01-01-2021',
        start: '28:00',
        end: '23:00'
      },
      {
        id: 2,
        name: 'White Russian Party',
        date: '04-10-2020',
        start: '12:00',
        end: '22:00'
      },
      {
        id: 3,
        name: 'I dunno',
        date: '01-01-2023',
        start: '05:00',
        end: '09:00'
      }
    ],
  },
  mutations: {
    SET_EVENTS(state, events) {
      state.events = events;
    },
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user', JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
  }
})
