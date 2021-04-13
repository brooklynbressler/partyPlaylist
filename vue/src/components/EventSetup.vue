<template>
  <div>
    <v-form
      id="setup-form"
      ref="form"
      lazy-validation
      v-on:submit.prevent="submitEventSetup"
    >
      <v-text-field
        v-model="event.EventName"
        :rules="rules.name"
        :counter="60"
        label="Name"
        required
        dark
      ></v-text-field>

      <v-textarea
        v-model="event.EventDescription"
        :rules="rules.description"
        :counter="145"
        label="Description"
        height="50"
        required
        dark
      >
      </v-textarea>

      <v-autocomplete
        :items="['Alternative', 'Country', 'Hip Hop', 'Pop', 'Rock']"
        label="Exclude Genres"
        v-model="genres"
        multiple
        dark
      ></v-autocomplete>
      <v-btn type="submit" class="form-button" color="white" x-large text>
        Save
      </v-btn>
    </v-form>
  </div>
</template>

<script>
import SongsService from "../services/SongsService.js";
import EventsService from "../services/EventsService.js";

export default {
  data() {
    return {
      event: {
        DjUserId: 0,
        HostUserId: "",
        PlaylistId: 2,
        EventName: "",
        EventDescription: "",
        EventDate: "",
        StartTime: "",
        EndTime: "",
      },
      genres: [],
      rules: {
        name: [(val) => (val || "").length > 0 || "This field is required"],
        description: [
          (val) => (val || "").length > 0 || "This field is required",
        ],
      },
    };
  },
  created() {
    EventsService.getEvents().then((resp) => {
      this.$store.commit("SET_EVENTS", resp.data);
    });
  },
  computed: {
    eventToSetup() {
      return this.$store.state.events.find((event) => {
        return event.eventId == this.$route.params.id;
      });
    },
  },
  methods: {
    submitEventSetup() {
      console.log(this.event);
      console.log(this.genres);

      SongsService.genres(this.$route.params.id, this.genres)
        .then((response) => {
          if (response.status === 200) {
            SongsService.createPossibleSongs(this.$route.params.id).then(
              (response) => {
                if (response.status === 200) {
                  this.$router.push({ name: "events" });
                }
              }
            );
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - ${error.response.statusText}`
          );
        })
    },
  },
};
</script>

<style>
#setup-form {
  width: 25%;
  height: 100vh;
  background: rgba(0, 0, 0, 0.4);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.8);
  margin: auto;
  padding: 10em 3em 3em 3em;
  text-align: center;
  font-weight: bold;
}

.form-button {
  color: white;
}
</style>