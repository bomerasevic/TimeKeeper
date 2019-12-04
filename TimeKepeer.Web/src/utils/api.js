import axios from "axios";
import { store } from "../index";
export const employeesUrl = "https://localhost:44350/api/employees";
export const customersUrl = "https://localhost:44350/api/customers";
export const projectsUrl = "https://localhost:44350/api/projects";
export const apiGetAllRequest = (url, method = "GET") => {
    const token = store.getState().user.user.access_token;
    // let user = store.getState().user.user;
    let headers = new Headers();
    headers = {
        Accept: "application/json",
        Authorization: `Bearer ${token}`
    };
    const options = {
        method,
        headers
    };
    return axios(url, options)
        .then(
            (data) => ({ data }))
        .catch((error) => ({ error }));
};
export const apiGetOneRequest = (url, id, method = "GET") => {
    let newUrl = `${url}/${id}`;
    const token = store.getState().user.user.access_token;
    // let user = store.getState().user.user;
    let headers = new Headers();
    headers = {
        Accept: "application/json",
        Authorization: `Bearer ${token}`
    };
    const options = {
        method,
        headers
    };
    return axios(newUrl, options)
        .then((data) => ({ data }))
        .catch((error) => ({ error }));
};
export const apiPutRequest = (url, id, body, method = "PUT") => {
    let newUrl = `${url}/${id}`;
    const token = store.getState().user.user.access_token;
    // let user = store.getState().user.user;
    let headers = new Headers();
    headers = {
        Accept: "application/json",
        Authorization: `Bearer ${token}`
    };
    const options = {
        method,
        headers,
        body
    };
    return axios(newUrl, options)
        .then((data) => ({ data }))
        .catch((error) => ({ error }));
};