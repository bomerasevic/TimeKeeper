import {
	EMPLOYEES_FETCH_SUCCESS,
	EMPLOYEES_FETCH_START,
	EMPLOYEES_FETCH_FAIL,
	EMPLOYEE_FETCH_FAIL,
	EMPLOYEE_FETCH_START,
	EMPLOYEE_FETCH_SUCCESS,
	EMPLOYEE_SELECT,
	EMPLOYEE_EDIT_CANCEL
} from "../actions/actionTypes";

const initialUserState = {
	data: [],
	loading: false,
	selected: false,
	employee: null,
	error: null
};

export const employeesReducer = (state = initialUserState, action) => {
	switch (action.type) {
		case EMPLOYEES_FETCH_START:
			return {
				...state,
				loading: true
			};
		case EMPLOYEES_FETCH_SUCCESS:
			return {
				...state,
				data: action.data,
				loading: false
			};
		case EMPLOYEES_FETCH_FAIL:
			return {
				...state,
				error: action.error,
				loading: false
			};
		case EMPLOYEE_SELECT:
			return {
				...state,
				selected: {
					id: action.id,
					mode: action.mode
				}
			};
		case EMPLOYEE_FETCH_START:
			return {
				...state
			};
		case EMPLOYEE_FETCH_SUCCESS:
			console.log(action.data);
			return {
				...state,
				employee: action.data
			};
		case EMPLOYEE_FETCH_FAIL:
			return {
				...state
			};
		case EMPLOYEE_EDIT_CANCEL:
			return {
				...state,
				employee: null,
				selected: false
			};
		default:
			return state;
	}
};
