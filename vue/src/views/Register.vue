<template>
  <div id="register" class="text-center">
    <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>

    <v-form class="form-register" @submit.prevent="register">
      <v-container>
        <v-row>
          <v-col cols="12" sm="6">
            <div
              class="alert alert-danger"
              role="alert"
              v-if="registrationErrors"
            >
              {{ registrationErrorMsg }}
            </div>
            <v-text-field
              id="firstname"
              label="First Name"
              outlined
              v-model="user.firstname"
              required
              autofocus
            ></v-text-field>
          </v-col>
        </v-row>
          <v-row>
          <v-col cols="12" sm="6">
            <v-text-field
              id="lastname"
              label="Last Name"
              outlined
              v-model="user.lastname"
              required
              autofocus
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" sm="6">
            <v-text-field
              id="username"
              label="Username"
              outlined
              v-model="user.username"
              required
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
            ></v-text-field>
          </v-col>
        </v-row>
          <v-row>
          <v-col cols="12" sm="6">
            <v-text-field
              id="confirmPassword"
              label="Confirm Password"
              outlined
              v-model="user.confirmPassword"
              :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
              :type="show2 ? 'text' : 'password'"
              @click:append="show2 = !show2"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="6" sm="3">
          <v-btn
            large
            block
            class="btn btn-lg btn-primary btn-block"
            @click="register"
          >
            Create Account
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
  name: "register",
  data() {
    return {
      show1: false,
      show2: false,
      user: {
        firstname: "",
        lastname: "",
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/login",
                query: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style></style>
