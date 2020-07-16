import React from 'react';
import style from '../shared/Style';
import { Container, Avatar, Typography, TextField, Button } from '@material-ui/core';
import LockIcon from '@material-ui/icons/Lock';

const Login = () => {
    return (
        <Container maxWidth="xs">
            <div style={style.paper}>
                <Avatar style={style.avatar}>
                    <LockIcon style={style.icon}/>
                </Avatar>
                <Typography component="h1" variant="h5">
                    Login de Usuario
                </Typography>
                <form style={style.form}>
                    <TextField name="username" variant="outlined" fullWidth label="Ingrese su username" margin="normal"/>
                    <TextField name="password" type="password" variant="outlined" fullWidth label="Ingrese su password" margin="normal"/>
                    <Button type="submit" fullWidth variant="contained" color="primary" style={style.submit}>
                        Iniciar Sesión
                    </Button>
                </form>
            </div>
        </Container>
    );
};

export default Login;