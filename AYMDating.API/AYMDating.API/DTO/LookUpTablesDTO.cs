using AYMDating.DAL.Entities;

namespace AYMDating.API.DTO
{
    public class LookUpTablesDTO
    {
        public List<Country>? AllCountries { get; set; }
        public List<Gender>? AllGenders { get; set; }
        public List<Education>? AllEducations { get; set; }
        public List<MaritalStatus>? AllMaritalStatuses { get; set; }
        public List<Purpose>? AllPurposes { get; set; }
        public List<FinancialMode>? AllFinancialModes { get; set; }
        public List<Language>? AllLanguages { get; set; }
        public List<Job>? AllJobs { get; set; }

    }
}
