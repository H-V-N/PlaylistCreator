import axios from "axios";

export const BackendApi = axios.create({
  baseURL: process.env.VUE_APP_URL,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json'
  }
});