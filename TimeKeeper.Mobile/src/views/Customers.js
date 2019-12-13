import React, { Component } from "react";
import { getNews } from "../server";
import { SafeAreaView, View, FlatList, Text } from "react-native";
import Lista from "../components/ListCustomer";
import Constants from 'expo-constants';

import { Icon, Header, Left } from 'native-base'
import theme from '../assets/Theme';

import { customers } from "../services/api";

export default class Customers extends Component {
  static navigationOptions = {
    header: null
  }
  state = {
    data: [],
  };
  

  async componentDidMount() {
    const data = await customers();
    if (data.length > 0) {
      this.setState({ data });
    }
  }

  openItem = (item) => {
    console.log('inside open item method');
    this.props.navigation.navigate("CustomerProfile", { item });
  }


  render() {
    const { result } = this.state;
    return (
      <View style={styles.container}>
        <Header style={styles.head}>
          <Left>
            <Icon style={styles.icon} name="ios-menu" onPress={() =>
              this.props.navigation.openDrawer()
            } />
          </Left>
          <Text style={styles.header}>CUSTOMERS</Text>
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
