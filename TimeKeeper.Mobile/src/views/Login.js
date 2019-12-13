import React, { Component } from 'react';
import { View, Image, Text } from 'react-native';
import { Button } from '../components';
import { Input } from '../components'
import LogoModal from '../../assets/logomodal.png'
import { login } from '../services/api';
export default class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      password: '',
    }
  }
  _login = async () => {
    console.log('propovi: ', this.props)
    const { username, password } = this.state;
    const data = {
      username,
      password
    }

    console.log("DATA: ", );

    const val = await login(data);
    if (val) {
      this.props.navigation.navigate('HOME')
      
    }
  }

  componentDidMount() {
    console.log("WDWADAWDADAWDAWDawDADWWADAWDAWDAWDADAWDA:", this.props);
  }

  render() {
    return (
      <View style={styles.container}>

        <Image style={styles.logo}
          source={LogoModal}
        />
        <Text style={styles.mainTitle}>Login to your account</Text>
        <Text style={styles.title}> Save time for doing great work.</Text>
        <Input style={styles.username} placeholder="Name" autoCompleteType='username' onChangeText={(text) => this.setState({username:text})}></Input>
        <Input style={styles.password} placeholder="Password" autoCompleteType='password' secureTextEntry={true} onChangeText={(text) => this.setState({password:text})}></Input>
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
  logo: {
    marginTop: 10
  },
  mainTitle: {
    //fontFamily: 'Roboto',
    fontSize: 25,
    fontWeight: 'bold',
    marginTop: 10,

  },
  title: {
    marginTop: 10,
    marginBottom: 30,
    fontSize: 20,
  },

}