import { Avatar, List, ListItem, ListItemText } from '@material-ui/core';
import FotoUsuario from '../../../logo.svg';
import React from 'react';
import { Link } from 'react-router-dom';

export const DrawerRight = ({
    classes,
    usuario,
    salirSesion
}) => (
    <div className={classes.list}>
        <List>
            <ListItem button component={Link}>
                <Avatar src={usuario.foto || FotoUsuario}/>
                <ListItemText classes={{primary : classes.listItemText}} primary={usuario ? usuario.nombreCompleto : null}/>
            </ListItem>
            <ListItem button onClick={salirSesion}>
                <ListItemText classes={{primary : classes.listItemText}} primary='Salir'/>
            </ListItem>
        </List>
    </div>
);