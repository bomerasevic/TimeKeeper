import { AsyncStorage } from 'react-native';
const AUTH_KEY = '@AUTH_TOKEN_KEY';

export const _storeToken = async AUTH_TOKEN => {
    try {
        console.log("Molim te", AUTH_TOKEN);
        await AsyncStorage.setItem(AUTH_KEY, AUTH_TOKEN);
    } catch (e) {
        console.log('errrr', e);
    }
}

export const _clearToken = async () => {
    try {
        await AsyncStorage.removeItem(AUTH_KEY);
    } catch (e) {
        console.log('errrr', e);
    }
}