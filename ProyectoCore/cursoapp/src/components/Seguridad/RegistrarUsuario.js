import style from '../shared/Style';
import React, { useState } from 'react';
import { registrarUsuario } from '../../actions/UsuarioAction';
import { Container, Typography, Grid, TextField, Button } from '@material-ui/core';

const RegistrarUsuario = () => {

    const [usuario, setUsuario] = useState({
        NombreCompleto : '',
        Email : '',
        Password : '',
        Confirmpassword : '',
        Username : ''
    });

    const ingresarValoresMemoria = (e) => {
        const {name, value} = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name] : value
        }));
    };

    const crearUsuario = (e) => {
        e.preventDefault();
        registrarUsuario(usuario).then(response => {
            console.log("Usuario Registrado");
        });
    };

    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Typography component="h1" variant="h5">
                    Registro de Usuario
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={12}>
                            <TextField name="NombreCompleto" value={usuario.NombreCompleto} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su nombre completo"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Email" value={usuario.Email} onChange={ingresarValoresMemoria} type="email" variant="outlined" fullWidth label="Ingrese su email"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Username" value={usuario.Username} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su username"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Password" value={usuario.Password} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Ingrese su password"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="Confirmpassword" value={usuario.Confirmpassword} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Confirme password"/>
                        </Grid>
                    </Grid>
                    <Grid container justify="center">
                        <Grid item xs={12} md={6}>
                            <Button type="submit" onClick={crearUsuario} fullWidth variant="contained" color="primary" size="large" style={style.submit}>
                                Enviar
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default RegistrarUsuario;