import React from 'react';
import { List, ListItem, ListItemText, Divider } from '@material-ui/core';
import { Link } from 'react-router-dom';

export const DrawerLeft = ({classes}) => (

    <div className={classes.list}>
        <List>
            <ListItem component={Link} button to='/auth/perfil'>
                <i className="material-icons">account_box</i>
                <ListItemText classes={{primary: classes.ListItemText}} primary="Perfil"/>
            </ListItem>
        </List>
        <Divider/>
        <List>
            <ListItem component={Link} button to='/curso/nuevo'>
                <i className="material-icons">add_box</i>
                <ListItemText classes={{primary: classes.ListItemText}} primary="Nuevo Curso"/>
            </ListItem>
            <ListItem component={Link} button to='/curso/paginador'>
                <i className="material-icons">menu_book</i>
                <ListItemText classes={{primary: classes.ListItemText}} primary="Lista de Cursos"/>
            </ListItem>
        </List>
        <Divider/>
        <List>
            <ListItem component={Link} button to='/instructor/nuevo'>
                <i className="material-icons">person_add</i>
                <ListItemText classes={{primary: classes.ListItemText}} primary="Nuevo Instructor"/>
            </ListItem>
            <ListItem component={Link} button to='/instructor/lista'>
                <i className="material-icons">people</i>
                <ListItemText classes={{primary: classes.ListItemText}} primary="Lista de Instructores"/>
            </ListItem>
        </List>
    </div>
);