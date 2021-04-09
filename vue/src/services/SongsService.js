import axios from 'axios';

export default {
    getSongs() {
        return axios.get('/songs');
    },
    vote(newVote) {
        return axios.post('/vote', newVote);
    }
}