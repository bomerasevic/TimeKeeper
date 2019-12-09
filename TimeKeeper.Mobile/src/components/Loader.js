import React from "react";
import { View } from "react-native";
import { ActivityIndicator } from "react-native";

const Loader = ({ size = 100, color = "green" }) => (
  <View>
    <ActivityIndicator size={size} color={color} />
  </View>
);

export default Loader;
