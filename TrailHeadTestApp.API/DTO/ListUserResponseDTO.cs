using System.Collections.Generic;
using TrailHeadTestApp.Interfaces.DTO;

namespace TrailHeadTestApp.API.DTO
{
    public class ListUserResponseDTO : IListUserResponseDTO
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<UserInformationResponseDTO> data { get; set; }
    }
}
