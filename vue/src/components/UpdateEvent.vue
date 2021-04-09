<template>
  <div>
      this will be an update page
      {{event}}

      <form class="form-add-event" v-on:submit.prevent="createNewEvent">
      <h1 class="h3 mb-3 font-weight-normal">Create An Event</h1>
      <div>
        <label for="eventname">New Event Name: </label>
        <input
          type="text"
          id="eventname"
          class="form-control"
          placeholder="enter event name"
          v-model="event.eventName"
          
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
          v-model="event.eventDescription"
        ></textarea>
      </div>

      <div>
        <v-row justify="space-around" align="center">
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>Date:</h2>
            <v-date-picker id="date" v-model="event.eventDate"></v-date-picker>
          </v-col>
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>Start:</h2>
            <v-time-picker
              id="start"
              v-model="event.startTime"
              :max="end"
              ampm-in-title
              :allowed-minutes="allowedMinutes"
            ></v-time-picker>
          </v-col>
          <v-col style="width: auto; flex: 0 1 auto">
            <h2>End:</h2>
            <v-time-picker
              id="end"
              v-model="event.endTime"
              :min="start"
              ampm-in-title
              :allowed-minutes="allowedMinutes"
            ></v-time-picker>
          </v-col>
        </v-row>
      </div>
      <div>
        <v-col
        class="d-flex"
        cols="12"
        sm="3"
      >
        <v-select
          :items="allItems"
          item-text="name"
          label="Select a host"
          dense
          solo
          v-model="event.hostUserId"
        ></v-select>
      </v-col>
      </div>
      <div>
        <button type="submit">Save Event</button>
      </div>
    </form>

  </div>
</template>

<script>
export default {
    data(){
        return{
            event: {}
        }
    },

    created() {
    this.event = this.$store.state.events.find((event) => {
      return event.eventId == this.$route.params.id;
    });
    },

    computed: {
        nameDefaultValue() {
            return this.event.eventName;
        }
    }

}
</script>

<style>

</style>