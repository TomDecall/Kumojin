import axios, { Method } from 'axios';
import { NotifyToast } from 'src/services/common/notifyService'

const client = axios.create({
  baseURL: process.env.VUE_APP_API_ENDPOINT,
  headers: {
    'Access-Control-Allow-Origin': '*',
  },
});

export default {
  async execute_Api(method: Method, resource: string, data?: object) {
    return client({
      method: method,
      url: resource,
      data: JSON.stringify(data),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(
      response => {
        return response.data;
      }).catch(
        error => {
          NotifyToast.ShowFail("Erreur lors de la communication avec le serveur")
        });
  },
};
