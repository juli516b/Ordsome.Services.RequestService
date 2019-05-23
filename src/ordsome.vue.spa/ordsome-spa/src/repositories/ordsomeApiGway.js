import axios from 'axios'

export default {

  async fetchTranslationRequests () {
    const response = await axios.get();
        return response;
  },

  async fetchTranslationRequest (id) {
    const response = await axios.get(id);
      return response;
  }
}