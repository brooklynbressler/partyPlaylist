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
        <v-toolbar color="deep-purple accent-4" dark>
          <v-toolbar-title>DJ's Current Playlist</v-toolbar-title>

          <v-spacer></v-spacer>

          <v-btn icon>
            <v-icon>mdi-music</v-icon>
          </v-btn>
        </v-toolbar>

        <v-list class="playlist">
          <v-list-item v-for="chat in recent" :key="chat.title">
            <v-list-item-avatar rounded size="60">
              <v-img :alt="`${chat.title} avatar`" :src="chat.avatar"></v-img>
            </v-list-item-avatar>

            <v-list-item-content>
              <v-list-item-title v-text="chat.title"></v-list-item-title>
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
          <v-list-item v-for="chat in recent" :key="chat.title">
            <v-list-item-avatar rounded size="60">
              <v-img :alt="`${chat.title} avatar`" :src="chat.avatar"></v-img>
            </v-list-item-avatar>

            <v-list-item-content>
              <v-list-item-title v-text="chat.title"></v-list-item-title>
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
export default {
  data() {
    return {
      event: {},
      recent: [
        {
          active: true,
          avatar:
            "https://images-na.ssl-images-amazon.com/images/I/71NT9ty1FNL._SL1500_.jpg",
          title: "Africa - TOTO",
        },
        {
          active: true,
          avatar:
            "https://upload.wikimedia.org/wikipedia/en/thumb/0/07/The_Clash_-_Combat_Rock.jpg/220px-The_Clash_-_Combat_Rock.jpg",
          title: "Rock the Casbah - The Clash",
        },
        {
          avatar:
            "https://upload.wikimedia.org/wikipedia/en/1/17/LikeAVirgin1984.png",
          title: "Material Girl - Madonna",
        },
        {
          avatar:
            "https://upload.wikimedia.org/wikipedia/en/4/42/Tina_Turner_Private_Dancer_US_CD_cover_art_1984_original.jpg",
          title: "What's Love Got to Do With It - Tina Turner",
        },
        {
          active: true,
          avatar:
            "https://img.discogs.com/MCcPigiGvOGxXXisER-AjWAr1OQ=/fit-in/600x602/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-621396-1474406704-8024.jpeg.jpg",
          title: "Melt With You - Modern English",
        },
      ],
    };
  },

  created() {
    this.event = this.$store.state.events.find((event) => {
      return event.eventId == this.$route.params.id;
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
</style>