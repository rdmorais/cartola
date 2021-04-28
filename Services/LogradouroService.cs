using System;
using System.Collections.Generic;
using System.Linq;
using ApiCliente.Domain.Models;
using ApiCliente.Persistence.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCliente.Services
{
    public class LogradouroService
    {
        private AppDbContext _context = new AppDbContext();
        
        /// <summary>
        /// Recupera todos os logradouros
        /// </summary>
         public List<Logradouro> FindAll (){
            var logradouros = _context.Logradouros.FromSqlRaw("EXEC GET_LOGRADOUROS null, null").AsEnumerable().ToList();
            return logradouros;
        }

        /// <summary>
        /// Recupera o logradouro com base no Id informado
        /// </summary>
        public Logradouro FindById (int id){
            var logradouro = _context.Logradouros.FromSqlRaw($"EXEC GET_LOGRADOUROS {id}, null").AsEnumerable().FirstOrDefault();
            return logradouro;
        }

        /// <summary>
        /// Busca os logradouros por Cliente
        /// </summary>
        public List<Logradouro> FindByClienteId (int clienteId){
            var logradouros = _context.Logradouros.FromSqlRaw($"EXEC GET_LOGRADOUROS null, {clienteId}").AsEnumerable().ToList();
            return logradouros;
        }

        /// <summary>
        /// Insere um logradouro
        /// </summary>
        public HttpObject Insert (Logradouro logradouro){
            var HttpObject = new HttpObject();

            try 
            {
                var logradouroIncluido = _context.Logradouros.FromSqlRaw($"EXEC INSERT_LOGRADOURO @cliente,@endereco",
                            new SqlParameter("@cliente", logradouro.ClienteId),
                            new SqlParameter("@endereco", logradouro.Endereco)
                )
                .AsEnumerable()
                .FirstOrDefault();

                if(logradouroIncluido != null){
                    HttpObject.Sucesso = true;
                    var idIncluido = logradouroIncluido.Id.ToString();
                    HttpObject.Mensagem = $"Logradouro incluído com sucesso! Id: {idIncluido}";
                }else{
                    HttpObject.Sucesso = false;
                    HttpObject.Mensagem = $"Ocorreu um erro ao incluír o Logradouro, rever parâmetros.";
                }

            }
            catch(Exception e)
            {
                HttpObject.Sucesso = false;
                HttpObject.Mensagem = e.Message;
            }

            return HttpObject;
        }

        /// <summary>
        /// Altera um logradouro
        /// </summary>
         public HttpObject Update (Logradouro logradouro, int id){
            var HttpObject = new HttpObject();

            try{

                var logradouroAlterado = _context.Logradouros.FromSqlRaw($"EXEC UPDATE_LOGRADOURO @id,@cliente,@endereco",
                             new SqlParameter("@id", id),
                            new SqlParameter("@cliente", logradouro.ClienteId),
                            new SqlParameter("@endereco", logradouro.Endereco)
                )
                .AsEnumerable()
                .FirstOrDefault();

                if(logradouroAlterado != null){
                    HttpObject.Sucesso = true;
                    var idAlterado = logradouroAlterado.Id.ToString();
                    HttpObject.Mensagem = $"Logradouro alterado com sucesso! Id: {idAlterado}";
                }else{
                    HttpObject.Sucesso = false;
                    HttpObject.Mensagem = $"Ocorreu um erro ao alterar o Logradouro, rever parâmetros.";
                }

            }catch(Exception e){
                HttpObject.Sucesso = false;
                HttpObject.Mensagem = e.Message;
            }

            return HttpObject;
        }

        /// <summary>
        /// Exclui um logradouro
        /// </summary>
        public HttpObject Delete (int id){
            var HttpObject = new HttpObject();

            try{

                var logradouroExcluido = _context.Logradouros.FromSqlRaw($"EXEC DELETE_LOGRADOURO @id",
                             new SqlParameter("@id", id)
                )
                .AsEnumerable()
                .FirstOrDefault();

                if(logradouroExcluido == null){
                    HttpObject.Sucesso = true;
                    HttpObject.Mensagem = $"Logradouro excluído com sucesso!";
                }else{
                    HttpObject.Sucesso = false;
                    HttpObject.Mensagem = $"Ocorreu um erro ao excluir o Logradouro, rever parâmetros.";
                }

            }catch(Exception e){
                HttpObject.Sucesso = false;
                HttpObject.Mensagem = e.Message;
            }

            return HttpObject;
        }
    }
}