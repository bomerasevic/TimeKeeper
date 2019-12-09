import {
	CUSTOMERS_FETCH_SUCCESS,
	CUSTOMERS_FETCH_START,
	CUSTOMERS_FETCH_FAIL,
	CUSTOMER_FETCH_FAIL,
	CUSTOMER_FETCH_START,
	CUSTOMER_FETCH_SUCCESS,
	CUSTOMER_SELECT,
	CUSTOMER_CANCEL,
	CUSTOMER_EDIT_SUCCESS,
	CUSTOMER_ADD_START,
	CUSTOMER_ADD_FAIL,
	CUSTOMER_ADD_SUCCESS,
	CUSTOMER_DELETE_FAIL,
	CUSTOMER_DELETE_START,
	CUSTOMER_DELETE_SUCCESS
} from "../actions/actionTypes";

const initialUserState = {
	data: [],
	loading: false,
	selected: false,
	customer: null,
	error: null,
	reload: false
};

export const customersReducer = (state = initialUserState, action) => {
	switch (action.type) {
		case CUSTOMERS_FETCH_START:
			return {
				...state,
				loading: true
			};
		case CUSTOMERS_FETCH_SUCCESS:
			return {
				...state,
				data: action.data,
				loading: false
			};
		case CUSTOMERS_FETCH_FAIL:
			return {
				...state,
				error: action.error,
				loading: false
			};
		case CUSTOMER_SELECT:
			return {
				...state,
				selected: {
					id: action.id,
					mode: action.mode
				}
			};
		case CUSTOMER_FETCH_START:
			return {
				...state
			};
		case CUSTOMER_FETCH_SUCCESS:
			return {
				...state,
				customer: action.data
			};
		case CUSTOMER_FETCH_FAIL:
			return {
				...state
			};
		case CUSTOMER_EDIT_SUCCESS:
			return {
				...state,
				reload: action.reload
			};
		case CUSTOMER_ADD_START:
			return {
				...state
			};
		case CUSTOMER_ADD_SUCCESS:
			return {
				...state,
				reload: action.reload
			};
		case CUSTOMER_ADD_FAIL:
			return {
				...state
			};
		case CUSTOMER_DELETE_START:
			return {
				...state
			};
		case CUSTOMER_DELETE_SUCCESS:
			return {
				...state,
				reload: action.reload
			};
		case CUSTOMER_DELETE_FAIL:
			return {
				...state,
				error: action.error
			};
		case CUSTOMER_CANCEL:
			return {
				...state,
				customer: null,
				selected: false
			};
		default:
			return state;
	}
};
