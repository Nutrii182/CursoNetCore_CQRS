using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConnection.Instructor
{
    public interface IInstructor
    {
         Task<IEnumerable<InstructorModel>> GetList();
         Task<InstructorModel> GetForId(Guid Id);
         Task<int> NewInstruc(string nombre, string apellidos, string titulo);
         Task<int> UpdateInstruc(InstructorModel parametros);
         Task<int> DeleteInstruc(Guid id);
    }
}