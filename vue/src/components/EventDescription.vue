<template>
  <div class="event-description">
    <h1>{{ event.eventName }}</h1>
    <h2>{{ event.eventDate }}</h2>
    <p>
      Party time from {{ event.startTime }} to
      {{ event.endTime }}
    </p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      event: {},
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
.event-description {
  text-align: center;
  border: solid black;
  width: 75%;
  margin: auto;
  margin-top: 30px;
  padding: 20px;
}
</style>