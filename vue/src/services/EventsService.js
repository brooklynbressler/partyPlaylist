import axios from 'axios';

export default {
    getEvents() {
        return axios.get('/events')
    },

    addEvent(newEvent) {
        return axios.post('/create-event', newEvent)
    }
}