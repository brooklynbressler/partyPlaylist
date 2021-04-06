<template>
  <div class="event-description">
      <p id="hosted-by">hosted by: {{$store.state.activeEvent.EventHost}}</p>
      <h1>{{$store.state.activeEvent.EventName}}</h1>
      <h2>{{$store.state.activeEvent.EventDate}}</h2>
      <h3>{{$store.state.activeEvent.EventDescription}}</h3>  
      <p>Party time from {{$store.state.activeEvent.StartTime}} to {{$store.state.activeEvent.EndTime}}</p>    
  </div>
</template>

<script>
export default {

}
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

#hosted-by {
    
}
</style>