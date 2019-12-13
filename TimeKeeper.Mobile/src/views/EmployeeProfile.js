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
import RNModal from '../components/Modal';
import Photo from '../../assets/Beri.jpg'

//import BackgroundImage from '../../assets/abstract-beads-blur-bright-276218.jpg'
 
 
export default class Profile extends Component {
  state = {
    open: false
  };
  handleOpen = () => {
    this.setState({ open: true });
  };
  handleClose = () => {
    this.setState({ open: false });
  };
  render() {
    const item = this.props.navigation.getParam('item');
    
    return (
      <View style={styles.container}>
       <View ><LinearGradient style={styles.header}
          colors={['#a8edea', '#fed6e3']}
            /></View>
          <Image style={styles.avatar} source={Photo}/> 
          <View style={styles.body}>
            
              <Text style={styles.name}> {item.title}</Text>
              <Divider style={styles.divider} />
              <Text style={styles.info}> <Icon  name='email' size={30} color="white" /> {item.description}</Text>
              <Divider style={styles.divider } />
              <Text style={styles.info}> <Icon  name='account' size={30} color="white" /> "Developer"</Text>
              <Divider style={styles.divider} />
              <Text style={styles.info}> <Icon  name='phone' size={30} color="white" />"061 513 321"</Text>
              <Button title="Calendar" onPress={this.handleOpen}>Calendar</Button>
        <RNModal visible={this.state.open} onClose={this.handleClose} />
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