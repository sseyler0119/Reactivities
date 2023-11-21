import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/serverError";

export default class CommonStore {
    error: ServerError | null = null;
    token: string | null | undefined = localStorage.getItem('jwt'); 
    appLoaded = false;

    constructor() {
      makeAutoObservable(this);
      /* this will only run when an observable changes, not when the token is set (as above) and the store 
        is intitialized, the reaction only runs when the token changes
      */
      reaction(
        () => this.token,
        (token) => {
          if (token) {
            localStorage.setItem('jwt', token);
          } else {
            localStorage.removeItem('jwt');
          }
        }
      );
    }

    setServerError(error: ServerError) {
        this.error = error;
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    setAppLoaded = () => {
        this.appLoaded = true;
    }
}