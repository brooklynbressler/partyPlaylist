import axios from 'axios';

export default {
    getEvents() {
        return axios.get('/events')
    },

    addEvent(newEvent) {
        console.log(newEvent);
        return axios.post('/create-event', newEvent)
    }
}