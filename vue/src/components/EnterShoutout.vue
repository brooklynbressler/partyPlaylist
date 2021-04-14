<template>
  <v-row justify="center">
  <v-dialog 
    max-width="600px"
    v-model="dialog"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          small
          icon
          outlined
          fab
          dark
          color="blue darken-3"
          v-bind="attrs"
          v-on="on"
        >
          <v-icon dark> mdi-bullhorn </v-icon>
        </v-btn>
      </template>

    <v-card>
      <v-card-title>
        <h3>Add Your Shoutout!</h3>
      </v-card-title>
      <v-card-text>
        <v-form class="px-3">
          <v-textarea label="Enter Shoutout" v-model="shoutout"></v-textarea>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          class="mx-0 mt-3"
          color="blue darken-3"
          text
          @click="dialog = false"
        >
          Cancel
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn class="mx-0 mt-3" color="blue darken-3" text @click="submit">
          Submit
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  </v-row>
</template>


<script>
export default {
  props: [],
  data() {
    return {
      dialog: false,
      shoutout: "",
      newSongShoutout: {
        PlaylistId: 0,
        SongId: 0,
        ShoutOutMessage: "",
      },

    };
  },
  methods: {
    submit() {
      console.log(this.shoutout);
      this.dialog = false;
    },
  },
};
</script>