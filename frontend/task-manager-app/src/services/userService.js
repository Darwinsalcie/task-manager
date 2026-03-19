import axios from 'axios';

const API_URL = 'https://localhost:7215/api/user';
const AUTH_URL = 'https://localhost:7215/api/Auth';

export const userService = {
  getAll: async () => {
    try {
      const response = await axios.get(API_URL);
      return response.data;
    } catch (error) {
      throw error.response?.data || error;
    }
  },

  create: async (userData) => {
    try {
      const response = await axios.post(API_URL, userData);
      return response.data;
    } catch (error) {
      throw error.response?.data || error;
    }
  },

  update: async (id, userData) => {
    try {
      const response = await axios.put(`${API_URL}/${id}`, userData);
      return response.data;
    } catch (error) {
      throw error.response?.data || error;
    }
  },

  delete: async (id) => {
    try {
      const response = await axios.delete(`${API_URL}/${id}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || error;
    }
  },

  login: async (email, password) => {
    try {
      const response = await axios.post(`${AUTH_URL}/login`, { email, password });
      return response.data;
    } catch (error) {
      if (error.response?.status === 401) {
        throw { errors: ['Credenciales inválidas'] };
      }
      throw error.response?.data || error;
    }
  }
};
