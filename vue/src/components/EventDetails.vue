<template>
  <div>
      <div class="event-div" v-for="event in $store.state.events" v-bind:key="event.id">
          <h1>{{event.name}}</h1>
          <h2>{{event.date}}</h2>
          <p>This event will go from {{event.start}} to {{event.end}}</p>
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