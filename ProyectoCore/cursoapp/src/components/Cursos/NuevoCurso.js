import React from "react";
import {
  Button,
  Container,
  Grid,
  TextField,
  Typography,
} from "@material-ui/core";
import style from "../shared/Style";

const NuevoCurso = () => {
  return (
    <Container component="main" maxWidth="md" justify="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Nuevo Curso
        </Typography>
        <form style={style.form}>
          <Grid container spacing={2}>
            <Grid item xs={12} md={12}>
              <TextField
                name="Titulo"
                variant="outlined"
                fullWidth
                label="Ingrese Título"
              />
            </Grid>
            <Grid item xs={12} md={12}>
              <TextField
                name="Descripción"
                variant="outlined"
                fullWidth
                label="Ingrese Descripción"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="Precio"
                variant="outlined"
                fullWidth
                label="Precio del Curso"
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="Promocion"
                variant="outlined"
                fullWidth
                label="Precio de Promoción"
              />
            </Grid>
          </Grid>
          <Grid container justify="center">
            <Grid item xs={12} md={6}>
              <Button
                type="submit"
                fullWidth
                variant="contained"
                color="primary"
                style={style.submit}
              >
                Guardar Curso
              </Button>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
};

export default NuevoCurso;
