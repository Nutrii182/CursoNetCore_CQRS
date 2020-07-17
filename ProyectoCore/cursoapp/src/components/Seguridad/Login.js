import React, { useState } from 'react';
import style from '../shared/Style';
import { Container, Avatar, Typography, TextField, Button } from '@material-ui/core';
import LockIcon from '@material-ui/icons/Lock';
import { loginUsuario } from '../../actions/UsuarioAction';

const Login = () => {

    const [usuario, setUsuario] = useState({
        Email: '',
        Password : '' 
     });

     const ingresarValores = (e) => {
        const {name, value} = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name] : value
        }));
    };
    
    const iniciarSesion = (e) => {
        e.preventDefault();
        loginUsuario(usuario).then(response => {
            console.log('Sesion iniciada');
            console.log(response);
            window.localStorage.setItem('token', response.data.token);
        });
    }

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
                    <TextField name="Email" value={usuario.Email} onChange={ingresarValores} variant="outlined" fullWidth label="Ingrese su email" margin="normal"/>
                    <TextField name="Password" value={usuario.Password} onChange={ingresarValores} type="password" variant="outlined" fullWidth label="Ingrese su password" margin="normal"/>
                    <Button type="submit" onClick={iniciarSesion} fullWidth variant="contained" color="primary" style={style.submit}>
                        Iniciar Sesión
                    </Button>
                </form>
            </div>
        </Container>
    );
};

export default Login;