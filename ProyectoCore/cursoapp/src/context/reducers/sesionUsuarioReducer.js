export const initialState = {
    usuario : {
        nombreCompleto : '',
        email : '',
        username : '',
        foto : ''
    },
    authenticate : false
}

const sesionUsuarioReducer = (state = initialState, action) => {

    switch(action.type){
        case "INICIAR_SESION" : 
            return {
                ...state,
                usuario : action.sesion,
                authenticate : action.authenticate
            };
        case "CERRAR_SESION":
            return {
                ...state,
                usuario : action.nuevoUsuario,
                authentication : action.authenticate
            };
        case "ACTUALIZAR_USUARIO" :
            return {
                ...state,
                usuario : action.nuevoUsuario,
                authentication : action.authenticate
            };
        default :
            return state;
    }
};

export default sesionUsuarioReducer;