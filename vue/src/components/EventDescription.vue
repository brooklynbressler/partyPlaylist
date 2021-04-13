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

    <div>
      <iframe src="https://open.spotify.com/embed/playlist/6L6CG4GP8xINEizIW0erbh" width="800" height="240" frameborder="0" allowtransparency="true" allow="encrypted-media"></iframe>
    </div>

    <div class="playlist-display">
      <div class="playlist-div-left">
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

              <td>
                <v-btn
                  v-if="$store.state.user.userId != event.djUserId"
                  color="primary"
                  elevation="6"
                  raised
                  rounded
                  x-small
                  v-on:click="showShoutout(song.songId)"
                  >add shoutout</v-btn
                >
                <input
                  v-if="$store.state.user.userId != event.djUserId"
                  type="text"
                  v-show="song.songId == showShoutoutIndex"
                  placeholder="enter shoutout"
                  v-model="newSongShoutout.ShoutOutMessage"
                />
                <v-btn
                  v-if="$store.state.user.userId == event.djUserId"
                  class="mx-2"
                  fab
                  dark
                  small
                  color="primary"
                  v-on:click="removeFromThePlaylist(song)"
                >
                  <v-icon dark> mdi-minus </v-icon>
                </v-btn>
              </td>
              <td v-if="hasShoutout(song.songId)">
                <!-- MAKE SHOUTOUT ICON VISIBLE IF SONG HAS SHOUTOUTS -->
                <!-- CHANGE V-IF CONDITION -->
                <!-- ADD V-ON:CLICK TO TRIGGER SHOUTOUT POPUP WINDOW -->
                <!-- v-if="hasShoutout(song.songId)" -->
                <v-btn
                  small
                  id="show-shoutout"
                  class="mx-2"
                  icon
                  outlined
                  fab
                  dark
                  color="blue darken-3"
                >
                  <v-icon dark> mdi-bullhorn </v-icon>
                </v-btn>
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
                    v-if="
                      !song.hasUpvoted &&
                      $store.state.user.userId != event.djUserId
                    "
                    id="likebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="green darken-3"
                    v-model="totalVotes"
                    v-on:click="upVote(song)"
                  >
                    <v-icon dark> mdi-thumb-up </v-icon>
                  </v-btn>

                  <v-btn
                    small
                    v-if="
                      song.hasUpvoted &&
                      $store.state.user.userId != event.djUserId
                    "
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

                  <v-btn
                    small
                    v-if="$store.state.user.userId == event.djUserId"
                    class="mx-2"
                    fab
                    dark
                    color="indigo"
                    v-on:click="addToPlaylist(song)"
                  >
                    <v-icon dark> mdi-plus </v-icon>
                  </v-btn>
                </td>
                <td>
                  <v-btn
                    small
                    v-if="
                      !song.hasDownvoted &&
                      $store.state.user.userId != event.djUserId
                    "
                    id="dislikebtn"
                    class="mx-2"
                    icon
                    outlined
                    fab
                    dark
                    color="red darken-2"
                    v-model="totalVotes"
                    v-on:click="downVote(song)"
                  >
                    <v-icon dark> mdi-thumb-down </v-icon>
                  </v-btn>
                  <v-btn
                    small
                    v-if="
                      song.hasDownvoted &&
                      $store.state.user.userId != event.djUserId
                    "
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
                  <div v-if="$store.state.user.userId == event.djUserId">
                    <h3>{{ song.songScore }}</h3>
                  </div>
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
        PlaylistId: 0,
        SongId: 0,
        VoteValue: 0,
      },
      addRemoveSong: {
        PlaylistId: 0,
        SongId: 0,
      },
      newSongShoutout: {
        PlaylistId: 0,
        SongId: 0,
        ShoutOutMessage: "",
      },
      totalVotes: 0,
      event: {},
      showShoutoutIndex: -1,
      shoutoutText: "",
      eventShoutOuts: [
        {
        playlistId: 1,
        songId: 9,
        shoutoutId: 1,
        shoutoutMessage: "Message 1"
        },
        {
        playlistId: 1,
        songId: 10,
        shoutoutId: 2,
        shoutoutMessage: "Message 2"
        },
        {
        playlistId: 1,
        songId: 11,
        shoutoutId: 3,
        shoutoutMessage: "Message 3"
        },
        {
        playlistId: 1,
        songId: 9,
        shoutoutId: 4,
        shoutoutMessage: "Message 42"
        }
      ]
    };
  },
  created() {
    EventsService.getEvents().then((resp) => {
      this.$store.commit("SET_EVENTS", resp.data);
      this.event = this.$store.state.events.find((event) => {
        return event.eventId == this.$route.params.id;
      });
    });
    SongsService.getPossibleSongs(this.$route.params.id).then((response) => {
      this.possibleSongs = response.data;
    });
    SongsService.getPlaylistByEvent(this.$route.params.id).then((resp) => {
      this.activePlaylist = resp.data;
    });
  },
  methods: {
    upVote(song) {
      song.hasUpvoted = true;
      song.hasDownvoted = false;
      this.songVote.PlaylistId = this.event.eventId;
      this.songVote.SongId = song.songId;
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
    downVote(song) {
      song.hasDownvoted = true;
      song.hasUpvoted = false;
      this.songVote.PlaylistId = this.event.eventId;
      this.songVote.SongId = song.songId;
      this.songVote.VoteValue = -1;
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
    showShoutout(songId) {
      if (songId != this.showShoutoutIndex) {
        this.showShoutoutIndex = songId;
      } else {
        this.newSongShoutout.PlaylistId = this.event.eventId;
        this.newSongShoutout.songId = songId;
        SongsService.addSongShoutout(this.newSongShoutout)
          .then((response) => {
            if (response.status == 201) {
              this.showShoutoutIndex = -1;
              this.newSongShoutout.ShoutOutMessage = "";
            }
          })
          .catch((error) => {
            alert(
              `Error: ${error.response.status} - ${error.response.statusText}`
            );
          });
      }
    },
    addToPlaylist(song) {
      this.addRemoveSong.PlaylistId = this.event.eventId;
      this.addRemoveSong.SongId = song.songId;
      this.activePlaylist.push(song);
      this.possibleSongs = this.possibleSongs.filter(
        (s) => s.songId != song.songId
      );
      SongsService.addSongToPlaylist(this.addRemoveSong)
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
    removeFromThePlaylist(song) {
      this.addRemoveSong.PlaylistId = this.event.eventId;
      this.addRemoveSong.SongId = song.songId;
      this.activePlaylist = this.activePlaylist.filter(
        (s) => s.songId != song.songId
      );
      SongsService.removeSongFromPlaylist(this.addRemoveSong)
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
    hasShoutout(songId) {
      console.log(songId);
       let found = false;
       this.eventShoutOuts.forEach((shoutout) => {
         if (songId == shoutout.songId && shoutout.shoutoutMessage != ""){
           found = true;
         }
      });
      return found;
    }
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
  max-height: 100%;
}
.media-player > img {
  width: 20%;
}
.playlist-div {
  width: 90%;
  margin: auto;
  height: auto;
  max-height: 150%;
  overflow: auto;
}

.playlist-div-left {
}

a {
  text-decoration: none;
}
</style>