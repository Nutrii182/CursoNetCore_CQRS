import React, { useState, useEffect } from 'react';
import style from '../shared/Style'
import { Container, Typography, Grid, TextField, Button, Avatar } from '@material-ui/core';
import { actualizarUsuario } from '../../actions/UsuarioAction';
import { useStateValue } from '../../context/store';
import reactFoto from '../../logo.svg';
import { v4 as uuidv4 } from 'uuid';
import ImageUploader from 'react-images-upload';
import { getDataImage } from '../../actions/ImagenAction';

const PerfilUsuario = () => {
    const [{ sesionUsuario }, dispatch] = useStateValue();
    const [usuario, setUsuario] = useState({
        nombreCompleto : '',
        email : '',
        username : '',
        password : '',
        confirmPassword : '',
        imagenPerfil : null,
        fotoUrl : ''
    });

    const ingresarValores = (e) => {
        const {name, value} = e.target;
        setUsuario(ant => ({
            ...ant,
            [name] : value
        }));
    };

    useEffect(() => {
        setUsuario(sesionUsuario.usuario);
        setUsuario(ant => ({
            ...ant,
            fotoUrl : sesionUsuario.usuario.imagenPerfil,
            imagenPerfil : null
        }));
    }, []);

    const modificaUsuario = (e) => {
        e.preventDefault();
        actualizarUsuario(usuario, dispatch).then(response => {
            console.log(response);
            if(response.status === 200){
                dispatch({
                    type : "OPEN_SNACKBAR",
                    openMessage : {
                        open : true,
                        message : "Usuario Modificado Exitosamente"
                    }
                });
                window.localStorage.setItem('token', response.data.token);
            } else{
                dispatch({
                    type : "OPEN_SNACKBAR",
                    openMessage : {
                        open : true,
                        message : "Error al guardar en : " + Object.keys(response.data.errors)
                    }
                });
            }
        });
    };

    const upPhoto = imagenes => {
        const foto = imagenes[0];
        const fotoUrl = URL.createObjectURL(foto);

        getDataImage(foto).then(resp => {
                console.log(resp);
                setUsuario((ant) => ({
                    ...ant,
                    imagenPerfil : resp, // json de action imagen
                    fotoUrl : fotoUrl // formato url
            }));           
        });
    };

    const fotoKey = uuidv4();

    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Avatar style={style.avatar} src={usuario.fotoUrl || reactFoto} />
                <Typography component="h1" variant="h5">
                    Perfil de Usuario
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={12}>
                            <TextField name="nombreCompleto" value={usuario.nombreCompleto} onChange={ingresarValores} variant="outlined" fullWidth label="Ingrese Nombre y Apellidos"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="email" value={usuario.email} onChange={ingresarValores} type="email" variant="outlined" fullWidth label="Ingrese Email"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="username" value={usuario.username} onChange={ingresarValores} variant="outlined" fullWidth label="Ingrese el Username"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="password" defaultValue={usuario.password} onChange={ingresarValores} type="password" variant="outlined" fullWidth label="Ingrese Password"/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name="confirmPassword" defaultValue={usuario.confirmPassword} onChange={ingresarValores} type="password" variant="outlined" fullWidth label="Confirme Password"/>
                        </Grid>
                        <Grid item xs={12} md={12}>
                            <ImageUploader
                                withIcon = {false} key = {fotoKey} singleImage = {true}
                                buttonText = "Seleccione una imagen de Perfil"
                                onChange = {upPhoto} imgExtension = {[".jpg", ".gif", ".png", ".jpeg"]}
                                maxFileSize = {5242880}
                            />
                        </Grid>
                    </Grid>
                    <Grid container justify="center">
                        <Grid item xs={12} md={6}>
                            <Button type="submit" onClick={modificaUsuario} fullWidth variant="contained" syze="large" color="primary" style={style.submit}>
                                Guardar Datos
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
};

export default PerfilUsuario;