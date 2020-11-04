import HttpClient from "../services/HttpClient";

export const guardarCurso = async (curso, imagen) => {
    const epCurso = '/cursos';
    const epImagen = '/documento';

    const promesaCurso = HttpClient.post(epCurso, curso);
    const promesaImagen = HttpClient.post(epImagen, imagen);

    const resp = await Promise.all([promesaCurso, promesaImagen]);
    return resp;
}