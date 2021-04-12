import axios from 'axios';

export default {
    getSongs() {
        return axios.get('/songs');
    },
    vote(songVote) {
        return axios.post('/vote', songVote);
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
    addSongShoutout(songShoutout){
        return axios.post(`/add-shoutout`, songShoutout)
    },
    addSongToPlaylist(addRemoveSong){
        return axios.post(`/add-to-playlist`, addRemoveSong)
    }
}