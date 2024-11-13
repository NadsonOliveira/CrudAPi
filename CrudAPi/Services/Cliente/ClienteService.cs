using Microsoft.EntityFrameworkCore;
using CrudAPi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrudAPi.Data;

namespace CrudAPi.Services.Cliente
{
    public class ClienteService : IClienteInterface
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<ClienteModel>>> ListarClientes()
        {
            var resposta = new ResponseModel<List<ClienteModel>>();
            try
            {
                var clientes = await _context.Cliente.ToListAsync();

                if (clientes == null || clientes.Count == 0)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Nenhum cliente encontrado.";
                }
                else
                {
                    resposta.Status = true;
                    resposta.Mensagem = "Clientes encontrados com sucesso.";
                    resposta.Dados = clientes;
                }
            }
            catch (Exception ex)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Erro: {ex.Message}";
            }
            return resposta;
        }

        public async Task<ResponseModel<ClienteModel>> BuscarClientePorId(int idCliente)
        {
            var resposta = new ResponseModel<ClienteModel>();
            try
            {
                var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == idCliente);

                if (cliente == null)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Cliente não encontrado.";
                }
                else
                {
                    resposta.Status = true;
                    resposta.Mensagem = "Cliente encontrado com sucesso.";
                    resposta.Dados = cliente;
                }
            }
            catch (Exception ex)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Erro: {ex.Message}";
            }
            return resposta;
        }

        public async Task<ResponseModel<ClienteModel>> CriarCliente(ClienteModel cliente)
        {
            var resposta = new ResponseModel<ClienteModel>();
            try
            {
                if (cliente == null)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Dados inválidos para cadastro.";
                    return resposta;
                }
                var emailExistente = await _context.Cliente.AnyAsync(c => c.Email == cliente.Email);

                if (emailExistente)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Este Email já existe, utilize outro Email!";
                }

                _context.Cliente.Add(cliente);
                await _context.SaveChangesAsync();

                resposta.Status = true;
                resposta.Mensagem = "Cliente criado com sucesso.";
                resposta.Dados = cliente;
            }
            catch (Exception ex)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Erro: {ex.Message}";
            }
            return resposta;
        }

        public async Task<ResponseModel<ClienteModel>> AtualizarCliente(int idCliente, ClienteModel cliente)
        {
            var resposta = new ResponseModel<ClienteModel>();
            try
            {
                if (cliente == null || idCliente <= 0)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Dados inválidos para atualização.";
                    return resposta;
                }

                var clienteExistente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == idCliente);

                if (clienteExistente == null)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Cliente não encontrado.";
                    return resposta;
                }

                clienteExistente.Name = cliente.Name;
                clienteExistente.Email = cliente.Email;

                _context.Cliente.Update(clienteExistente);
                await _context.SaveChangesAsync();

                resposta.Status = true;
                resposta.Mensagem = "Cliente atualizado com sucesso.";
                resposta.Dados = clienteExistente;
            }
            catch (Exception ex)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Erro: {ex.Message}";
            }
            return resposta;
        }

        public async Task<ResponseModel<bool>> DeletarCliente(int idCliente)
        {
            var resposta = new ResponseModel<bool>();
            try
            {
                if (idCliente <= 0)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "ID do cliente inválido.";
                    resposta.Dados = false;
                    return resposta;
                }

                var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == idCliente);

                if (cliente == null)
                {
                    resposta.Status = false;
                    resposta.Mensagem = "Cliente não encontrado.";
                    resposta.Dados = false;
                    return resposta;
                }

                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();

                resposta.Status = true;
                resposta.Mensagem = "Cliente deletado com sucesso.";
                resposta.Dados = true;
            }
            catch (Exception ex)
            {
                resposta.Status = false;
                resposta.Mensagem = $"Erro: {ex.Message}";
                resposta.Dados = false;
            }
            return resposta;
        }
    }
}
