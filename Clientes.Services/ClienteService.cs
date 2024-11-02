using Microsoft.EntityFrameworkCore;
using Clientes.Data.Context;
using Clientes.Abstraccion;
using Clientes.Domain;
using System.Linq.Expressions;
using Clientes.Domain.DTO;
using Clientes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Services
{
    public class ClienteService(IDbContextFactory<ClientesContext> DbFactory) : IClientesService
    {

        private async Task<bool> Existe(int prioridadId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Clientes.AnyAsync(e => e.ClienteId == prioridadId);
        }

        private async Task<bool> Insertar(ClientesDTO clientesDTO)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            var client = new Cliente()
            {
                ClienteId = clientesDTO.ClienteID,
                NombreCliente = clientesDTO.NombreCliente,
                NumeroWhatsapp = clientesDTO.NumeroDeWhatsapp,
                


            };
            context.Clientes.Add(client);
            var guardar = await context.SaveChangesAsync() > 0;
            clientesDTO.ClienteID = client.ClienteId;
            return guardar;
        }

        private async Task<bool> Modificar(ClientesDTO clientesDTO)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            var client = new Cliente()
            {
                ClienteId = clientesDTO.ClienteID,
                NombreCliente = clientesDTO.NombreCliente,
                NumeroWhatsapp = clientesDTO.NumeroDeWhatsapp,
             
               



            };
            context.Update(client);
            var modificado = await context.SaveChangesAsync() > 0;
            return modificado;
        }

        public async Task<bool> Guardar(ClientesDTO client)
        {
            if (!await Existe(client.ClienteID))
            {
                return await Insertar(client);
            }
            else
            {
                return await Modificar(client);
            }
        }

        public async Task<bool> Eliminar(int clientId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Clientes
                .Where(e => e.ClienteId == clientId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<ClientesDTO> Buscar(int id)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            var client = await context.Clientes
                .Where(e => e.ClienteId == id)
                .Select(c => new ClientesDTO()
                {
                    ClienteID = c.ClienteId,
                    NombreCliente = c.NombreCliente,
                    NumeroDeWhatsapp = c.NumeroWhatsapp,
                   
                   

                }).FirstOrDefaultAsync();

            return client ?? new ClientesDTO();
        }

        public async Task<List<ClientesDTO>> Listar(Expression<Func<ClientesDTO, bool>> criterio)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Clientes
                .Select(c => new ClientesDTO()
                {
                    ClienteID = c.ClienteId,
                    NombreCliente = c.NombreCliente,
                    NumeroDeWhatsapp =c.NumeroWhatsapp,
                   
                    
                })
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> ExistePrioridad(int id, string Nombre, string Whatsapp)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Clientes
                .AnyAsync(e => e.ClienteId != id
                && e.NombreCliente == Nombre
                || e.NumeroWhatsapp.ToLower().Equals(Whatsapp.ToLower()));
        }




    }
}
