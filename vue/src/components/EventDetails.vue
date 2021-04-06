<template>
  <div>
      <div class="event-div" v-for="event in $store.state.events" v-bind:key="event.eventId">
          <h1>{{event.eventName}}</h1>
          <h2>{{event.eventDate}}</h2>
          <p>This event will go from {{event.startTime}} to {{event.endTime}}</p>
      </div>
  </div>
</template>

<script>
export default {

}
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
</style>