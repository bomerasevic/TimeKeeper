import React, { Component } from "react";
import { getNews } from "../server";
import { SafeAreaView, View, FlatList, Text } from "react-native";
import Lista from "../components/List";
import Constants from 'expo-constants';

import { Icon, Header, Left } from 'native-base'
import theme from '../assets/Theme';
const DATA = [
  {
    id: '1',
    title: 'DCS',
    description: 'Team: Alpha'
  },
  {
    id: '2',
    title: 'LB',
    description: 'Team: Bravo'
  },
  {
    id: '3',
    title: 'IMP',
    description: 'Team: Charlie'
  },
  {
    id: '4',
    title: 'TW',
    description: 'Team: Delta'
  },
  {
    id: '5',
    title: 'TDS',
    description: 'Team: Theta'
  },
];

const Item = ({ title }) => (
  <View>
    <Text>{title}</Text>
  </View>
);

export default class Projects extends Component {
  state = {
    result: [],
  };
  constructor(props) {
    super(props);
    this.state = {
      data: DATA
    };
  }

  async componentDidMount() {
    const result = await getNews();
    this.setState({ result });
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
          <Text style={styles.header}>PROJECTS</Text>
        </Header>
        <Lista
          data={this.state.data}
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
