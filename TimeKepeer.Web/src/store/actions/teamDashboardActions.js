import Axios from "axios";

import { store } from "../../index";
import Config from "../../config";
import {
  FETCH_TEAM_DASHBOARD,
  FETCH_TEAM_DASHBOARD_SUCCESS,
  FETCH_TEAM_DASHBOARD_FAILURE
} from "./types";

export const getTeamDashboard = (selectedTeam, selectedYear, selectedMonth) => {
  return dispatch => {
    dispatch({ type: FETCH_TEAM_DASHBOARD });
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
    if (selectedTeam) {
      Axios.get(
        Config.url + "api/dashboard/team-dashboard-stored/" + selectedTeam.id + "/" + selectedYear + "/" + selectedMonth, headers
      )
        .then(res => {
          dispatch({ type: FETCH_TEAM_DASHBOARD_SUCCESS, payload: res.data });
        })
        .catch(err => {
          console.log(err);
          dispatch({ type: FETCH_TEAM_DASHBOARD_FAILURE, payload: err });
        });
    } else {
      dispatch({ type: FETCH_TEAM_DASHBOARD_FAILURE, payload: "team is null" });
    }
  };
};
