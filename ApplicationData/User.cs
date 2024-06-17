using System;
using System.Collections.Generic;

namespace Frontend_LeLire.ApplicationData;

public partial class User
{
    public int UserId { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }

    public string? Name { get; set; }

    public int StatusId { get; set; }

    public string? CoverPhoto { get; set; }
}
