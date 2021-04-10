<template>
  <div>
    <div
      class="event-div"
      v-for="event in $store.state.events"
      v-bind:key="event.eventId"
    >
      <h1>{{ event.eventName }}</h1>
      <h2>{{ event.eventDate }}</h2>
      <p>
        This event will go from {{ event.startTime }} to {{ event.endTime }}
      </p>
      <div class="button-spacing">
      <router-link
        :to="{
          name: 'eventDescription',
          params: { id: event.eventId },
        }"
        class="addMessage"
      >
        <v-btn color="primary" elevation="6" raised rounded x-small
          >details</v-btn
        >
      </router-link>
      <router-link
        class="update-event"
        v-if="$store.state.user.userId == 0"
        :to="{ name: 'updateEvent', params: { id: event.eventId } }"
      >
        <v-btn elevation="2" icon medium rounded>
          <v-icon>mdi-pencil</v-icon>
        </v-btn>
      </router-link>
      <p>DJ user ID is: {{ event.djUserId }}</p>
      </div>
    </div>
  </div>
</template>

<!--
$store.state.user.userId
-->

<script>
export default {};
</script>

<style>
.event-div {
  text-align: center;
  margin: auto;
  margin-top: 15px;
  padding: 10px;
  border: solid black;
  border-radius: 10px;
  width: 60%;
}
.button-spacing {
  display: flex;
  justify-content: space-around;
}
</style>