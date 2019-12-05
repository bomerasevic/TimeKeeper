import React, { Component } from 'react';
import { View, Image,Text } from 'react-native';
import { Button } from '../components';
import {Input} from '../components'
export default class Login extends Component {
  _login = () => {
    console.log("I pressed Login Button");
  }

  render() {
    return (
      <View style={styles.container}>

         <Image  style={styles.logo}   
          source={require('../../assets/logomodal.png')}
        />
        <Text style={styles.mainTitle}>Login to your account</Text>
        <Text style={styles.title}> Save time for doing great work.</Text>
        <Input style={styles.username} placeholder={"Name"} autoCompleteType={'username'}></Input>
        <Input style={styles.password}  placeholder={"Password"} autoCompleteType={'password'}></Input>
        <Button onPress={this._login} outline>Login</Button> 
      </View>
    )
  }
}

const styles = {
  container: {
    flex: 1, 
    
    backgroundColor: 'white',
    justifyContent: 'flex-start',
    alignItems: 'center',
    padding: 10
  },
  logo:{
    marginTop:10
  },
  mainTitle: {
    fontFamily: 'Roboto',
    fontSize:25,    
    fontWeight: 'bold',
    marginTop:10,

  },
  title:{
    marginTop:10,
    marginBottom:30,
    fontSize:20, 
  },
 
}