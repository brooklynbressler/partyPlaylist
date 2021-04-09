<template>
  <div class="main-description">
    <h1>{{ event.eventName }}</h1>
    <h3>Hosted by: {{ event.hostName }}</h3>
    <h4>{{ event.eventDate }} from {{ event.startTime }} until {{ event.endTime }}</h4>
    <h4>Here are the details</h4>
    <div class="event-description">
      <p>{{ event.eventDescription }}</p>
    </div>

    <div class="media-player">
      <img
        src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQtX9WajqXXqPsS2GK24YylCqfKrLRJLsdhvQ&usqp=CAU"
        alt="a media player"
      />
    </div>
    <div class="playlist-display">
    <div class="playlist-div">
      <v-card class="mx-auto" max-width="500">
        <!-- Tool bar -->
        <v-toolbar color="deep-purple accent-4" dark>
          <v-toolbar-title>DJ's Current Playlist</v-toolbar-title>

          <v-spacer></v-spacer>

          <v-btn icon>
            <v-icon>mdi-music</v-icon>
          </v-btn>
        </v-toolbar>

        <!-- List -->
        <v-list class="playlist">
          <!-- loop through data source -->
          <v-list-item v-for="chat in recent" :key="chat.title">

            <!-- Image -->
            <v-list-item-avatar rounded size="60">
              <v-img :alt="`${chat.title} avatar`" :src="chat.albumCover"></v-img>
            </v-list-item-avatar>

            <!-- Title -->
            <v-list-item-content>
              <v-list-item-title v-text="`${chat.title} - ${chat.artist}`"></v-list-item-title>
            </v-list-item-content>

            <!-- Icon -->
            <v-list-item-icon>
              <v-icon :color="chat.active ? 'deep-purple accent-4' : 'grey'">
                mdi-music-note-plus
              </v-icon>
            </v-list-item-icon>
          </v-list-item>
        </v-list>
      </v-card>      
    </div>
        <div class="playlist-div">
      <v-card class="mx-auto" max-width="700">
        <v-toolbar color="deep-purple accent-4" dark>
          <v-toolbar-title>Available Songs To Pick</v-toolbar-title>

          <v-spacer></v-spacer>

          <v-btn icon>
            <v-icon>mdi-music</v-icon>
          </v-btn>
        </v-toolbar>

        <!-- songs table -->
        <table>
          <thead>
            <tr>
              <th>Song Title</th>
              <th>Artist</th>
              <th>Vote Buttons</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="song in possibleSongs" v-bind:key="song.songId">
              <td>
                <v-avatar rounded size="60">
                  <img v-bind:src="song.imgUrl" alt="">
                </v-avatar>
              </td>
              <td id="song-artist-info">{{song.songName}} - {{song.artistName}}</td>
              <td>
                <v-btn small v-if="!song.hasUpvoted" id="likebtn" class="mx-2" icon outlined fab dark color="green darken-3" v-model="totalVotes" v-on:click="upVote">
                  <v-icon dark> mdi-thumb-up </v-icon>
                </v-btn>

                <v-btn small v-if="song.hasUpvoted" id="likebtn" class="mx-2" icon outlined fab dark color="grey darken-2">
                  <v-icon dark> mdi-thumb-up </v-icon>
                </v-btn>          

                <v-btn small v-if="!song.hasDownvoted" id="dislikebtn" class="mx-2" icon outlined fab dark color="red darken-2" v-model="totalVotes" v-on:click="downVote">
                  <v-icon dark> mdi-thumb-down </v-icon>
                </v-btn>
              <v-btn small v-if="song.hasDownvoted" id="likebtn" class="mx-2" icon outlined fab dark color="grey darken-2">
                  <v-icon dark> mdi-thumb-down </v-icon>
              </v-btn>
              </td>            
            </tr>
          </tbody>
        </table>
      </v-card>
    </div>
      
    </div>
  </div>
</template>

<script>
import SongsService from "../services/SongsService.js";


export default {
  data() {
    return {
      /* the possible playlist needs to be set when a user clicks on an event. 
      A get method needs to run that retrieves the playlist by its id. */ 
      activePlaylist: [], 
      event: {},
      possibleSongs: [],
      songVote: {
        PlaylistId: '',
        SongId: '',
        VoteValue: ''
      },
    };
  },
  created() {
    this.event = this.$store.state.events.find((event) => {
      return event.eventId == this.$route.params.id;
    });
    console.log(this.event.eventId);
    SongsService.getPossibleSongs(this.event.eventId).then((response) => {
      this.possibleSongs = response.data;
    });
  },
  methods: {
    upVote() {
        this.song.hasUpvoted = true;
        this.song.hasDownvoted = false;
        this.songVote.PlaylistId = this.event.eventId;
        this.songVote.SongId = this.song.songId;
        this.songVote.VoteValue = 1;        
        SongsService.vote(this.songVote).then(
            response => {
                if (response.status == 200) {
                    console.log(response);
                }
            }
        ).catch(error => {
        alert(`Error: ${error.response.status} - ${error.response.statusText}`)
      })
    }
      },
      downVote() {
            this.song.hasDownvoted = true;
            this.song.hasUpvoted = false;
            this.songVote.PlaylistId = this.event.eventId;
            this.songVote.SongId = this.song.songId;
            this.songVote.VoteValue = -1;
            SongsService.vote(this.songVote).then(
            response => {
                if (response.status == 200) {
                    console.log(response);
                }
            }
        ).catch(error => {
        alert(`Error: ${error.response.status} - ${error.response.statusText}`)
      })
    }  
};
</script>

<style>
.main-description {
  text-align: center;
  border: solid black;
  border-radius: 3px;
  width: 75%;
  margin: auto;
  margin-top: 30px;
  padding: 20px;
}
.event-description {
  text-align: center;
  border: solid black;
  border-radius: 3px;
  width: 50%;
  margin: auto;
  margin-top: 5px;
  margin-bottom: 5px;
  padding: 10px;
}
.playlist-display {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-gap: 10px;
}
.media-player > img {
  width: 20%;
}
.playlist-div {
  width: 90%;
  margin: auto;
}
</style>