import Axios from "axios";
import {
    FETCH_MONTHLY_REPORT,
    FETCH_MONTHLY_REPORT_SUCCESS,
    FETCH_MONTHLY_REPORT_FAILURE
} from "./types";
import Config from "../../config";
export const getMonthlyReport = (selectedYear, selectedMonth) => {
    return dispatch => {
        dispatch({ type: FETCH_MONTHLY_REPORT });
        Axios.get("http://192.168.60.72/TimeKeeper/api/report/monthly-overview-stored/" + selectedYear + "/" + selectedMonth, Config.authHeader)
            .then(res => {
                console.log("data", res.data);
                let tableHead = res.data.projects.map(x => x.name);
                tableHead.unshift("Employee");
                tableHead.push("PTO");
                let data = res.data.employees.map(x => {
                    return {
                        ...x.hours,
                        employee: x.employee.name,
                        totalHours: x.totalHours,
                    }
                });
                dispatch({ type: FETCH_MONTHLY_REPORT_SUCCESS, payload: { data: data, tableHead: tableHead } });
            })
            .catch(err => {
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