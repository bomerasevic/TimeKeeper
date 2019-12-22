import axios from "axios";

import { store } from "../index";
export const loginUrl = "http://192.168.60.72/timekeeper/login"
export const employeesUrl = "http://192.168.60.72/timekeeper/api/employees";
export const customersUrl = "http://192.168.60.72/timekeeper/api/customers";
export const projectsUrl = "http://192.168.60.72/timekeeper/api/projects";


export const apiGetAllRequest = (url, method = "GET") => {
	const token = store.getState().user.user.access_token;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios(url, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const apiGetOneRequest = (url, id, method = "GET") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user.user.access_token;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios(newUrl, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const apiPutRequest = (url, id, body, method = "PUT") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user.user.access_token;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios.put(newUrl, body, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const apiPostRequest = (url, body, method = "POST") => {
	const token = store.getState().user.user.access_token;

	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios.post(url, body, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const apiDeleteRequest = (url, id, method = "POST") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user.user.access_token;

	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};

	const options = {
		method,
		headers
	};

	return axios.delete(newUrl, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const login = (url, credentials) => {
	return axios
		.post(url, credentials)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
};
