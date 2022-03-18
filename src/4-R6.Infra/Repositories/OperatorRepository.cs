using R6.Domain.Entities;
using R6.Infra.Context;
using R6.Infra.Interfaces;

namespace R6.Infra.Repositories
{
    public class OperatorRepository : BaseRepository<Operator>, IOperatorRepository{
        private readonly R6Context _context;

        public OperatorRepository(R6Context context) : base(context)
        {
            _context = context;
        }
    }
}