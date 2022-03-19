using System.Threading.Tasks;
using System.Collections.Generic;
using R6.Services.DTO;
using R6.Core.Structs;

namespace R6.Services.Interfaces{
    public interface IOperatorService{
        Task<Optional<OperatorDto>> CreateAsync(OperatorDto operatorDto);
        Task<Optional<IList<OperatorDto>>> GetAllAsync();
    }
}