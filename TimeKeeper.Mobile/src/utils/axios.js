import axios from "axios";
import { AsyncStorage } from "react-native";
const AUTH_KEY = '@AUTH_TOKEN_KEY';


const getBearerToken = async () => {
    const token = await AsyncStorage.getItem(AUTH_KEY);
    console.log('token', token);
    return 'Bearer ' + token;
}

export default axios.create({
    baseURL: 'http://192.168.60.71/timekeeper',
    headers: {
        'Content-type': 'application/json',
    }
})