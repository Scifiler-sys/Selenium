using Ganss.Excel;

public class Model
{
    [Column("Github username")]
    public string Username { get; set; }
}

public class Excel
{
    private readonly ExcelMapper _mapper;
    public Excel()
    {
        _mapper = new ExcelMapper("./Data/Welcome Survey!.xlsx");
    }

    public List<string> GetUsers 
    { 
        get
        {
            var models = _mapper.Fetch<Model>();
            List<string> users = new List<string>();
            foreach (var item in models)
            {
                users.Add(item.Username);
            }

            return users;
        } 
    }
}