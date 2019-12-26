import axios from "axios";
import url from "../config"
import Config from "../config";
import { store } from "../index";
export const loginUrl = Config.url + "login"
export const employeesUrl = Config.url + "api/employees";
export const customersUrl = Config.url + "api/customers";
export const projectsUrl = Config.url + "api/projects";
export const dropDownTeamsUrl = Config.url + "api/teams";
export const teamTrackingUrl = Config.url + "api/dashboard/team-time-tracking";
export const companyDashboard = Config.url + "api/dashboard/admin-dashboard-stored/"
export const calendarUrl = Config.url + "api/dashboard";
export const tasksUrl = Config.url + "api/assignments";


export const getCalendar = (url, id, year, month) => {
	let newUrl = `${url}/${id}/${year}/${month}`;
	const token = store.getState().user.user.token;
	let headers = new Headers();
	headers = {
		Accept: "application/json",
		Authorization: `Bearer ${token}`
	};
	const options = {
		headers
	};
	return axios(newUrl, options)
		.then(data => ({ data }))
		.catch(error => ({ error }));
};

export const apiGetAllRequest = (url, method = "GET") => {
	console.log("TOKEN", store.getState().user.user.token);
	const token = store.getState().user.user.token;
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

	const token = store.getState().user.user.token;
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

	const token = store.getState().user.user.token;
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
	const token = store.getState().user.user.token;

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

	const token = store.getState().user.user.token;
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

export const apiGetTeamTracking = (url, team, year, month, method = "GET") => {
	let newUrl = `${url}/${team}/${year}/${month}`;

	const token = store.getState().user.user.token;

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