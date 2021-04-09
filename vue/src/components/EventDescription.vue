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
      <v-card class="mx-auto" max-width="500">
        <v-toolbar color="deep-purple accent-4" dark>
          <v-toolbar-title>Available Songs To Pick</v-toolbar-title>

          <v-spacer></v-spacer>

          <v-btn icon>
            <v-icon>mdi-music</v-icon>
          </v-btn>
        </v-toolbar>

        <v-list class="playlist">
          <v-list-item v-for="song in possibleSongs" :key="song.songId">
            <v-list-item-avatar rounded size="60">
              <v-img :alt="`${song.songName} avatar`" :src="chat.albumCover"></v-img>
            </v-list-item-avatar>

            <v-list-item-content>
              <v-list-item-title v-text="`${song.songName} - ${song.artist}`"></v-list-item-title>
            </v-list-item-content>

            <v-list-item-icon>
              <v-icon :color="chat.active ? 'deep-purple accent-4' : 'grey'">
                mdi-music-note-plus
              </v-icon>
            </v-list-item-icon>
          </v-list-item>
        </v-list>
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
      ],
      activePlaylist: [], 
      event: {},
      recent: [
        {
          active: true,
          albumCover:
            "https://images-na.ssl-images-amazon.com/images/I/71NT9ty1FNL._SL1500_.jpg",
          title: "Africa",
          artist: 'TOTO'
        },
        {
          active: true,
          albumCover:
            "https://upload.wikimedia.org/wikipedia/en/thumb/0/07/The_Clash_-_Combat_Rock.jpg/220px-The_Clash_-_Combat_Rock.jpg",
          title: "Rock the Casbah",
          artist: 'The Clash'
        },
        {
          albumCover:
            "https://upload.wikimedia.org/wikipedia/en/1/17/LikeAVirgin1984.png",
          title: "Material Girl",
          artist: 'Madonna'
        },
        {
          albumCover:
            "https://upload.wikimedia.org/wikipedia/en/4/42/Tina_Turner_Private_Dancer_US_CD_cover_art_1984_original.jpg",
          title: "What's Love Got to Do With It",
          artist: 'Tina Turner'
        },
        {
          active: true,
          albumCover:
            "https://img.discogs.com/MCcPigiGvOGxXXisER-AjWAr1OQ=/fit-in/600x602/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-621396-1474406704-8024.jpeg.jpg",
          title: "Melt With You",
          artist: 'Modern English'
        },
      ],
      possibleSongs: [],
      // excludedGenres: {},
    };
  },
  created() {
    const excludedGenres = ["rock", "country"];
    this.event = this.$store.state.events.find((event) => {
      return event.eventId == this.$route.params.id;
    });
    console.log(this.event.eventId);
    console.log(excludedGenres);
    SongsService.getPossibleSongs(excludedGenres).then((response) => {
      this.possibleSongs = response.data;
    });
    // SongsService.getSongs().then(
    //   songs => {
    //     if (songs.status == 200) {
    //       this.possiblePlaylist = songs;
    //     }
    //   }
    // ).catch(error => {
    //     alert(`Error: ${error.response.status} - ${error.response.statusText}`)
    // });

    // created() {
    //     SongsService.getSongByEvent(this.eventId).then((response) => {
    //         this.$store.commit("SET_EVENT_PLAYLIST", response.data);
    //     });
    // }

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
</style>