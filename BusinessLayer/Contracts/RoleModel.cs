
namespace BusinessLayer.Contracts
{
    public class RoleModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public RoleTypeEnum RoleType { get; set; }
    }

    public enum RoleTypeEnum
    {
        PoliceOfficer = 0,
        PostOfficeEmployee = 1,
        Admin = 2,
        Driver = 3
    }
}
