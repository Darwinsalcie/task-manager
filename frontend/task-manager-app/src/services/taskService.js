import axios from 'axios';

const API_URL = 'https://localhost:7215/api/taskitem';

export const taskService = {
  getAll: async () => {
    try {
      const response = await axios.get(API_URL);
      return response.data;
    } catch (error) {
      throw error.response?.data || error;
    }
  },

  getByUserId: async (userId) => {
    try {
      const response = await axios.get(`${API_URL}/user/${userId}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || error;
    }
  },

  getById: async (id) => {
    try {
      const response = await axios.get(`${API_URL}/${id}`);
      return response.data;
    } catch (error) {
      throw error.response?.data || error;
    }
  },

  create: async (taskData) => {
    try {
      const response = await axios.post(API_URL, taskData);
      return response.data;
    } catch (error) {
      throw error.response?.data || error;
    }
  },

  update: async (id, taskData) => {
    try {
      const response = await axios.put(`${API_URL}/${id}`, taskData);
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
  }
};
