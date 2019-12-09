import {
	CUSTOMERS_FETCH_START,
	CUSTOMERS_FETCH_SUCCESS,
	CUSTOMERS_FETCH_FAIL,
	CUSTOMER_FETCH_START,
	CUSTOMER_FETCH_SUCCESS,
	CUSTOMER_FETCH_FAIL,
	CUSTOMER_SELECT,
	CUSTOMER_CANCEL,
	CUSTOMER_EDIT_START,
	CUSTOMER_EDIT_FAIL,
	CUSTOMER_EDIT_SUCCESS,
	CUSTOMER_ADD_START,
	CUSTOMER_ADD_SUCCESS,
	CUSTOMER_ADD_FAIL,
	CUSTOMER_DELETE_START,
	CUSTOMER_DELETE_FAIL,
	CUSTOMER_DELETE_SUCCESS
} from "./actionTypes";
import {
	customersUrl,
	apiGetAllRequest,
	apiGetOneRequest,
	apiPutRequest,
	apiPostRequest,
	apiDeleteRequest
} from "../../utils/api";

const customersFetchStart = () => {
	return {
		type: CUSTOMERS_FETCH_START
	};
};

const customersFetchSuccess = (data) => {
	return {
		type: CUSTOMERS_FETCH_SUCCESS,
		data
	};
};

const customersFetchFail = (error) => {
	return {
		type: CUSTOMERS_FETCH_FAIL,
		error
	};
};

export const fetchCustomers = () => {
	return (dispatch) => {
		dispatch(customersFetchStart());
		apiGetAllRequest(customersUrl)
			.then((res) => {
				dispatch(customersFetchSuccess(res.data.data));
			})
			.catch((err) => dispatch(customersFetchFail(err)));
	};
};

export const customerSelect = (id, mode) => {
	return {
		type: CUSTOMER_SELECT,
		id,
		mode
	};
};

const customerFetchStart = () => {
	return {
		type: CUSTOMER_FETCH_START
	};
};

const customerFetchFail = (error) => {
	return {
		type: CUSTOMER_FETCH_FAIL,
		error
	};
};

const customerFetchSuccess = (data) => {
	return {
		type: CUSTOMER_FETCH_SUCCESS,
		data
	};
};

export const fetchCustomer = (id) => {
	return (dispatch) => {
		dispatch(customerFetchStart());
		apiGetOneRequest(customersUrl, id)
			.then((res) => {
				return dispatch(customerFetchSuccess(res.data.data));
			})
			.catch((err) => dispatch(customerFetchFail(err)));
	};
};

export const customerCancel = () => {
	return {
		type: CUSTOMER_CANCEL
	};
};

const customerEditStart = () => {
	return {
		type: CUSTOMER_EDIT_START
	};
};

const customerEditFail = (error) => {
	return {
		type: CUSTOMER_EDIT_FAIL,
		error
	};
};

const customerEditSuccess = () => {
	return {
		type: CUSTOMER_EDIT_SUCCESS,
		reload: "customerEditReload"
	};
};

export const customerPut = (id, body) => {
	return (dispatch) => {
		dispatch(customerEditStart());
		apiPutRequest(customersUrl, id, body)
			.then((res) => {
				dispatch(customerEditSuccess());
				dispatch(customerCancel());
			})
			.catch((err) => {
				dispatch(customerEditFail(err));
			});
	};
};

const customerAddStart = () => {
	return {
		type: CUSTOMER_ADD_START
	};
};

const customerAddFail = (error) => {
	return {
		type: CUSTOMER_ADD_FAIL,
		error
	};
};

const customerAddSuccess = () => {
	return {
		type: CUSTOMER_ADD_SUCCESS,
		reload: "customerAddReload"
	};
};

export const customerAdd = (body) => {
	return (dispatch) => {
		dispatch(customerAddStart());
		apiPostRequest(customersUrl, body)
			.then((res) => {
				dispatch(customerAddSuccess());
				dispatch(customerCancel());
			})
			.catch((err) => dispatch(customerAddFail(err)));
	};
};

const customerDeleteStart = () => {
	return {
		type: CUSTOMER_DELETE_START
	};
};

const customerDeleteFail = (error) => {
	return {
		type: CUSTOMER_DELETE_FAIL,
		error
	};
};

const customerDeleteSuccess = () => {
	return {
		type: CUSTOMER_DELETE_SUCCESS,
		reload: "customerDeleteReload"
	};
};

export const customerDelete = (id) => {
	return (dispatch) => {
		dispatch(customerDeleteStart());
		apiDeleteRequest(customersUrl, id)
			.then((res) => {
				dispatch(customerDeleteSuccess());
				dispatch(customerCancel());
			})
			.catch((err) => dispatch(customerDeleteFail(err)));
	};
};
