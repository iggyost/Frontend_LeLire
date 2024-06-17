using System;
using System.Collections.Generic;

namespace Frontend_LeLire.ApplicationData;

public partial class LibraryView
{
    public int LibraryBooksId { get; set; }

    public int LibraryId { get; set; }

    public int BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string CoverImage { get; set; }
}
