<template>
 <v-row justify="center">
    <v-dialog
      v-model="dialog"
      width="600px"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn
            small
            icon
            outlined
            fab
            dark
            color="blue darken-3"
            v-bind="attrs"
            v-on="on"
        >
            <v-icon dark> mdi-bullhorn </v-icon>        
        </v-btn>
      </template>
      <v-card>
        <v-card-title>
          <span class="headline">Party Shout Out!</span>
        </v-card-title>
        <v-card-text>
            Shoutout Text Goes Here
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="green darken-1"
            text
            @click="dialog = false"
          >
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
export default {
    data () {
        return {
            dialog: false,
        }
    }
};
</script>