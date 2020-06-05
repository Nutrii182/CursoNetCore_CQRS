using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConnection.Instructor
{
    public class InstructorRepository : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;
        public InstructorRepository(IFactoryConnection factoryConnection){
            _factoryConnection = factoryConnection;
        }
        public Task<int> DeleteInstruc(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InstructorModel> GetForId(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> GetList()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var storeProcedure = "usp_Obtener_Instructores";
            try{
                var connection = _factoryConnection.GetConnection();
                instructorList = await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType : CommandType.StoredProcedure);
            }catch(Exception e){
                throw new Exception("Error en la consulta", e);
            }finally{
                _factoryConnection.CloseConnection();
            }
            return instructorList;
        }

        public async Task<int> NewInstruc(string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "usp_instructor_nuevo";
            try{
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(
                storeProcedure,
                new {
                    InstructorId = Guid.NewGuid(),
                    Nombre = nombre,
                    Apellidos = apellidos,
                    Titulo = titulo
                },
                commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return result;
            }catch(Exception e){
                throw new Exception("Error creando instructor", e);
            }
        }

        public Task<int> UpdateInstruc(InstructorModel parametros)
        {
            throw new NotImplementedException();
        }
    }
}