import Axios from "axios";

import { store } from "../../index";
import Config from "../../config";
import {
  FETCH_COMPANY_DASHBOARD,
  FETCH_COMPANY_DASHBOARD_SUCCESS,
  FETCH_COMPANY_DASHBOARD_FAILURE
} from "./types";

export const getCompanyDashboard = (selectedYear, selectedMonth) => {
  return dispatch => {
    dispatch({ type: FETCH_COMPANY_DASHBOARD });
    const token = store.getState().user.user.token;
    console.log("token", token)
    let headers = new Headers();

    headers = {
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      }
    };
    console.log("headers", headers)
    Axios.get(
      Config.url + "api/dashboard/admin-dashboard-stored/" + selectedYear + "/" + selectedMonth, headers

    )
      .then(res => {
        dispatch({ type: FETCH_COMPANY_DASHBOARD_SUCCESS, payload: res.data });
      })
      .catch(err => {
        console.log(err);
        dispatch({ type: FETCH_COMPANY_DASHBOARD_FAILURE, payload: err });
      });
  };
};

export const startLoading = () => {
  return dispatch => {
    dispatch({ type: FETCH_COMPANY_DASHBOARD });
  };
};

