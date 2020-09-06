using System;
using System.Collections.Generic;
using System.Text;

namespace TrailHeadTestApp.Interfaces.Models
{
    public interface IEmployee
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Avatar { get; set; }
    }
}
