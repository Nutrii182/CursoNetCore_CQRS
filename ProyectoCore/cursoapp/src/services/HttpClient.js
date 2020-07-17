import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.request.use((config) => {
    const token = window.localStorage.getItem('token');
    if(token){
        config.headers.Authorization = 'Bearer ' + token;
        return config;
    }
}, error => {
    return Promise.reject(error);
});

const requestGeneric = {
    get : (url) => axios.get(url),
    post : (url, body) => axios.post(url, body),
    put : (url, body) => axios.put(url, body),
    delete : (url) => axios.delete(url)
};

export default requestGeneric;