import axios from 'axios';

export default {
    getSongs() {
        return axios.get('/songs');
    },
    vote(newVote) {
        return axios.post('/vote', newVote);
    },
    getPlaylistByEvent(eventId){
        return axios.get(`/playlist/${eventId}`)
    },
    getPossibleSongs(eventId){
        return axios.get(`/possible-songs/${eventId}`)
    },
    createPossibleSongs(eventId){
        return axios.post(`/create-possible-songs/${eventId}`)
    },
    addSongShoutout(){
        return axios.post(`/add-shoutout`)
    }
}