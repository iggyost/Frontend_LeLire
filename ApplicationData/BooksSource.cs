using System;
using System.Collections.Generic;

namespace Frontend_LeLire.ApplicationData;

public partial class BooksSource
{
    public int BookSourceId { get; set; }

    public int BookId { get; set; }

    public string Source { get; set; }
}
