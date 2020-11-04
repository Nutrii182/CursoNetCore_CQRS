import HttpClient from "../services/HttpClient";

export const guardarCurso = async (curso, imagen) => {
  const epCurso = "/cursos";
  const promesaCurso = HttpClient.post(epCurso, curso);

  if (imagen) {
    const epImagen = "/documento";
    const promesaImagen = HttpClient.post(epImagen, imagen);
    return await Promise.all([promesaCurso, promesaImagen]);
  } else {
    return await Promise.all([promesaCurso]);
  }
};

export const paginacionCurso = (paginador) => {
  return new Promise((resolve, eject) => {
    HttpClient.post("/cursos/report", paginador).then((response) => {
      resolve(response);
    });
  });
};
