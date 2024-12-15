using GraphQL.Types;

public class RootQuery : ObjectGraphType
{
    public RootQuery(CryptoPriceQuery cryptoPriceQuery, UserScoreDataQuery userScoreDataQuery)
    {
        AddField(cryptoPriceQuery.FieldType);
        AddField(userScoreDataQuery.FieldType);
    }
}