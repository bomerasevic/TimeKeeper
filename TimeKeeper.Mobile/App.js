import React, { Component } from "react";
import { Provider }from "react-redux";
import configureStore from "./src/redux/configureStore";
import { StyleSheet, SafeAreaView, AsyncStorage } from "react-native";
import { createAppContainer } from "react-navigation";
const AUTH_KEY = `@AUTH_TOKEN_KEY`;


import { getRootNavigator } from "./src/navigation";

export default class App extends Component {
  state = {
    isLoggedIn: null
  }
  async componentDidMount() {
    const token = await AsyncStorage.getItem(AUTH_KEY);
    if (token) {
      this.setState({
        isLoggedIn: true
      })
    } else {
      this.setState({
        isLoggedIn: false
      })
    }
  }
  render() {
    const RootNavigator = createAppContainer(getRootNavigator(this.state.isLoggedIn));
    return (
      <SafeAreaView style={styles.container}>
        <RootNavigator />
      </SafeAreaView>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1
  }
});
