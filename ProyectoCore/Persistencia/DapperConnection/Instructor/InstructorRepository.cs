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
        public async Task<int> DeleteInstruc(Guid id)
        {
            var storeProcedure = "usp_instructor_eliminar";
            try{
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(
                    storeProcedure,
                    new {
                        InstructorId = id
                    },
                    commandType : CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return result;
            }catch(Exception e){
                throw new Exception("No se pudo eliminar el instructor", e);
            }
        }

        public async Task<InstructorModel> GetForId(Guid Id)
        {
            InstructorModel instructor = null;
            var storeProcedure = "usp_instructor_id";
            try{
                var connection = _factoryConnection.GetConnection();
                instructor = await connection.QueryFirstAsync<InstructorModel>(
                    storeProcedure,
                    new {
                        InstructorId = Id
                    },
                    commandType : CommandType.StoredProcedure
                );
            }catch(Exception e){
                throw new Exception("No se encontro el instructor", e);
            }finally{
                _factoryConnection.CloseConnection();
            }
            return instructor;
        }

        public async Task<IEnumerable<InstructorModel>> GetList()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var storeProcedure = "usp_Obtener_Instructores";
            try{
                var connection = _factoryConnection.GetConnection();
                instructorList = await connection.QueryAsync<InstructorModel>(
                    storeProcedure,
                    null,
                    commandType : CommandType.StoredProcedure
                );
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
                    Titulo = titulo,
                },
                commandType: CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return result;
            }catch(Exception e){
                throw new Exception("Error creando instructor", e);
            }
        }

        public async Task<int> UpdateInstruc(Guid instructorId, string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "usp_instructor_editar";
            try{
                var connection = _factoryConnection.GetConnection();
                var result = await connection.ExecuteAsync(
                    storeProcedure,
                    new {
                        InstructorId = instructorId,
                        Nombre = nombre,
                        Apellidos = apellidos,
                        Titulo = titulo
                    },
                    commandType : CommandType.StoredProcedure
                );
                _factoryConnection.CloseConnection();
                return result;
            }catch(Exception e){
                throw new Exception("No se pudo actualizar el Instructor", e);
            }
        }
    }
}