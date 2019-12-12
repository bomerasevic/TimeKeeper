import axios from '../utils/axios';
import { _storeToken, _clearToken } from '../helpers/asyncStorage';
import { AsyncStorage } from 'react-native';
const AUTH_KEY = '@AUTH_TOKEN_KEY';


export const login = async (data) => {
    let axiosConfig = {
        'Content-Type': 'application/json'
    }
    const result = await axios.post('/login', data, axiosConfig);
    if (result && result.data) {
        const AUTH_TOKEN = result.data.token;
        console.log("hi",AUTH_TOKEN);
        _storeToken(AUTH_TOKEN);
        return true;
    }
    return false;
}

export const logout = async ()=>{
    await AsyncStorage.removeItem(AUTH_KEY);
    return true;
}

export const employees = async () => {
    const token = await AsyncStorage.getItem(AUTH_KEY);
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
    const result = await axios.get('/api/mobile/people');
    console.log("RUUUUUUU: ", result);
    //console.log('rezzzzzzzzzz: ', result.data);
    if (result && result.data) {       
        return result.data;
    }
    return false;
}