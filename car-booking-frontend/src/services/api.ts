// src/services/api.ts
import axios from 'axios';

export const api = axios.create({
  baseURL: 'https://localhost:7209/api',  // ✅ Backend HTTPS port from launchSettings.json
  withCredentials: false
});
