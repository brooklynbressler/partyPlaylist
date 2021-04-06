<template>
  <div class="form-div" justify="space-around" align="center">
    <form class="form-add-event" v-on:submit.prevent="createNewEvent">
      <h1 class="h3 mb-3 font-weight-normal">Create An Event</h1>
      <div>
        <label for="eventname">New Event Name: </label>
        <input
          type="text"
          id="eventname"
          class="form-control"
          placeholder="enter event name"
          v-model="event.EventName"
        />
      </div>
      <div>
        <label for="eventdescription">Event Description: </label>
        <textarea
          id="eventdescription"
          name="w3review" 
          placeholder="Enter Event Description"
          rows="4" 
          cols="50"
          v-model="event.EventDescription"
        ></textarea>
      </div>

      <div>
        <v-row justify="space-around" align="center">
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>Date:</h2>
            <v-date-picker id="date" v-model="event.EventDate"></v-date-picker>
          </v-col>
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>Start:</h2>
            <v-time-picker
              id="start"
              v-model="event.StartTime"
              :max="end"
            ></v-time-picker>
          </v-col>
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>End:</h2>
            <v-time-picker
              id="end"
              v-model="event.EndTime"
              :min="start"
            ></v-time-picker>
          </v-col>
        </v-row>
      </div>
      <div>
        <button type="submit">Save Event</button>
      </div>
    </form>
  </div>
</template>

<script>
import EventsService from "../services/EventsService.js";

export default {
  name: "create-event",
  data() {
    return {
      event: {
        DjUserId: 0,
        HostUserId: 0,
        PlaylistId: 0,
        EventName: "",
        EventDescription: "",
        EventDate: "",
        StartTime: "",
        EndTime: "",
      },
    };
  },
  methods: {
    createNewEvent() {
      EventsService.addEvent(this.event)
        .then((response) => {
          console.log(response.status);
          if (response.status === 201) {
            this.$router.push({ name: "events" });
          }
        })
        .catch((error) => {
          alert(
            `Error: ${error.response.status} - DANG! ${error.response.statusText}`
          );
        });
    },
  },
};
</script>

<style>
.form-div {
  border: solid black;
  padding: 25px;
}

div {
  margin-bottom: 15px;
}
</style>