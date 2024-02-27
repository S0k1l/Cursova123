using Cursova.Models;
using Cursova.ViewModels;

namespace Cursova.Interfaces
{
    public interface IStatementsRepository
    {
        Task<IEnumerable<Statements>> GetOtherUserStatements(string id);
        Task<IEnumerable<Statements>> GetStatementsByUserIdAsync(string id);
        Task<IEnumerable<Statements>> GetAllUsersStatementaAsync();
        Task<AllStatementInfoViewModel> GetAllStatementInfoAsync(int id);
        Task<AppUser> GetUserById(string id);
        bool UpdateStatus(AllStatementInfoViewModel model);
        bool Add(Statements statements);
        bool Save();
    }
}
