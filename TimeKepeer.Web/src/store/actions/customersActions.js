import config from "../../config"
import axios from "axios"
import { LOAD_CUSTOMERS, LOAD_CUSTOMERS_SUCCESS } from "./types"
export const loadCustomers = () => {
    return async (dispatch) => {
        dispatch({ type: LOAD_CUSTOMERS });
        const response = await axios.get(`${config.apiUrl}Customers`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: config.token
            }
        });
        dispatch({ type: LOAD_CUSTOMERS_SUCCESS, payload: response.data });
    };
};