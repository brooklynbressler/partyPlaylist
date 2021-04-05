<template>
  <div>
      <div class="event-div" v-for="event in $store.state.events" v-bind:key="event.id">
          <h1>{{event.host}}</h1>
          <h2>{{event.name}}</h2>
          <p>This event will take place on {{event.date}}</p>
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