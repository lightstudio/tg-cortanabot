namespace CortanaBot.Models
{
    public class RpcResult
    {
        public int Status { get; set; }
        public string Content { get; set; }

        public RpcResult(int status, string content)
        {
            Status = status;
            Content = content;
        }
    }
}