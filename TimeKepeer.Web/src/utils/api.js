import axios from "axios";

import { store } from "../index";
export const loginUrl = "http://192.168.60.72/timekeeper/login"
export const employeesUrl = "http://192.168.60.72/timekeeper/api/employees";
export const customersUrl = "http://192.168.60.72/timekeeper/api/customers";
export const projectsUrl = "http://192.168.60.72/timekeeper/api/projects";
export const dropDownTeamsUrl = "http://192.168.60.72/timekeeper/api/teams";
export const teamTrackingUrl = "http://192.168.60.72/timekeeper/api/dashboard/team-time-tracking";

export const apiGetAllRequest = (url, method = "GET") => {
	const token = store.getState().user.access_token;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwibmFtZSI6IlNhcmFoIEV2YW5zIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNTc3MTc4MjM0LCJleHAiOjE1Nzc3ODMwMzQsImlhdCI6MTU3NzE3ODIzNH0.y8NDIoeCpYAHHLMj-MjsppzQgjdhhUfioAcFZIF5IdY`
	};

	const options = {
		method,
		headers
	};

	return axios(url, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const apiGetOneRequest = (url, id, method = "GET") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user.token;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwibmFtZSI6IlNhcmFoIEV2YW5zIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNTc3MTc4MjM0LCJleHAiOjE1Nzc3ODMwMzQsImlhdCI6MTU3NzE3ODIzNH0.y8NDIoeCpYAHHLMj-MjsppzQgjdhhUfioAcFZIF5IdY`
	};

	const options = {
		method,
		headers
	};

	return axios(newUrl, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const apiPutRequest = (url, id, body, method = "PUT") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user.token;
	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwibmFtZSI6IlNhcmFoIEV2YW5zIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNTc3MTc4MjM0LCJleHAiOjE1Nzc3ODMwMzQsImlhdCI6MTU3NzE3ODIzNH0.y8NDIoeCpYAHHLMj-MjsppzQgjdhhUfioAcFZIF5IdY`
	};

	const options = {
		method,
		headers
	};

	return axios.put(newUrl, body, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const apiPostRequest = (url, body, method = "POST") => {
	const token = store.getState().user.token;

	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwibmFtZSI6IlNhcmFoIEV2YW5zIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNTc3MTc4MjM0LCJleHAiOjE1Nzc3ODMwMzQsImlhdCI6MTU3NzE3ODIzNH0.y8NDIoeCpYAHHLMj-MjsppzQgjdhhUfioAcFZIF5IdY`
	};

	const options = {
		method,
		headers
	};

	return axios.post(url, body, options).then((data) => ({ data })).catch((error) => ({ error }));
};

export const apiDeleteRequest = (url, id, method = "POST") => {
	let newUrl = `${url}/${id}`;

	const token = store.getState().user.token;

	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwibmFtZSI6IlNhcmFoIEV2YW5zIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNTc3MTc4MjM0LCJleHAiOjE1Nzc3ODMwMzQsImlhdCI6MTU3NzE3ODIzNH0.y8NDIoeCpYAHHLMj-MjsppzQgjdhhUfioAcFZIF5IdY`
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

	const token = store.getState().user.token;

	let headers = new Headers();

	headers = {
		Accept: "application/json",
		Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwibmFtZSI6IlNhcmFoIEV2YW5zIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNTc3MTc4MjM0LCJleHAiOjE1Nzc3ODMwMzQsImlhdCI6MTU3NzE3ODIzNH0.y8NDIoeCpYAHHLMj-MjsppzQgjdhhUfioAcFZIF5IdY`
	};

	const options = {
		method,
		headers
	};

	return axios(newUrl, options)
		.then((data) => ({ data }))
		.catch((error) => ({ error }));
};