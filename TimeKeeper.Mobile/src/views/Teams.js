import React, { Component } from "react";
import { getNews } from "../server";
import { SafeAreaView, View, FlatList, Text } from "react-native";
import Lista from "../components/ListTeam";
import Constants from 'expo-constants';
import { teams } from "../services/api";

import { Icon, Header, Left } from 'native-base'
import theme from '../assets/Theme';

export default class Teams extends Component {
  static navigationOptions = {
    header: null
  }
  state = {
    data: [],
  };
  

  async componentDidMount() {
    const data = await teams();
    if (data.length > 0) {
      this.setState({ data });
    }
  }

  openItem = (item) => {
    console.log('inside open item method');
    this.props.navigation.navigate("TeamProfile", { item });
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
          <Text style={styles.header}>TEAMS</Text>
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
    marginLeft: -150,
    color: theme.COLORS.LISTCOLOR,
    marginTop: 10
  },
  head: {
    backgroundColor: 'white',

  },
  icon: {
    marginLeft: -110
  }
};
