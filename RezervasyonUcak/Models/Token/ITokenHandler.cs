namespace RezervasyonUcak.Models.Token
{
    public interface ITokenHandler
    {

        Dto.Token CreateAccessToken(Login loginRequest);
        string getUsernameFromToken(string token);

    }
}
