using Core.Entity;
using Core.IRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ContatoRepository : IContato
    {
        private readonly AppDbContext _context;

        public ContatoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContatoModel>> GetContatos()
        {
            const string query = @"
                SELECT 
                    CTT_ID,
                    CTT_DTCRIACAO,
                    CTT_NOME,
                    CTT_EMAIL,
                    CTT_DDD,
                    CTT_NUMERO
                FROM CONTATO_CTT (NOLOCK)";

            using (var connection = _context.Database.GetDbConnection())
            {
                return await connection.QueryAsync<ContatoModel>(query);
            }
        }
    }
}
