import React, { Component } from 'react';
import { View, Image, Text, ImageBackground } from 'react-native';
import { Button } from '../components';
import { Input } from '../components'
import Puzzle from '../../assets/puzzle.png'
import BackgroundImage from '../../assets/loggedin_header.png'

export default class Welcome extends Component {
    static navigationOptions = {
        title: "People"
    }
    constructor(props) {
        super(props);
        this.state = {
            date: '',
            message: '',
        };
    }
    componentDidMount() {
        var that = this;

        var date = new Date().getDate(); //Current Date
        var month = new Date().getMonth() + 1; //Current Month
        var year = new Date().getFullYear();
        var hours = new Date().getHours(); 
        var temp;
        if(hours>6 && hours<12) temp="Good Morning"
        else if(hours>11 && hours<18) temp="Good Afternoon"
             
        else  temp="Good Evening";
        that.setState({
            //Setting the value of the date time
            message:temp,
            date:
                date + '/' + month + '/' + year + ' ',
        });
    }
    render() {
        return (
            <View >
                <ImageBackground source={BackgroundImage} style={styles.background}>

                    <Image style={styles.logo}
                        source={Puzzle}
                    />
                    <Text style={styles.mainTitle}>{this.state.message} Sarah Evans</Text>
                    <Text style={styles.title}> This is your TimeKeeper.</Text>
                    <Text style={styles.title}> Keep doing a great job.</Text>
                    <Text style={styles.date}>{this.state.date}</Text>
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
        marginLeft: 30
    },
    mainTitle: {
        fontSize: 30,
        fontWeight: 'bold',
        marginTop: 50,
        color: 'white'

    },
    title: {
        marginTop: 10,
        fontSize: 20,
        color: 'white'
    },
    background: {
        justifyContent: 'flex-start',
        alignItems: 'center',

        width: '100%',
        height: '100%',

    },
    date:{
        marginTop: 100,
        fontSize: 20,
        color: 'white'
    }

}