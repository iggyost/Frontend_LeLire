using System;
using System.Collections.Generic;

namespace Frontend_LeLire.ApplicationData;

public partial class ReaderDatum
{
    public int ReaderDataId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public int CurrentPageNum { get; set; }

}
