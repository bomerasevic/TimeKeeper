import {
  TEAMTRACKING_FETCH_START,
  TEAMTRACKING_FETCH_SUCCESS,
  TEAMTRACKING_FETCH_FAIL
} from "./actionTypes";
import { teamTrackingUrl, apiGetTeamTracking } from "../../utils/api";

const teamTrackingFetchStart = () => {
  return {
    type: TEAMTRACKING_FETCH_START
  };
};

const teamTrackingFetchSuccess = (data) => {
  return {
    type: TEAMTRACKING_FETCH_SUCCESS,
    data
  };
};

const teamTrackingFetchFail = (error) => {
  return {
    type: TEAMTRACKING_FETCH_FAIL,
    error
  };
};

export const fetchTeamTracking = (team, year, month) => {
  return (dispatch) => {
    dispatch(teamTrackingFetchStart());
    apiGetTeamTracking(teamTrackingUrl, team, year, month)
      .then((res) => {
        // console.log("Ovo je Berinin console.log za res", res);
        // console.log("Ovo je poslije resp.data.data", res.data.data[0]);
        dispatch(teamTrackingFetchSuccess(res.data.data));
      })
      .catch((err) => {
        console.log(err, "fetchTeamTracking err");
        dispatch(teamTrackingFetchFail(err));
      });
  };
};