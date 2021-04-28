using System;
using System.Collections.Generic;
using System.Linq;
using ApiCliente.Domain.Models;
using ApiCliente.Persistence.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCliente.Services
{
    public class ClienteService
    {

        private AppDbContext _context = new AppDbContext();
        private LogradouroService _logradouroService = new LogradouroService();

        /// <summary>
        /// Recupera todos os clientes
        /// </summary>
        public List<Cliente> FindAll (){
            var clientes = _context.Clientes.FromSqlRaw("EXEC GET_CLIENTES null, null, null").AsEnumerable().ToList();
            var logradouros = _logradouroService.FindAll();

            if(clientes != null && clientes.Count > 0){
                foreach(var cliente in clientes){
                    cliente.Logradouros = logradouros.Where(x => x.ClienteId == cliente.Id).ToList();
                }
            }

            return clientes;
        }

        /// <summary>
        /// Recupera o cliente com base no Id informado
        /// </summary>
        public Cliente FindById (int id){
            var cliente = _context.Clientes.FromSqlRaw($"EXEC GET_CLIENTES {id},null,null").AsEnumerable().FirstOrDefault();
            
            if(cliente != null)
                cliente.Logradouros = _logradouroService.FindByClienteId(id);

            return cliente;
        }

        /// <summary>
        /// Insere um cliente
        /// </summary>
        public HttpObject Insert (Cliente cliente){
            var HttpObject = new HttpObject();

            try 
            {
                var clienteIncluido = _context.Clientes.FromSqlRaw($"EXEC INSERT_CLIENTE @nome,@email,@logotipo",
                            new SqlParameter("@nome", cliente.Nome),
                            new SqlParameter("@email", cliente.Email),
                            new SqlParameter("@logotipo", cliente.Logotipo)
                )
                .AsEnumerable()
                .FirstOrDefault();

                if(clienteIncluido != null){
                    HttpObject.Sucesso = true;
                    var idIncluido = clienteIncluido.Id.ToString();
                    HttpObject.Mensagem = $"Cliente incluído com sucesso! Id: {idIncluido}";
                }else{
                    HttpObject.Sucesso = false;
                    HttpObject.Mensagem = $"Ocorreu um erro ao incluír o Cliente, rever parâmetros.";
                }

            }
            catch(Exception e)
            {
                var retorno = HandleException(e);
                HttpObject.Sucesso = false;
                HttpObject.Mensagem = retorno;
            }

            return HttpObject;
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        public HttpObject Update (Cliente cliente, int id){
            var HttpObject = new HttpObject();

            try{

                var clienteAlterado = _context.Clientes.FromSqlRaw($"EXEC UPDATE_CLIENTE @id,@nome,@email,@logotipo",
                             new SqlParameter("@id", id),
                            new SqlParameter("@nome", cliente.Nome),
                            new SqlParameter("@email", cliente.Email),
                            new SqlParameter("@logotipo", cliente.Logotipo)
                )
                .AsEnumerable()
                .FirstOrDefault();

                if(clienteAlterado != null){
                    HttpObject.Sucesso = true;
                    var idAlterado = clienteAlterado.Id.ToString();
                    HttpObject.Mensagem = $"Cliente alterado com sucesso! Id: {idAlterado}";
                }else{
                    HttpObject.Sucesso = false;
                    HttpObject.Mensagem = $"Ocorreu um erro ao alterar o Cliente, rever parâmetros.";
                }

            }catch(Exception e){
                var retorno = HandleException(e);
                HttpObject.Sucesso = false;
                HttpObject.Mensagem = retorno;
            }

            return HttpObject;
        }

        /// <summary>
        /// Exclui um cliente
        /// </summary>
        public HttpObject Delete (int id){
            var HttpObject = new HttpObject();

            try{

                var clienteExcluido = _context.Clientes.FromSqlRaw($"EXEC DELETE_CLIENTE @id",
                             new SqlParameter("@id", id)
                )
                .AsEnumerable()
                .FirstOrDefault();

                if(clienteExcluido == null){
                    HttpObject.Sucesso = true;
                    HttpObject.Mensagem = $"Cliente excluído com sucesso!";
                }else{
                    HttpObject.Sucesso = false;
                    HttpObject.Mensagem = $"Ocorreu um erro ao excluir o Cliente, rever parâmetros.";
                }

            }catch(Exception e){
                var retorno = HandleException(e);
                HttpObject.Sucesso = false;
                HttpObject.Mensagem = retorno;
            }

            return HttpObject;
        }

        /// <summary>
        /// Trata a exceção de acordo com o retorno.
        /// </summary>
        private string HandleException(Exception dbUpdateEx)
        {
            var retorno = dbUpdateEx.Message;

            if(retorno.Contains("IX_Cliente")){
                retorno = "Registro não alterado, e-mail já cadastrado.";
            }
        
            return retorno;
        }
        
    }
}