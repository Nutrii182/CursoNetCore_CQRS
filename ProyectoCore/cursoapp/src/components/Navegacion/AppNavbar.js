import React from 'react';
import { AppBar } from '@material-ui/core';
import BarSesion from './bar/BarSesion';
import { useStateValue } from '../../context/store';

const AppNavbar = () => {

    const [{sesionUsuario}, dispatch] = useStateValue();

    return sesionUsuario
        ? (sesionUsuario.authenticate === true ? <AppBar position="static"><BarSesion/></AppBar> : null)
        : null
};

export default AppNavbar;