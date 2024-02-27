using Cursova.Models;

namespace Cursova.ViewModels
{
    public class MyStatemantsViewModel
    {
        public IEnumerable<Statements> MyStatements { get; set; }
        public IEnumerable<Statements> NotMyStatements { get; set; }

    }
}
