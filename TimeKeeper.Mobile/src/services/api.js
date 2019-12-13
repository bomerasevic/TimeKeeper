import axios from '../utils/axios';
import { _storeToken, _clearToken,_storeUser } from '../helpers/asyncStorage';
import { AsyncStorage } from 'react-native';
const AUTH_KEY = '@AUTH_TOKEN_KEY';
const USER = '@USER';

export const login = async (data) => {
    let axiosConfig = {
        'Content-Type': 'application/json'
    }
    const result = await axios.post('/login', data, axiosConfig);
    if (result && result.data) {
        const AUTH_TOKEN = result.data.token;
        const AUTH_USER=result.data.name;
        console.log("hi",AUTH_TOKEN);
        _storeToken(AUTH_TOKEN);
        _storeUser(AUTH_USER);
        return true;
    }
    return false;
}

export const logout = async ()=>{
    await AsyncStorage.removeItem(AUTH_KEY);
    await AsyncStorage.removeItem(USER);
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

export const customers = async () => {
    const token = await AsyncStorage.getItem(AUTH_KEY);
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
    const result = await axios.get('/api/mobile/customers');
    console.log("RUUUUUUU: ", result);
    //console.log('rezzzzzzzzzz: ', result.data);
    if (result && result.data) {       
        return result.data;
    }
    return false;
}

export const projects = async () => {
    const token = await AsyncStorage.getItem(AUTH_KEY);
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
    const result = await axios.get('/api/mobile/projects');
    console.log("RUUUUUUU: ", result);
    //console.log('rezzzzzzzzzz: ', result.data);
    if (result && result.data) {       
        return result.data;
    }
    return false;
}

export const teams = async () => {
    const token = await AsyncStorage.getItem(AUTH_KEY);
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
    const result = await axios.get('/api/mobile/teams');
    console.log("RUUUUUUU: ", result);    
    if (result && result.data) {       
        return result.data;
    }
    return false;
}
export const getCalendarMonth = async (id, year, month) => {
    const token = await AsyncStorage.getItem(AUTH_KEY);
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
    const result = await axios.get('/api/mobile/calendar' + '/' +  id + '/' + year + '/' + month);
    if (result && result.data) {       
        return result.data;
    }
    return false;
}