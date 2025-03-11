namespace VoiceOtp.Models
{
    public class ResponseData
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public ResponseData(int Code, string Message, object? Data)
        {
            this.Code = Code;
            this.Message = Message;
            this.Data = Data;
        }
    }
}
