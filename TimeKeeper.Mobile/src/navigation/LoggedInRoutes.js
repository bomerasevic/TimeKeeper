import { createBottomTabNavigator } from "react-navigation-tabs";

import { createStackNavigator } from "react-navigation-stack";
import People from "../views/People";
import Profile from "../views/Profile";
import Calendar from "../views/Calendar";
import List from "../components/List"
import Welcome from "../views/Welcome"
const StackNavigator = createStackNavigator({
  Profile: {
    screen: Profile
  },
  Calendar: {
    screen: Calendar
  }
});

const LoggedInRoutes = createBottomTabNavigator({
  Welcome:{
    screen:Welcome
  },
  People: {
    screen: People
  },
  Profile: {
    screen: StackNavigator
  },
  List:  {
    screen: List
  },
  
});

export default LoggedInRoutes;
