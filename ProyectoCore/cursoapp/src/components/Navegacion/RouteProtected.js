import React from 'react';
import { Redirect, Route } from 'react-router-dom';
import { useStateValue } from '../../context/store';

function RouteProtected({ component : Component, ...rest}){

    const [{sesionUsuario}, dispatch] = useStateValue();

    return(
        <Route
            {...rest}
            render = {(props) =>
                sesionUsuario ? (
                    sesionUsuario.authenticate == true ? (
                        <Component {...props} {...rest}/>
                    )
                    : <Redirect to="/auth/login"/>
                )
                : <Redirect to="/auth/login"/>
            }
        />
    );
}

export default RouteProtected;