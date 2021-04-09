<template>
  <div>
    <table>
      <thead>
        <tr>
          <th>Album Cover</th>
          <th>Song and Artist</th>
          <th>Vote Button</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><img src={{}} alt="" v-bind="possiblePlaylist.albumCover"></td>
          <td>some song - some artist</td>
          <td>
            <v-btn id="likebtn" class="mx-2" icon outlined fab dark color="green darken-3" v-model="totalVotes" v-on:click="toggleUpvote">
              <v-icon dark> mdi-thumb-up </v-icon>
            </v-btn>

            <v-btn id="dislikebtn" class="mx-2" icon outlined fab dark color="red darken-2" v-model="totalVotes" v-on:click="toggleDownvote">
              <v-icon dark> mdi-thumb-down </v-icon>
            </v-btn>
          </td>            
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  data() {
    return {
      possiblePlaylist: [
            {
            songId: 1,
            albumCover: 'https://images-na.ssl-images-amazon.com/images/I/51uC0yguxEL.jpg',
            songName: 'Friends in Low Places',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 2,
            albumCover: 'https://images-na.ssl-images-amazon.com/images/I/51uC0yguxEL.jpg',
            songName: 'The Thunder Rolls',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 3,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/5/5a/Garth_Brooks-Ropin%27_the_Wind_%28album_cover%29.jpg',
            songName: 'The River',
            artistName: 'Garth Brooks',          
            },
            {
            songId: 4,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Boot Scootin Boogie',
            artistName: 'Brooks & Dunn',          
            },
            {
            songId: 5,
            albumCover: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/cc/BrandNewMan.jpg/220px-BrandNewMan.jpg',
            songName: 'Neon Moon',
            artistName: 'Brooks & Dunn',          
            }
        ]
    }
  }
};
</script>

<style>
</style>