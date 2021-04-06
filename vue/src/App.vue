<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" app>
      <!--  -->
      <router-link :to="{name: 'home'}">
        <v-btn class="nav-button" block color="primary" depressed elevation="6" rounded>HOME</v-btn>
      </router-link>

      <router-link :to="{ name: 'events' }"> 
        <v-btn class="nav-button" block color="primary" depressed elevation="6" rounded>SEE EVENTS</v-btn>
      </router-link>      

      <router-link :to="{ name: 'register' }" v-if="$store.state.isLoggedIn === false">
        <v-btn class="nav-button" block color="primary" depressed elevation="6" rounded>NEED AN ACCOUNT?</v-btn>
      </router-link>      

      <router-link :to="{ name: 'create-event' }" v-if="$store.state.isLoggedIn === true">
        <v-btn class="nav-button" block color="primary" depressed elevation="6" rounded>ADD AN EVENT</v-btn>
      </router-link>  

      <router-link :to="{name: 'login'}" v-if="$store.state.isLoggedIn === false">
        <v-btn class="nav-button" block color="primary" depressed elevation="6" rounded>SIGN IN</v-btn>
      </router-link>    

      <router-link :to="{ name: 'logout' }" v-if="$store.state.isLoggedIn === true">
        <v-btn class="nav-button" block color="primary" depressed elevation="6" rounded>SIGN OUT</v-btn>
      </router-link>

    </v-navigation-drawer>
    <v-app-bar app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>Application</v-toolbar-title>
    </v-app-bar>

    <v-main>
      <div id="app">
        <router-view />
      </div>
    </v-main>
  </v-app>
</template>

<script>
export default {
  data: () => ({ drawer: null }),
};
</script>

<style>
.nav-button {
  margin-top: 40px;
  
}
</style>