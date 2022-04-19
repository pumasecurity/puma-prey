namespace Gopher.Models;

public class UserErrorData
{
    public DateTime Date { get; set; }

    public int UserID { get; set; }

    public int ErrorID { get; set; }

    public string? Message { get; set; }
}

