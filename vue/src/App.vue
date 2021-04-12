<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" app>
      <!--  -->
      <router-link :to="{ name: 'home' }">
        <v-btn
          class="nav-button"
          block
          color="primary"
          depressed
          elevation="6"
          rounded
          ><v-icon>mdi-home-outline</v-icon> HOME</v-btn
        >
      </router-link>

      <router-link :to="{ name: 'events' }">
        <v-btn
          class="nav-button"
          block
          color="primary"
          depressed
          elevation="6"
          rounded
          ><v-icon>mdi-calendar-star</v-icon> SEE EVENTS</v-btn
        >
      </router-link>

      <router-link
        :to="{ name: 'register' }"
        v-if="$store.state.isLoggedIn === false"
      >
        <v-btn
          class="nav-button"
          block
          color="primary"
          depressed
          elevation="6"
          rounded
          ><v-icon>mdi-account-plus-outline</v-icon> NEED AN ACCOUNT?</v-btn
        >
      </router-link>

      <router-link
        :to="{ name: 'createEvent' }"
        v-if="$store.state.isLoggedIn === true"
      >
        <v-btn
          class="nav-button"
          block
          color="primary"
          depressed
          elevation="6"
          rounded
          ><v-icon>mdi-calendar-plus</v-icon> ADD AN EVENT</v-btn
        >
      </router-link>

      <router-link
        :to="{ name: 'login' }"
        v-if="$store.state.isLoggedIn === false"
      >
        <v-btn
          class="nav-button"
          block
          color="primary"
          depressed
          elevation="6"
          rounded
          ><v-icon>mdi-login</v-icon> SIGN IN</v-btn
        >
      </router-link>

      <router-link
        :to="{ name: 'logout' }"
        v-if="$store.state.isLoggedIn === true"
      >
        <v-btn
          class="nav-button"
          block
          color="primary"
          depressed
          elevation="6"
          rounded
          ><v-icon>mdi-logout</v-icon> SIGN OUT</v-btn
        >
      </router-link>
    </v-navigation-drawer>
    <v-app-bar app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title class="toolbar-title">TurboTrax</v-toolbar-title>
    </v-app-bar>

    <v-main class="v-main">
      <div id="app">
        <router-view />
        <div v-if="$route.path === '/'">
          <div class="logo-div">
            <img
              id="logo-img"
              src="https://lh3.googleusercontent.com/proxy/TmUJlkAgW9smBxoN-4h2D9lLq3tylyJJomxwgihwwlvrf4J8FKrOMjTOxvKaNDS6gjqY_8xrP8Q9oqSVC9hhmXHRPnxgnaUM1mFSJRY74EpN"
              alt=""
            />
          </div>
          <div class="description-div">
            <h1>Get your Party started with TurboTrax</h1>
            <h3 class="how-to">
              Let your friends control the music at your party.
            </h3>
            <h3>
              TurboTrax is a free app that let's your guests choose which music
              should be played.
            </h3>
          </div>
          <div class="grid-container instructions-div">
            <div class="grid-child create-playlist">
              <v-icon size="45" color="white"
                >mdi-numeric-1-circle-outline</v-icon
              >
              <br />
              <v-icon size="75" color="white"
                >mdi-playlist-music-outline</v-icon
              >
              <p>Create a custom playlist</p>
            </div>
            <div class="grid-child voting">
              <v-icon size="45" color="white"
                >mdi-numeric-2-circle-outline</v-icon
              >
              <br />
              <v-icon size="75" color="white">mdi-thumbs-up-down</v-icon>
              <p>Up-vote or down-vote songs</p>
            </div>
            <div class="grid-child played-songs">
              <v-icon size="45" color="white"
                >mdi-numeric-3-circle-outline</v-icon
              >
              <br />
              <v-icon size="75" color="white">mdi-music</v-icon>
              <p>Songs with the most votes are played</p>
            </div>
          </div>
        </div>
      </div>
    </v-main>
  </v-app>
</template>

<script>
export default {
  data: () => ({ drawer: false }),
};
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Bebas+Neue&family=Fredoka+One&display=swap");
@import url("https://fonts.googleapis.com/css2?family=Titillium+Web:wght@300&display=swap");

body {
  margin: 0;
  padding: 0;
}

.nav-button {
  margin-top: 40px;
}

.toolbar-title {
  font-family: "Bebas Neue", cursive;
  font-family: "Fredoka One", cursive;
  color: rgb(49, 118, 223);
  margin-top: 17px;
}

main {
  background-image: url(https://www.contiki.com/six-two/wp-content/uploads/2015/09/sarthak-navjivan-iTZOPe7BpTM-unsplash.jpg);
  background-size: cover;
}

.toolbar-title {
  font-family: "Titillium Web", sans-serif;
}

.grid-container {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-gap: 20px;
  text-align: center;
  width: 50%;
  margin: auto;
  margin-top: 4em;
  font-family: "Titillium Web", sans-serif;
}

#logo-img {
  width: 25%;
  margin-bottom: -1.5em;
}

.logo-div {
  text-align: center;
}

.how-to {
  margin-top: 0.2em;
}

.description-div {
  color: white;
  text-align: center;
  font-family: "Titillium Web", sans-serif;
}

.instructions-div div {
  color: white;
}

h1 {
  font-size: 4em;
}

h3 {
  font-size: 2em;
}

p {
  font-size: 1.5em;
}
</style>