import React, { useState } from "react";
import {
  Button,
  Container,
  Grid,
  TextField,
  Typography
} from "@material-ui/core";
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider
} from "@material-ui/pickers";
import DateFnsUtils from "@date-io/date-fns";
import style from "../shared/Style";
import ImageUploader from "react-images-upload";
import { v4 as uuidv4 } from "uuid";
import { getDataImage } from "../../actions/ImagenAction";
import { guardarCurso } from "../../actions/CursoAction";

const NuevoCurso = () => {
  const [fechaSelect, setFechaSelect] = useState(new Date());
  const [imagenCurso, setImagenCurso] = useState(null);
  const [curso, setCurso] = useState({
    titulo: "",
    descripcion: "",
    precio: 0.0,
    promocion: 0.0
  });

  const ingresarValores = e => {
    const { name, value } = e.target;

    setCurso(ant => ({
      ...ant,
      [name]: value
    }));
  };

  const subirFoto = imagenes => {
    const foto = imagenes[0];

    getDataImage(foto).then((resp) => {
      setImagenCurso(resp);
    });
  };

  const saveCurso = e => {
    e.preventDefault();
    const cursoId = uuidv4();

    const objCurso = {
      titulo : curso.titulo,
      descripcion : curso.descripcion,
      precio : parseFloat(curso.precio || 0.0),
      promocion : parseFloat(curso.promocion || 0.0),
      fechaPublicacion : fechaSelect,
      cursoId : cursoId
    };

    const objImagen = {
      nombre : imagenCurso.nombre,
      data : imagenCurso.data,
      extension : imagenCurso.extension,
      objetoReferencia : cursoId
    };

    guardarCurso(objCurso, imagenCurso).then(resp => {
      console.log('respuesta', resp);
    });
  }

  const fotoKey = uuidv4();

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
                name="titulo"
                variant="outlined"
                fullWidth
                label="Ingrese Título"
                value={curso.titulo}
                onChange={ingresarValores}
              />
            </Grid>
            <Grid item xs={12} md={12}>
              <TextField
                name="descripcion"
                variant="outlined"
                fullWidth
                label="Ingrese Descripción"
                value={curso.descripcion}
                onChange={ingresarValores}
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="precio"
                variant="outlined"
                fullWidth
                label="Precio del Curso"
                value={curso.precio}
                onChange={ingresarValores}
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <TextField
                name="promocion"
                variant="outlined"
                fullWidth
                label="Precio de Promoción"
                value={curso.promocion}
                onChange={ingresarValores}
              />
            </Grid>
            <Grid item xs={12} md={6}>
              <MuiPickersUtilsProvider utils={DateFnsUtils}>
                <KeyboardDatePicker
                  value={fechaSelect}
                  onChange={setFechaSelect}
                  margin="normal"
                  id="fecha-publicion-Id"
                  label="Seleccione Fecha de Publicación"
                  format="dd/MM/yyyy"
                  fullWidth
                  KeyboardButtonProps={{
                    "aria-label": "change date"
                  }}
                />
              </MuiPickersUtilsProvider>
            </Grid>
            <Grid item xs={12} md={6}>
              <ImageUploader
                withIcon={false}
                key={fotoKey}
                singleImage={true}
                buttonText="Imagen del Curso"
                onChange={subirFoto}
                imgExtension={[".jpg", ".gif", ".png", ".jpeg"]}
                maxFileSize={5242880}
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
                onClick = {saveCurso}
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
