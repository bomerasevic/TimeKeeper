import React, { Component } from 'react';
import { View, Image, Text, ImageBackground } from 'react-native';
import { Button } from '../components';
import { Input } from '../components'
import Puzzle from '../../assets/puzzle.png'
import BackgroundImage from '../../assets/loggedin_header.png'
 
export default class Welcome extends Component {

    render() {
        return (
            <View >
                <ImageBackground source={BackgroundImage} style={styles.background}>

                    <Image style={styles.logo}
                        source={Puzzle}
                    />
                    <Text style={styles.mainTitle}>Welcome</Text>
                    <Text style={styles.title}> This is your TimeKeeper.</Text>
                    <Text style={styles.title}> Keep doing a great job.</Text>
                </ImageBackground>
            </View>
        )
    }
}

const styles = {
    container: {
        flex: 1,

        backgroundColor: 'white',
        
        padding: 10
    },
    logo: {
        marginTop: 150,
        marginLeft:30
    },
    mainTitle: {
        fontSize: 30,
        fontWeight: 'bold',
        marginTop: 50,
        color:'white'

    },
    title: {
        marginTop: 10,
        fontSize: 20,
        color:'white'
    },
    background: {
        justifyContent: 'flex-start',
        alignItems: 'center',
       
        width: '100%',
        height: '100%',

    }

}