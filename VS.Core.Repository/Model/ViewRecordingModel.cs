namespace VS.Core.Repository.Model
{
    public class ViewRecordingModel : BaseIndexModel
    {
        public string? UserId { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? LineCode { get; set; }

        public string? FileRecording { get; set; }

        public string? PhoneLog { get; set; }


    }
}
