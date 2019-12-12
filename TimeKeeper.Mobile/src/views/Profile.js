import React, { Component } from 'react';
import {
  StyleSheet,
  Text,
  View,
  Image,
  ImageBackground
} from 'react-native';
import { Divider } from 'react-native-elements';
import Icon from 'react-native-vector-icons/MaterialCommunityIcons';
import { LinearGradient } from 'expo-linear-gradient';
import {Button} from '../components/Button';
import { logout } from '../services/api';
//import BackgroundImage from '../../assets/abstract-beads-blur-bright-276218.jpg'
import {AsyncStorage } from "react-native";

const AUTH_KEY = '@AUTH_TOKEN_KEY';
export default class Profile extends Component {
  
  _logout = async () => {
    console.log("LOGOUT");
    
   const result =  await logout();
   console.log('prop: ', this.props)
   if (result) {
     this.props.navigation.navigate('Login');
   }
  }
  render() {
    const data = [
      {
        id: "1",
        title: "Berina Omerasevic",
        description: "berkica@gmail.com",
        position: "Developer",
        phone: "061244555"
      }
    ]
    return (
      <View style={styles.container}>
       <View ><LinearGradient style={styles.header}
          colors={['#a8edea', '#fed6e3']}
            /></View>
          <Image style={styles.avatar} source={require("../../assets/person1.jpg")}/>
          <View style={styles.body}>
              <Text style={styles.name}> {data[0].title}</Text>
              <Divider style={styles.divider} />
              <Text style={styles.info}> <Icon  name='email' size={30} color="white" /> {data[0].description}</Text>
              <Divider style={styles.divider } />
              <Text style={styles.info}> <Icon  name='account' size={30} color="white" /> {data[0].position}</Text>
              <Divider style={styles.divider} />
              <Text style={styles.info}> <Icon  name='phone' size={30} color="white" /> {data[0].phone}</Text>
              <Button onPress={this._logout} outline>Logout</Button>
        </View>
      </View>
    );
  }
}
 
const styles = {
  header: {
  backgroundColor:"#fed6e3",
   height:200,
  },
  avatar: {
    width: 130,
    height: 130,
    borderRadius: 63,
    borderWidth: 4,
    borderColor: "white",
    marginBottom:10,
    alignSelf:"center",
    position: 'absolute',
    marginTop:110,
    zIndex: 1,
  },
  name:{
    fontSize:22,
    color:"white",
    fontWeight:'700',
    marginTop: 5,
    
  },
  body:{
    paddingTop: 44,
    paddingBottom: 200,
    
    //alignItems: "center",
    justifyContent: 'center',
    backgroundColor: 'rgb(57, 54, 67)',
  },
  bodyContent: {
    flex: 1,
    alignItems: "center",
    //flexDirection: 'row',
    justifyContent: 'center',
  },
  name:{
    fontSize:28,
    paddingTop: 20,
    color: "white",
    fontWeight: "600",
    alignItems: "center",
    marginLeft: 10,
  },
  info:{
    fontSize:16,
    color: "white",
    marginTop:10,
    padding:23
  },
  description:{
    fontSize:16,
    color: "white",
    marginTop:9,
    padding: 15,
    alignItems: "center",
    textAlign: "center",
    
  }, 
  position: {
    fontSize:16,
    color: "white",
    marginTop:10,
    marginHorizontal: "auto",
    alignItems: "center",
    textAlign: "center",
  },
  divider: {
      backgroundColor: "white",
      alignItems: "center",
      flexDirection: 'row',
      justifyContent: 'center',
  },
}