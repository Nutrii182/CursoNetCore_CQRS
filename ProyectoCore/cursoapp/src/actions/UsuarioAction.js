import HttpClient from '../services/HttpClient';
import axios from 'axios';

const instance = axios.create();
instance.CancelToken = axios.CancelToken;
instance.isCancel = axios.isCancel;

export const registrarUsuario = usuario => {
    return new Promise((resolve, eject) => {
        instance.post('/usuario/registrar', usuario).then(response => {
            resolve(response);
        });
    });
}

export const usuarioActual = (dispatch) => {
    return new Promise((resolve, eject) => {
        HttpClient.get('/usuario').then(response => {

            if(response.data && response.data.imagenPerfil){
                let fotoPerfil = response.data.imagenPerfil;
                const nuevoFile = 'data:image/' + fotoPerfil.extension + ';base64,' + fotoPerfil.data;
                response.data.imagenPerfil = nuevoFile;
            }
            dispatch({
                type: "INICIAR_SESION",
                sesion : response.data,
                authenticate : true
            });
            resolve(response);
        })
        .catch((error) => {
            resolve(error);
        })
    });
}

export const actualizarUsuario = (usuario, dispatch) => {
    return new Promise((resolve, eject) => {
        HttpClient.put('/usuario', usuario).then(response => {

            if(response.data && response.data.imagenPerfil){
                let fotoPerfil = response.data.imagenPerfil;
                const nuevoFile = 'data:image/' + fotoPerfil.extension + ';base64,' + fotoPerfil.data;
                response.data.imagenPerfil = nuevoFile;
            }

            dispatch({
                type : 'INICIAR_SESION',
                sesion : response.data,
                authenticate : true
            });
            
            resolve(response);
        }).catch(error => {
            resolve(error.response);
        });
    });
}

export const loginUsuario = (usuario, dispatch) => {
    return new Promise((resolve, eject) => {
        instance.post('/usuario/login', usuario).then(response => {

            if(response.data && response.data.imagenPerfil){
                let fotoPerfil = response.data.imagenPerfil;
                const nuevoFile = 'data:image/' + fotoPerfil.extension + ';base64,' + fotoPerfil.data;
                response.data.imagenPerfil = nuevoFile;
            }

            dispatch({
                type : "INICIAR_SESION",
                sesion : response.data,
                authenticate : true
            });
            
            resolve(response);
        }).catch(error => {
            resolve(error.response);
        });
    });
}