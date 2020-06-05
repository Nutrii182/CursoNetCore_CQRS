using System;

namespace Persistencia.DapperConnection.Instructor
{
    public class InstructorModel
    {
        public Guid InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Titulo { get; set; }
    }
}