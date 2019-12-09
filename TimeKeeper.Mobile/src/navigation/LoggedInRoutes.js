import { createBottomTabNavigator } from "react-navigation-tabs";

import { createStackNavigator } from "react-navigation-stack";
import People from "../views/People";
import Profile from "../views/Profile";
import Calendar from "../views/Calendar";

const StackNavigator = createStackNavigator({
  Profile: {
    screen: Profile
  },
  Calendar: {
    screen: Calendar
  }
});

const LoggedInRoutes = createBottomTabNavigator({
  People: {
    screen: People
  },
  Profile: {
    screen: StackNavigator
  }
});

export default LoggedInRoutes;
