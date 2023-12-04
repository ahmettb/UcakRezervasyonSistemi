namespace RezervasyonUcak.Models.Token
{
    public interface ITokenHandler
    {

        Dto.Token CreateAccessTokenAsync(Login loginRequest);
        string getUsernameFromToken(string token);

    }
}
