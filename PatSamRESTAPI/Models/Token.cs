namespace PatSamRESTAPI.Models
{
    //classe utilizada para obter o token da api servidor a fim de poder fazer as chamadas as actions dessa api servidor
    //com o token retornado da api server a esta api, essa api irá fazer um post/get com a uri específica
    //o token deverá ser colocado como cabeçalho da requisição http (bearer token)
    public class Token
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
    }
}
