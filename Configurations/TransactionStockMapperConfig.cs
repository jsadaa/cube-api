using AutoMapper;

namespace ApiCube.Configurations;

public class TransactionStockMapperConfig : Profile
{
    public TransactionStockMapperConfig()
    {
        CreateMap<Domain.Entities.TransactionStock, Persistence.Models.TransactionStockModel>();
        CreateMap<Persistence.Models.TransactionStockModel, Domain.Entities.TransactionStock>();
    }
}