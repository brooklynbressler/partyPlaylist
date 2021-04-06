<template>
  <div id="login" class="text-center">
    <h1>Please Sign In</h1>

    <v-form class="form-signin" @submit.prevent="login">
      <v-container>
        <v-row>
          <v-col cols="12" sm="6">
            <div
              class="alert alert-danger"
              role="alert"
              v-if="invalidCredentials"
            >
              Invalid username and password!
            </div>
            <div
              class="alert alert-success"
              role="alert"
              v-if="this.$route.query.registration"
            >
              Thank you for registering, please sign in.
            </div>
            <v-text-field
              id="username"
              label="Username"
              outlined
              v-model="user.username"
              required
              autofocus
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" sm="6">
            <v-text-field
              id="password"
              label="Password"
              outlined
              v-model="user.password"
              :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
              :type="show1 ? 'text' : 'password'"
              @click:append="show1 = !show1"
              required
              autofocus
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="6" sm="3">
            <v-btn
              large
              block
              class="btn btn-lg btn-primary btn-block"
              @click="login"
              >
              Login
            </v-btn>
          </v-col>
        </v-row>

      </v-container>
    </v-form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      show1: false,
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$store.commit("FLIP_LOGIN_STATUS");
            this.$router.push("/");
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>
