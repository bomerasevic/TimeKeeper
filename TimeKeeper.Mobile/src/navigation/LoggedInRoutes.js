import React from "react"
import { createBottomTabNavigator } from "react-navigation-tabs";
import {} from '@expo/vector-icons';
import { createStackNavigator } from "react-navigation-stack";
import People from "../views/People";
import Profile from "../views/Profile";
import Projects from "../views/Projects";
import Customers  from "../views/Customers";
import Teams from "../views/Teams";
import Calendar from "../views/Calendar";
import Welcome from "../views/Welcome";
import EmployeeProfile from "../views/EmployeeProfile";
import TeamProfile from "../views/TeamProfile"
import CustomerProfile from "../views/CustomerProfile";
import {createDrawerNavigator} from "react-navigation-drawer"
import theme from '../assets/Theme';
import Icon from 'react-native-vector-icons/MaterialCommunityIcons';
const StackNavigator = createStackNavigator({
  Profile: {
    screen: Profile
  },
  EMPLOYEES: {
    screen: People
  }
},
{
  drawerStyle:{
    backgroundColor: '#c6cbef',
    width: 240,
  } 
},);

const StackNavigatorEmployee = createStackNavigator({
  EMPLOYEES: {
    screen: People
  },
  EmployeeProfile: {
    screen: EmployeeProfile
  },
  Calendar: {
    screen: Calendar
  }
});
const StackNavigatorCustomer = createStackNavigator({
  CUSTOMERS: {
    screen: Customers
  },
  CustomerProfile: {
    screen: CustomerProfile
  }
});

const StackNavigatorTeam = createStackNavigator({
  TEAMS: {
    screen: Teams
  },
  TeamProfile: {
    screen: TeamProfile
  }
});
const StackNavigatorProject = createStackNavigator({
  TEAMS: {
    screen: Teams
  },
  EmployeeProfile: {
    screen: EmployeeProfile
  }
});

const DrawerNavigator=createDrawerNavigator({

  EMPLOYEES: {
    screen: StackNavigatorEmployee
  },
  CUSTOMERS: {
    screen: StackNavigatorCustomer
  },
  PROJECTS: {
    screen: Projects
  },
  TEAMS: {
    screen: StackNavigatorTeam
  }, 
})


const LoggedInRoutes = createBottomTabNavigator({
  
  HOME: {
    screen:Welcome,
    navigationOptions: {
      tabBarIcon: ({ tintColor }) => (
        <Icon  name='home' size={30} color={tintColor}/>
      )
    }
  },
  DATA:{
    screen:DrawerNavigator,
    navigationOptions: {
      tabBarIcon: ({ tintColor }) => (
        <Icon  name='menu' size={30} color={tintColor}/>
      )
    }
  },
  PROFILE: {
    screen: StackNavigator,
    navigationOptions: {
      tabBarIcon: ({ tintColor }) => (
        <Icon name='account' size={30} color={tintColor}/>
      )
    }    
  }},
  {
    tabBarOptions: {
      showLabel:false,
      activeTintColor: theme.COLORS.BUTTONCOLOR,
      activeColor: theme.COLORS.BUTTONCOLOR,
      inactiveTintColor: 'white',
      labelStyle: {
        fontSize: 16,
        marginBottom:15,
      },
      style: {
        backgroundColor: theme.COLORS.LISTCOLOR,
      },
    }
  },
);

export default LoggedInRoutes;
