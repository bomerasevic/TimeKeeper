import React from "react";
import { Text, TouchableOpacity, StyleSheet } from "react-native";
import theme from '../assets/Theme';

const Button = ({ onPress, children, outline }) => {
  return (
    <TouchableOpacity onPress={onPress} style={outline ? styles.outline : styles.button}>
      <Text style={styles.title}>{children}</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  button: {
    height: 60,
    width: '100%',
    backgroundColor: theme.COLORS.BUTTONCOLOR,
    borderRadius: 5,
    justifyContent: 'center',
    marginTop:20
  },
  title: {
    color: theme.COLORS.WHITE,
    alignSelf: 'center'
  },
  outline: {
    width: '85%',
    height: 60,
    backgroundColor: theme.COLORS.BUTTONCOLOR,
    borderRadius: 5,
    borderColor: theme.COLORS.DEFAULT,
    borderWidth: 1,
    justifyContent: 'center',
    marginTop:20
  }
})


export { Button };
