using Clientes.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Clientes.Abstraccion
{
    public  interface IClientesService
    {
        Task<bool> Guardar(ClientesDTO clientes);
        Task<bool> Eliminar(int clienteId);
        Task<ClientesDTO> Buscar(int id);
        Task<List<ClientesDTO>> Listar(Expression<Func<ClientesDTO, bool>> criterio);

        Task<bool> ClienteExiste(int id, string nombres);


    }
}
