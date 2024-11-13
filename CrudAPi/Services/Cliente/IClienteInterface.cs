using CrudAPi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudAPi.Services.Cliente
{
    public interface IClienteInterface
    {
        Task<ResponseModel<List<ClienteModel>>> ListarClientes();
        Task<ResponseModel<ClienteModel>> BuscarClientePorId(int idCliente);
        Task<ResponseModel<ClienteModel>> CriarCliente(ClienteModel cliente);
        Task<ResponseModel<ClienteModel>> AtualizarCliente(int idCliente, ClienteModel cliente);
        Task<ResponseModel<bool>> DeletarCliente(int idCliente);
    }
}
