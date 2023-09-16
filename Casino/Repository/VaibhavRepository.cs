using Casino.Data;
using Casino.IRepository;
using Casino.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino.Repository
{
    public class VaibhavRepository : IVaibhavRepository
    {
        private readonly RegisterDbContext _registerDbContext;

        public VaibhavRepository(RegisterDbContext registerDbContext)
        {
            _registerDbContext = registerDbContext;
        }

        public List<Register> MyRegister()
        {
            return _registerDbContext.Registers.ToList();
        }

    }
}
