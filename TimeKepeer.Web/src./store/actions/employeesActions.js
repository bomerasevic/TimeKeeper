import config from "../../config"
import axios from "axios"
import { LOAD_EMPLOYEES, LOAD_EMPLOYEES_SUCCESS } from "./types"
export const loadEmployees = () => {
    return async (dispatch) => {
        dispatch({ type: LOAD_EMPLOYEES });
        const response = await axios.get(`${config.apiUrl}employees`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: config.token
            }
        });
        dispatch({ type: LOAD_EMPLOYEES_SUCCESS, payload: response.data });
    };
};