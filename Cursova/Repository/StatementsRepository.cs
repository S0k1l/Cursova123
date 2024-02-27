using Cursova.Data;
using Cursova.Interfaces;
using Cursova.Models;
using Cursova.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Cursova.Repository
{
    public class StatementsRepository : IStatementsRepository
    {
        private readonly AppDbContext _context;

        public StatementsRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Statements statements)
        {
            _context.Add(statements);
            return Save();
        }

        public bool UpdateStatus(AllStatementInfoViewModel model)
        {
            var statements = _context.Statements.FirstOrDefault(s => s.Id == model.Id);
            statements.Status = model.Status;
            _context.Update(statements);
            return Save();
        }

        public async Task<AllStatementInfoViewModel> GetAllStatementInfoAsync(int id)
        {
            var query = await (from statement in _context.Statements
                         join user in _context.Users on statement.AppUserId equals user.Id
                         where statement.Id == id
                         select new
                         {
                             user.FirstName,
                             user.MiddleName,
                             user.LastName,
                             statement.Type,
                             statement.MainInfo,
                             statement.MoreDetail,
                             statement.Status,
                             statement.DateTime,
                             statement.ImageUrl,
                         }).FirstOrDefaultAsync();

            var users = await _context.Users.ToListAsync();
            var model = new AllStatementInfoViewModel
            {
                UserFullName = $"{query.LastName} {query.FirstName} {query.MiddleName}",
                Type = query.Type,
                MainInfo = query.MainInfo,
                MoreDetail = query.MoreDetail,
                Status = query.Status,
                DateTime = query.DateTime,
                Id = id,
                ImageUrl = query.ImageUrl,
                AppUsers = users,
            };
            return model;
        }

        public async Task<IEnumerable<Statements>> GetAllUsersStatementaAsync()
        {
            var statements = await _context.Statements.ToListAsync();
            return statements;
        }

        public async Task<IEnumerable<Statements>> GetStatementsByUserIdAsync(string id)
        {
            var statements = await _context.Statements.Where(s => s.AppUserId == id).ToListAsync();
            return statements;
        }
        public async Task<IEnumerable<Statements>> GetOtherUserStatements(string id)
        {
            var statements = await _context.Statements.Where(s => s.AppUserId != id).ToListAsync();
            return statements;
        }

        public async Task<AppUser> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
