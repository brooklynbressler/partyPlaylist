<template>
  <div class="form-div">
    <form class="form-add-event" v-on:submit.prevent="createNewEvent">
      <h1 class="h3 mb-3 font-weight-normal">Create An Event</h1>
      <div>
        <label for="eventname">Event Name</label> <br />
        <input
          type="text"
          id="eventname"
          class="form-control"
          placeholder="enter event name"
          v-model="event.EventName"
        />
      </div>
      <div>
        <label for="date">Date</label> <br />
        <input
          type="date"
          id="date"
          class="form-control"
          v-model="event.EventDate"
        />
      </div>
      <div>
        <label for="start">Start Time</label> <br />
        <input
          type="time"
          id="start"
          class="form-control"
          v-model="event.StartTime"
        />
      </div>
      <div>
        <label for="end">End Time</label> <br />
        <input type="time" id="end" class="form-control" v-model="event.EndTime" />
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
        DjUserId: 1,
        HostUserId: 2,
        PlaylistId: 1,
        EventName: "",
        EventDate: "",
        StartTime: "",
        EndTime: "",
      },
    };
  },
  methods: {
    createNewEvent() {
      console.log("trying to create new event");
      EventsService.addEvent(this.event)
        .then((response) => {
          if (response.status === 201) {
            this.$router.push({ name: 'events' });
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