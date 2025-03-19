namespace TrainManagement.Common.Models.Settings
{
    public class AppSettings
    {
        public JWTOptions JWTOptions { get; set; }
        public AIOptions AIOptions { get; set; }
        public VoiceOptions VoiceOptions { get; set; }
        public TwilioOptions TwilioOptions { get; set; }      
    }
}
