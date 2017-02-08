
public class TunneStruct : FeelingInterface
{


    public string feeling { get; set; }
    public int id { get; set; }
    public FeelingType type { get; set; }



    public TunneStruct()
    {

    }
    public string GetFeeling()
    {
        return feeling;
    }

    public int GetID()
    {
        return id;
    }

    FeelingType FeelingInterface.GetType()
    {
        return type;
    }

   

}