using System.Collections.Generic;

namespace POD_Chat.Base
{
    public static class PodParameterName
    {
        public static Dictionary<string, string> ParametersName = new Dictionary<string, string>
        {
            {"Token", "_token_"},
            {"ApiToken", "token"},
            {"TokenIssuer", "_token_issuer_"},
            {"ScVoucherHash", "scVoucherHash"},
            {"ScApiKey", "scApiKey"},
            {"ClientId", "client_id"},
            {"ClientSecret", "client_secret"},
            {"Ott", "_ott_"},
            {"FirstName", "firstName"},
            {"LastName", "lastName"},
            {"CellphoneNumber", "cellphoneNumber"},
            {"Email", "email"},
            {"UniqueId", "uniqueId"},
            {"Username", "username"},
            {"TypeCode", "typeCode"},
            {"Query", "q"},
            {"Id", "id"},
            {"Offset", "offset"},
            {"Size", "size"},
            {"OwnerId", "ownerId"},
        };
    }
}
