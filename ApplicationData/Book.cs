using System;
using System.Collections.Generic;

namespace Frontend_LeLire.ApplicationData;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; }

    public decimal Cost { get; set; }

    public int PageCount { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public string Author { get; set; } 

    public string CoverImage { get; set; }

    public int GenreId { get; set; }

}
