import React, { useEffect, useState } from "react";
import { paginacionCurso } from "../../actions/CursoAction";
import {
  Grid,
  Hidden,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  TextField,
} from "@material-ui/core";
import ControlTyping from "../shared/ControlTyping";

const PaginadorCurso = () => {
  const [textSearchCourse, setTextSearchCourse] = useState("");
  const typingSearch = ControlTyping(textSearchCourse, 1000);

  const [pagiRequest, setPagiRequest] = useState({
    titulo: "",
    numeroPagina: 0,
    cantidadElementos: 5,
  });

  const [pagiResponse, setPagiResponse] = useState({
    listaRecords: [],
    totalRecords: 0,
    numeroPaginas: 0,
  });

  useEffect(() => {
    const getListaCurso = async () => {
      let titulo = "";
      let pagina = pagiRequest.numeroPagina + 1;

      if (typingSearch) {
        titulo = typingSearch;
        pagina = 1;
      }

      const objPagiRequest = {
        titulo: titulo,
        numeroPagina: pagina,
        cantidadElementos: pagiRequest.cantidadElementos,
      };

      const response = await paginacionCurso(objPagiRequest);
      setPagiResponse(response.data);
    };

    getListaCurso();
  }, [pagiRequest, typingSearch]);

  const changePage = (event, newPage) => {
    setPagiRequest((ant) => ({
      ...ant,
      numeroPagina: parseInt(newPage),
    }));
  };

  const changeRecords = (event) => {
    setPagiRequest((ant) => ({
      ...ant,
      cantidadElementos: parseInt(event.target.value),
      numeroPagina: 0,
    }));
  };

  return (
    <div style={{ padding: "10px", width: "100%" }}>
      <Grid container style={{ paddingTop: "20px", paddingBottom: "20px" }}>
        <Grid item xs={12} sm={4} md={6}>
          <TextField
            fullWidth
            name="textBusqueda"
            variant="outlined"
            label="Buscar Curso"
            onChange={(e) => setTextSearchCourse(e.target.value)}
          />
        </Grid>
      </Grid>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="left">Cursos</TableCell>
              <Hidden mdDown>
                <TableCell align="left">Descripción</TableCell>
                <TableCell align="left">Fecha de Publicación</TableCell>
                <TableCell align="left">Precio Original</TableCell>
                <TableCell align="left">Precio de Promoción</TableCell>
              </Hidden>
            </TableRow>
          </TableHead>
          <TableBody>
            {pagiResponse.listaRecords.map((curso) => (
              <TableRow key={curso.titulo}>
                <TableCell align="left">{curso.Titulo}</TableCell>
                <Hidden mdDown>
                  <TableCell align="left">{curso.Descripcion}</TableCell>
                  <TableCell align="left">
                    {new Date(curso.FechaPublicacion).toLocaleString()}
                  </TableCell>
                  <TableCell align="left">{curso.PrecioActual}</TableCell>
                  <TableCell align="left">{curso.Promocion}</TableCell>
                </Hidden>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        component="div"
        rowsPerPageOptions={[5, 10, 25]}
        count={pagiResponse.totalRecords}
        rowsPerPage={pagiRequest.cantidadElementos}
        page={pagiRequest.numeroPagina}
        onChangePage={changePage}
        onChangeRowsPerPage={changeRecords}
        labelRowsPerPage="Cursos por Página"
      />
    </div>
  );
};

export default PaginadorCurso;
