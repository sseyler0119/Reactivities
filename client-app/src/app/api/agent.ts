import axios, { AxiosResponse } from 'axios';
import { Activity } from '../models/activity';

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};

axios.defaults.baseURL = 'http://localhost:5000/api'; // set base url

/* every time we receive a response, we use an interceptor to apply a 1s delay*/
axios.interceptors.response.use(async (response) => {
  try {
    await sleep(1000);
    return response;
  } catch (error) {
    console.log(error);
    return await Promise.reject(error);
  }
});

// store the response.data in responseBody, pass in response of type AxiosResponse
/* add <T> for a generic type for the response body*/
const responseBody = <T>(response: AxiosResponse<T>) => response.data;

/* create an object that will store our common requests that  we will make to axios */
/* add <T> to each request to specify the return type for each request */
const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};
/* create an object that will store requests for Activities <T> in the above code allows us to
    set the return type to <Activity[]> */
const Activities = {
  list: () => requests.get<Activity[]>('/activities'), // list the activities and return the responseBody
  details: (id: string) => requests.get<Activity>(`/activities/${id}`),
  create: (activity: Activity) => requests.post<void>('/activities', activity),
  update: (activity: Activity) =>
    requests.put<void>(`/activities/${activity.id}`, activity),
  delete: (id: string) => requests.del<void>(`/activities/${id}`),
};

/* create an object that can be used to get our Activities and our list request  */
const agent = {
  Activities,
};

export default agent;
