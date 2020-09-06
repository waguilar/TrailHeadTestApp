using TrailHeadTestApp.Interfaces.DTO;

namespace TrailHeadTestApp.API.DTO
{
    public class UserInformationResponseDTO : IUserInformationResponseDTO
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }
}
