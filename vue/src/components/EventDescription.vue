<template>
  <div class="main-description">
    <h1>{{ event.eventName }}</h1>
    <h3>Hosted by: {{ event.hostName }}</h3>
    <h4>
      {{ event.eventDate }} from {{ event.startTime }} until {{ event.endTime }}
    </h4>
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

          <table>
            <thead>
              <tr>
                <th>Song Title</th>
                <th>Artist</th>
                <th>Add ShoutOut</th>
              </tr>
            </thead>

            <tr v-for="song in activePlaylist" v-bind:key="song.songId">
              <td>
                <v-avatar rounded size="60">
                  <img v-bind:src="song.imgUrl" alt="" />
                </v-avatar>
              </td>

              <td id="song-artist-info">
                {{ song.songName }} - {{ song.artistName }}
              </td>

            </tr>
          </table>
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
                <th>Up Vote</th>
                <th>Down Vote</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="song in possibleSongs" v-bind:key="song.songId">
                <td>
                  <v-avatar rounded size="60">
                    <img v-bind:src="song.imgUrl" alt="" />
                  </v-avatar>
                </td>
                <td id="song-artist-info">
                  {{ song.songName }} - {{ song.artistName }}
                </td>
                <td>
                  <v-btn
                    small
                    v-if="!song.hasUpvoted && ($store.state.user.userId != event.djUserId)"
                    id="likebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="green darken-3"
                    v-model="totalVotes"
                    v-on:click="upVote(song.songId)"
                  >
                    <v-icon dark> mdi-thumb-up </v-icon>
                  </v-btn>

                  <v-btn
                    small
                    v-if="song.hasUpvoted && ($store.state.user.userId != event.djUserId)"
                    id="likebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="grey darken-2"
                  >
                    <v-icon dark> mdi-thumb-up </v-icon>
                  </v-btn>
                </td>
                <td>
                  <v-btn
                    small
                    v-if="!song.hasDownvoted && ($store.state.user.userId != event.djUserId)"
                    id="dislikebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="red darken-2"
                    v-model="totalVotes"
                    v-on:click="downVote"
                  >
                    <v-icon dark> mdi-thumb-down </v-icon>
                  </v-btn>
                  <v-btn
                    small
                    v-if="song.hasDownvoted && ($store.state.user.userId != event.djUserId)"
                    id="likebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="grey darken-2"
                  >
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
import EventsService from "../services/EventsService.js";

export default {
  data() {
    return {
      /* the possible playlist needs to be set when a user clicks on an event. 
      A get method needs to run that retrieves the playlist by its id. */
      dialog: false,
      activePlaylist: [],
      possibleSongs: [],
      songVote: {
        PlaylistId: "",
        SongId: "",
        VoteValue: "",
      },
      songShoutout: {},
      totalVotes: 0
    };
  },
  created() {
    EventsService.getEvents().then((resp) => {
      this.$store.commit("SET_EVENTS", resp.data);
    });
    SongsService.getPossibleSongs(this.$route.params.id).then((response) => {
      this.possibleSongs = response.data;
    });
    SongsService.getPlaylistByEvent(this.$route.params.id).then((resp) => {
      this.activePlaylist = resp.data;
    });
  },
  computed: {
    event: function() {
      return this.$store.state.events.find((event) => {
      return event.eventId == this.$route.params.id;
    });
    }
  },
  methods: {
    upVote(currentSong) {
      this.song.hasUpvoted = true;
      this.song.hasDownvoted = false;
      let songVote = {
        PlaylistId: this.$route.params.id,
        SongId: currentSong,
        VoteValue: 1
      }
      SongsService.vote(songVote)
        .then((response) => {
          if (response.status == 200) {
            console.log(response);
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - ${error.response.statusText}`
          );
        });
    },
  downVote() {
    this.song.hasDownvoted = true;
    this.song.hasUpvoted = false;
    this.songVote.PlaylistId = this.event.eventId;
    this.songVote.SongId = this.song.songId;
    this.songVote.VoteValue = -1;
    SongsService.vote(this.songVote)
      .then((response) => {
        if (response.status == 200) {
          console.log(response);
        }
      })
      .catch((error) => {
        alert(`Error: ${error.response.status} - ${error.response.statusText}`);
      });
  },
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

a {
  text-decoration: none;
}

</style>