import React, { Component } from "react";
import { getNews } from "../server";
import { SafeAreaView, View, FlatList, Text } from "react-native";

const Item = ({ title }) => (
  <View>
    <Text>{title}</Text>
  </View>
);

export default class People extends Component {
  state = {
    result: [],
  };

  async componentDidMount() {
    const result = await getNews();
    this.setState({ result });
  }

  render() {
    const { result } = this.state;
    return (
      <SafeAreaView style={styles.container}>
        <FlatList
          data={result.data}
          renderItem={({ item }) => <Item title={item.title} />}
          keyExtractor={item => item.id}
        />
      </SafeAreaView>
    );
  }
}
const styles = {
  constainer: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center"
  }
};
