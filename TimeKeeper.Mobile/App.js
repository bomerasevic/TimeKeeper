import React, { Component } from "react";
import { Provider }from "react-redux";
import configureStore from "./src/redux/configureStore";
import { StyleSheet, SafeAreaView } from "react-native";
import { createAppContainer } from "react-navigation";

import { getRootNavigator } from "./src/navigation";

export default class App extends Component {
  render() {
    const RootNavigator = createAppContainer(getRootNavigator(false));
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
