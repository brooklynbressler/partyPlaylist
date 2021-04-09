<template>
  <div>
    <div id="container">
      <h2>Voting System</h2>
      <img src="" alt="">

      <div class="voting">
          <v-btn v-if="upVoteBool === true" id="likebtn" class="mx-2" icon outlined fab dark color="green darken-3" v-model="totalVotes" v-on:click="toggleUpvote">
              <v-icon dark> mdi-thumb-up </v-icon>
          </v-btn>

          <v-btn v-if="upVoteBool === false" id="likebtn" class="mx-2" icon outlined fab dark color="grey darken-2">
              <v-icon dark> mdi-thumb-up </v-icon>
          </v-btn>          

          <v-btn v-if="downVoteBool === true" id="dislikebtn" class="mx-2" icon outlined fab dark color="red darken-2" v-model="totalVotes" v-on:click="toggleDownvote">
              <v-icon dark> mdi-thumb-down </v-icon>
          </v-btn>
          <v-btn v-if="downVoteBool === false" id="likebtn" class="mx-2" icon outlined fab dark color="grey darken-2">
              <v-icon dark> mdi-thumb-down </v-icon>
          </v-btn>
      </div>
    </div>

    <div>
        <v-list>
            <v-list-tile>
                <v-list-tile-content>

                </v-list-tile-content>
            </v-list-tile>
        </v-list>
    </div>    
  </div>
</template>

<script>
import SongsService from "../services/SongsService.js"

export default {
  data() {
    return {
        upVoteBool: true,
        downVoteBool: true,      
        songVote: {
            PlaylistId: '',
            SongId: '',
            VoteValue: ''
        },
        possiblePlaylist: [
            {
            songId: 1,
            albumCover: 'https://images-na.ssl-images-amazon.com/images/I/51uC0yguxEL.jpg',
            songName: 'Friends in Low Places',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 2,
            albumCover: 'https://images-na.ssl-images-amazon.com/images/I/51uC0yguxEL.jpg',
            songName: 'The Thunder Rolls',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 3,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/5/5a/Garth_Brooks-Ropin%27_the_Wind_%28album_cover%29.jpg',
            songName: 'The River',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 4,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Boot Scootin Boogie',
            artistName: 'Brooks & Dunn',          
            },
            {
            songId: 5,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Neon Moon',
            artistName: 'Brooks & Dunn',          
            }
        ]
    };
  },
  methods: {
      toggleUpvote() {
          this.upVoteBool = !this.upVoteBool;
          this.downVoteBool = true;
      },
      toggleDownvote() {
          this.downVoteBool = !this.downVoteBool;
          this.upVoteBool = true;
      },
      upVote() {
        const newVote = {
            PlaylistId: this.songVote.PlaylistId,
            SongId: this.songVote.SongId,
            VoteValue: 1
        }
        SongsService.vote(newVote).then(
            response => {
                if (response.status == 200) {
                    console.log(response);
                }
            }
        ).catch(error => {
        alert(`Error: ${error.response.status} - ${error.response.statusText}`)
      })
      },
      downVote() {
            const newVote = {
            PlaylistId: this.songVote.PlaylistId,
            SongId: this.songVote.SongId,
            VoteValue: -1
        }
        SongsService.vote(newVote).then(
            response => {
                if (response.status == 200) {
                    console.log(response);
                }
            }
        ).catch(error => {
        alert(`Error: ${error.response.status} - ${error.response.statusText}`)
      })
      }
   }
};
</script>

<style>
#j.container {
  border: solid black;
  padding: 40px;
  position: fixed;
  top: 50%;
  left: 50%;
  margin-top: -50px;
  margin-left: -100px;
}
</style>