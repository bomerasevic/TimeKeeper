import { createStackNavigator } from "react-navigation-stack";
import Login from "../views/Login";

const LoggedOutRoutes = createStackNavigator({
  Login: {
    screen: Login
  }
});

export default LoggedOutRoutes;
