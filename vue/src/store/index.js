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
    isLoggedIn: false,
    token: currentToken || '',
    user: currentUser || {},
    events: [],
    possiblePlaylist: [],
    activePlaylist: [],    
    activeEvent: {
        DjUserId: 0,
        HostUserId: 0,
        PlaylistId: 0,
        EventHost: "Party Greg",
        EventName: "Event Name",
        EventDate: "01-01-1999",
        EventDescription: "A party for people who love to party. Let's celebrate our love of parties as we party like it's our last party. Partying is life, life is partying. I struggle to conceptualize myself when I don't party. I party, therefore I am.",
        StartTime: "4:30 pm",
        EndTime: "8:45 pm",
    },
  },
  mutations: {
    SET_EVENTS(state, events) {
      state.events = events;
    },
    SET_ACTIVE_EVENT(state, data) {
      state.activeEvent = data;
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
    FLIP_LOGIN_STATUS(state) {
      state.isLoggedIn = !state.isLoggedIn;
    }
  }
})
