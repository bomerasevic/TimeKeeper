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
import  Company  from '../../assets/2.mistral.jpg';
export default class Customer extends Component {

    render() {
        const item = this.props.navigation.getParam('item');

        return (
            <View style={styles.container}>
                <View ><LinearGradient style={styles.header}
                    colors={['#a8edea', '#fed6e3']}
                /></View>
                <Image style={styles.avatar} source={Company} />
                <View style={styles.body}>
                    <Text style={styles.name}> {item.name}</Text>
                    <Divider style={styles.divider} />
                    <Text style={styles.info}> <Icon name='email' size={30} color="white" /> {item.contact}</Text>
                    <Divider style={styles.divider} />
                    <Text style={styles.info}> <Icon name='account' size={30} color="white" /> {item.email}</Text>
                    <Divider style={styles.divider} />
                    <Text style={styles.info}>{item.status.name}</Text>
                    <Divider style={styles.divider} />
                    <Text style={styles.info}> <Icon name='phone' size={30} color="white" /> {item.phone}</Text>




                </View>

            </View>

        );
    }
}


const styles = {
    container: {
        flex: 1,
    },
    button: {
        flex: 1,
        justifyContent: 'flex-start',
        alignItems: 'center',
        backgroundColor: 'rgb(57, 54, 62)'
    },
    header: {
        backgroundColor: "#fed6e3",
        height: 200,
    },
    avatar: {
        width: 130,
        height: 130,
        borderRadius: 63,
        borderWidth: 4,
        borderColor: "white",
        marginBottom: 10,
        alignSelf: "center",
        position: 'absolute',
        marginTop: 110,
        zIndex: 1,
    },
    name: {
        fontSize: 22,
        color: "white",
        fontWeight: '700',
        marginTop: 5,

    },
    body: {
        paddingTop: 44,
         paddingBottom: 200,
        //alignItems: "center",
        justifyContent: 'center',
        backgroundColor: 'rgb(57, 54, 62)',
    },
    bodyContent: {
        flex: 1,
        alignItems: "center",
        //flexDirection: 'row',
        justifyContent: 'center',
    },
    name: {
        fontSize: 28,
        paddingTop: 20,
        color: "white",
        fontWeight: "600",
        alignItems: "center",
        marginLeft: 70,
        padding: 10,
    },
    info: {
        fontSize: 16,
        color: "white",
        marginTop: 10,
        marginBottom: 5,
        padding: 10,
    },
    description: {
        fontSize: 16,
        color: "white",
        marginTop: 9,
        padding: 10,
        alignItems: "center",
        textAlign: "center",

    },
    position: {
        fontSize: 16,
        color: "white",
        marginTop: 10,
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
    logout: {
        height: 50,
        width: 100,
        backgroundColor: 'black',
        borderRadius: 5,
        borderColor: 'black',
        borderWidth: 1,
        justifyContent: 'center',
        marginTop: 90,
        fontSize: 20,
        zIndex: 1,

    }



}
