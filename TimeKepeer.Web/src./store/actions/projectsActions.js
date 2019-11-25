import config from "../../config"
import axios from "axios"
import { LOAD_PROJECTS, LOAD_PROJECTS_SUCCESS } from "./types"
export const loadProjects = () => {
    return async (dispatch) => {
        dispatch({ type: LOAD_PROJECTS });
        const response = await axios.get(`${config.apiUrl}Projects`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: config.token
            }
        });
        dispatch({ type: LOAD_PROJECTS_SUCCESS, payload: response.data });
    };
};