<template>
  <v-toolbar id="navigation" flat>
    <v-toolbar-title id="toolbar-title" class="text-uppercase white--text">
        <span class="font-weight-light">Turbo</span>
        <span>Trax</span>
    </v-toolbar-title>

    <v-spacer></v-spacer>

    <router-link :to="{ name: 'home' }">
      <v-btn class="nav" icon dark>
        <v-icon x-large>mdi-home-outline</v-icon>
      </v-btn>
    </router-link>

    <router-link :to="{ name: 'events' }">
      <v-btn class="nav" icon dark>
        <v-icon large>mdi-calendar-star</v-icon>
      </v-btn>
    </router-link>

    <router-link :to="{ name: 'register' }" v-if="$store.state.isLoggedIn === false">
        <v-btn class="nav" icon dark>
        <v-icon large>mdi-account-plus-outline</v-icon>
        </v-btn>
    </router-link>

    <router-link :to="{ name: 'createEvent' }" v-if="$store.state.isLoggedIn === true">
        <v-btn class="nav" icon dark>
        <v-icon large>mdi-calendar-plus</v-icon>        
        </v-btn>
    </router-link>

    <router-link :to="{ name: 'login' }" v-if="$store.state.isLoggedIn === false">
        <v-btn class="nav" icon dark>
        <v-icon large>mdi-login</v-icon>
        </v-btn>
    </router-link>

    <router-link :to="{ name: 'logout' }" v-if="$store.state.isLoggedIn === true">
        <v-btn class="nav" icon dark>
        <v-icon large>mdi-logout</v-icon>
        </v-btn>
    </router-link>
  </v-toolbar>
</template>


<script>
export default {};
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Syncopate&family=Train+One&display=swap');

#navigation {
    z-index: 10000;
    position: fixed;
    width: 100%;
    background: rgba(0, 0, 0, 0);
    color: white;
}
#toolbar-title {
    font-family: 'Train One', cursive;
    font-size: 1.5em;
}
</style>