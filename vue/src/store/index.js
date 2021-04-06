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
    events: [
      {
        eventId: 1,
        eventName: "Party Time",
        eventDate: "01-01-1999",
        startTime: "5:30 pm",
        endTime: "10:30 pm",
      },
      {
        eventId: 2,
        eventName: "Another Party",
        eventDate: "01-01-2000",
        startTime: "7:30 pm",
        endTime: "12:30 pm",
      },
      {
        eventId: 3,
        eventName: "Another One",
        eventDate: "01-01-2005",
        startTime: "7:30 pm",
        endTime: "12:30 pm",
      },
    ],
    possiblePlaylist: [],
    activePlaylist: [],
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
