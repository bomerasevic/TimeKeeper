import Axios from "axios";
import {
    FETCH_PROJECT_REPORT,
    FETCH_PROJECT_REPORT_SUCCESS,
    FETCH_PROJECT_REPORT_FAILURE
} from "./types";
import Config from "../../config";
export const getProjectReport = (selectedProject) => {
    return dispatch => {
        dispatch({ type: FETCH_PROJECT_REPORT });
        Axios.get("http://192.168.60.72/TimeKeeper/api/report/project-history-report/" + selectedProject, Config.authHeader)
            .then(res => {
                console.log("data", res.data);

            })
        let data = res.data.employees.map(x => {
            return {
                employee: x.employee.name,
                total: x.totalHours,
                1: x.hours[1],
                2: x.hours[2],
                3: x.hours[3],
                4: x.hours[5],
                5: x.hours[7],
                6: x.hours[8],
                7: x.hours[9],
                8: x.hours[10]
            };
        });
        dispatch({ type: FETCH_MONTHLY_REPORT_SUCCESS, payload: data });
    })
            .catch (err => {
    console.log(err);
    dispatch({ type: FETCH_MONTHLY_REPORT_FAILURE, payload: err });
});
    };
};

export const startLoading = () => {
    return dispatch => {
        dispatch({ type: FETCH_MONTHLY_REPORT });
    };
};