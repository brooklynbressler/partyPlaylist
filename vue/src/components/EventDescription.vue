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
              <td id="song-artist-info">{{ song.songName }} - {{ song.artistName }}</td>
              <td>
<v-row justify="center">
    <v-dialog
      v-model="dialog"
      persistent
      max-width="600px"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          color="primary"
          dark
          v-bind="attrs"
          v-on="on"
        >
          Open Dialog
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="headline">User Profile</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col
                cols="12"
                sm="6"
                md="4"
              >
                <v-text-field
                  label="Legal first name*"
                  required
                ></v-text-field>
              </v-col>
              <v-col
                cols="12"
                sm="6"
                md="4"
              >
                <v-text-field
                  label="Legal middle name"
                  hint="example of helper text only on focus"
                ></v-text-field>
              </v-col>
              <v-col
                cols="12"
                sm="6"
                md="4"
              >
                <v-text-field
                  label="Legal last name*"
                  hint="example of persistent helper text"
                  persistent-hint
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  label="Email*"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field
                  label="Password*"
                  type="password"
                  required
                ></v-text-field>
              </v-col>
              <v-col
                cols="12"
                sm="6"
              >
                <v-select
                  :items="['0-17', '18-29', '30-54', '54+']"
                  label="Age*"
                  required
                ></v-select>
              </v-col>
              <v-col
                cols="12"
                sm="6"
              >
                <v-autocomplete
                  :items="['Skiing', 'Ice hockey', 'Soccer', 'Basketball', 'Hockey', 'Reading', 'Writing', 'Coding', 'Basejump']"
                  label="Interests"
                  multiple
                ></v-autocomplete>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="blue darken-1"
            text
            @click="dialog = false"
          >
            Close
          </v-btn>
          <v-btn
            color="blue darken-1"
            text
            @click="dialog = false"
          >
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>              </td>
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
                    v-if="!song.hasUpvoted"
                    id="likebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="green darken-3"
                    v-model="totalVotes"
                    v-on:click="upVote"
                  >
                    <v-icon dark> mdi-thumb-up </v-icon>
                  </v-btn>

                  <v-btn
                    small
                    v-if="song.hasUpvoted"
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
                    v-if="!song.hasDownvoted"
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
                    v-if="song.hasDownvoted"
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

export default {
  data() {
    return {
      /* the possible playlist needs to be set when a user clicks on an event. 
      A get method needs to run that retrieves the playlist by its id. */
      dialog: false,
      activePlaylist: [],
      event: {},
      possibleSongs: [],
      songVote: {
        PlaylistId: "",
        SongId: "",
        VoteValue: "",
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
    SongsService.getPlaylistByEvent(this.event.eventId).then((resp) => {
      this.activePlaylist = resp.data;
    });
  },
  methods: {
    upVote() {
      this.song.hasUpvoted = true;
      this.song.hasDownvoted = false;
      this.songVote.PlaylistId = this.event.eventId;
      this.songVote.SongId = this.song.songId;
      this.songVote.VoteValue = 1;
      SongsService.vote(this.songVote)
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