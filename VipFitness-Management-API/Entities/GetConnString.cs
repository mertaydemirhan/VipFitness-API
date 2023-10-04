using Microsoft.Extensions.Configuration;

public class GetConnString
{
    private readonly IConfiguration _config;


    public GetConnString(IConfiguration config)
    {
        _config = config;
    }

    public string SendConnStr()
    {
        var str = _config["ConnectionString"];
        return str;
    }
}   