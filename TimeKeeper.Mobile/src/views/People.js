import React, { Component } from "react";
import { getNews } from "../server";
import { SafeAreaView, View, FlatList, Text } from "react-native";
import Lista from "../components/List";
import Constants from 'expo-constants';

import theme from '../assets/Theme';
import { Icon, Header, Left } from 'native-base'

import { employees } from "../services/api";
// _employees = async () => {  
//    const val = await employees();
//    //console.log("value", val)
// }


export default class People extends Component {
  static navigationOptions = {
    header: null
  }
  state = {
    data:[]
    
  };

  async componentDidMount() {
    const data = await employees();
    if (data.length > 0) {
      this.setState({ data });
    }
  }

  openItem = (item) => {
    console.log('inside open item method');
    this.props.navigation.navigate("EmployeeProfile", { item });
  }


  render() {
    const {navigate} = this.props.navigation;
    const { result } = this.state;
    return (

      <View style={styles.container}>
        <Header style={styles.head}>
          <Left>
            <Icon style={styles.icon} name="ios-menu" onPress={() =>
              this.props.navigation.openDrawer()
            } />
          </Left>
          <Text style={styles.header}>EMPLOYEES</Text>
        </Header>
        
        <Lista
          data={this.state.data}
          openItem={this.openItem}
        />
      </View>
    );
  }
}
const styles = {
  container: {
    flex: 1,
    marginTop: Constants.statusBarHeight,
    backgroundColor: theme.COLORS.LISTCOLOR,
  },
  list: {
    flex: 1,
    backgroundColor: theme.COLORS.LISTCOLOR,
  },
  header: {
    fontSize: 30,
    fontWeight: 'bold',
    marginLeft: -80,
    color: theme.COLORS.LISTCOLOR,
    marginTop: 10
  },
  head: {
    backgroundColor: 'white',

  },
  icon: {
    marginLeft: -65
  }
};
