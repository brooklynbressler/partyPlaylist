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
    getPossibleSongs(excludedGenres){
        console.log("excluded genres");
        console.log(excludedGenres);

        return axios.get(`/possible-songs`, excludedGenres)
    }
}