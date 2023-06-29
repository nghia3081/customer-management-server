using AutoMapper;

namespace Repository.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<BusinessObject.Models.Contract, Repository.Entities.Contract>();
            this.CreateMap<BusinessObject.Models.ContractDetail, Repository.Entities.ContractDetail>();
            this.CreateMap<BusinessObject.Models.Customer, Repository.Entities.Customer>();
            this.CreateMap<BusinessObject.Models.Menu, Repository.Entities.Menu>();
            this.CreateMap<BusinessObject.Models.Package, Repository.Entities.Package>();
            this.CreateMap<BusinessObject.Models.StatusCategory, Repository.Entities.StatusCategory>();
            this.CreateMap<BusinessObject.Models.TaxCategory, Repository.Entities.TaxCategory>();
            this.CreateMap<BusinessObject.Models.User, Repository.Entities.User>();

            this.CreateMap<Repository.Entities.Contract, BusinessObject.Models.Contract>();
            this.CreateMap<Repository.Entities.ContractDetail, BusinessObject.Models.ContractDetail>();
            this.CreateMap<Repository.Entities.Customer, BusinessObject.Models.Customer>();
            this.CreateMap<Repository.Entities.Menu, BusinessObject.Models.Menu>();
            this.CreateMap<Repository.Entities.Package, BusinessObject.Models.Package>();
            this.CreateMap<Repository.Entities.StatusCategory, BusinessObject.Models.StatusCategory>();
            this.CreateMap<Repository.Entities.TaxCategory, BusinessObject.Models.TaxCategory>();
            this.CreateMap<Repository.Entities.User, BusinessObject.Models.User>();
        }
    }
}
