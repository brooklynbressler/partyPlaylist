<template>
  <div class="container">
    <event-details />
  </div>
</template>

<script>
import EventDetails from "../components/EventDetails.vue";
import EventsService from "../services/EventsService.js";

export default {
  components: {
    EventDetails,
  },
  created() {
    console.log("are we here");
    EventsService.getEvents().then((resp) => {
      this.$store.commit("SET_EVENTS", resp.data);
    });
  },
};
</script>

<style>
.container {
  text-align: center;
}
</style>