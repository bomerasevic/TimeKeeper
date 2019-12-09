import React from "react";
import { View, TextInput } from "react-native";

const Input = ({
  placeholder,
  secureTextEntry,
  onChangeText,
  name,
  keyboardType,
  autoCompleteType,
  style,
  value
}) => {
  return (
    <View style={styles.container}>
      <TextInput
        style={[styles.inputBox, style]}
        underlineColorAndroid="rgb(57, 54, 67, 0.9)"
        placeholder={placeholder}
        placeholderTextColor="rgb(57, 54, 67, 0.9)"
        secureTextEntry={secureTextEntry}
        onChangeText={onChangeText}
        name={name}
        autoCompleteType={autoCompleteType}
        selectionColor="white"
        keyboardType={keyboardType}
        onChangeText={onChangeText}
        value={value}
      />
    </View>
  );
};

const styles = {
  inputBox: {
    width: 300,
    backgroundColor: "white",
    color: "#80cbc4",
    borderRadius: 20,
    marginBottom: 10,
    marginTop:10,
    paddingHorizontal: 15,
    paddingVertical: 10
  }
};

export { Input };
