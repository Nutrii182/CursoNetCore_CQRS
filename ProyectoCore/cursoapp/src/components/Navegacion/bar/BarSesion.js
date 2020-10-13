import React, { useState } from 'react';
import { Toolbar, IconButton, Typography, makeStyles, Button, Avatar, Drawer} from '@material-ui/core';
import FotoUsuario from '../../../logo.svg';
import { useStateValue } from '../../../context/store';
import { DrawerLeft } from './DrawerLeft';
import { withRouter } from 'react-router-dom';
import { DrawerRight } from './DrawerRight';

const useStyles = makeStyles((theme) => ({
    sectionDesktop : {
        display : "none",
        [theme.breakpoints.up("md")] : {
            display : "flex"
        }
    },
    sectionMobile : {
        display : "flex",
        [theme.breakpoints.up("md")] : {
            display : "none"
        }
    },
    grow : {
        flexGrow : 1
    },
    avatarSize : {
        width : 40,
        height : 40
    },
    list : {
        width : 250
    },
    listItemText : {
        fontSize : "14px",
        fontWeight : 600,
        paddingLeft : "15px",
        color : "#212121"
    }
}));

const BarSesion = (props) => {

    const classes = useStyles();
    const [{sesionUsuario}] = useStateValue();
    const [openDrawerLeft, setOpenDrawerLeft] = useState(false);
    const [openDrawerRight, setOpenDrawerRight] = useState(false);
    
    const closeDrawerLeft = () => {
        setOpenDrawerLeft(false);
    }

    const openDrawerLeftAction = () => {
        setOpenDrawerLeft(true);
    }

    const closeDrawerRight = () => {
        setOpenDrawerRight(false);
    }

    const openDrawerRightAction = () => {
        setOpenDrawerRight(true);
    }

    const salirSesionApp = () => {
        localStorage.removeItem('token');
        props.history.push('/auth/login');
    }

    return (
        <React.Fragment>
            <Drawer open = {openDrawerLeft} onClose = {closeDrawerLeft} anchor = "left">
                <div className = {classes.list} onKeyDown={closeDrawerLeft} onClick={closeDrawerLeft}>
                    <DrawerLeft classes={classes}/>
                </div>
            </Drawer>
            <Drawer open = {openDrawerRight} onClose = {closeDrawerRight} anchor = "right">
                <div role='button' onClick={closeDrawerRight} onKeyDown={closeDrawerRight}>
                    <DrawerRight classes={classes} salirSesion={salirSesionApp} usuario = {sesionUsuario ? sesionUsuario.usuario : null}/>
                </div>
            </Drawer>
            <Toolbar>
                <IconButton color="inherit" onClick={openDrawerLeftAction}>
                    <i className="material-icons">menu</i>
                </IconButton>
                <Typography variant="h6">Cursos Online</Typography>
                <div className={classes.grow}></div>
                <div className={classes.sectionDesktop}>
                    <Button color="inherit">Salir</Button>
                    <Button color="inherit">{sesionUsuario ? sesionUsuario.usuario.nombreCompleto : ""}</Button>
                    <Avatar src={FotoUsuario}/>
                </div>
                <div className={classes.sectionMobile}>
                    <IconButton color="inherit" onClick={openDrawerRightAction}>
                        <i className="material-icons">more_vert</i>
                    </IconButton>
                </div>
            </Toolbar>
        </React.Fragment>
    );
};

export default withRouter(BarSesion);