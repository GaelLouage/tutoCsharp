internal class Program
{
    private static void Main(string[] args)
    {
       var video = new Video() { Title = "Video 1" };
       var videoEncoder = new VideoEncoder();
       var mailservice = new MailService();
       var messageService = new MessageService();   
       videoEncoder.VideoEncoded += mailservice.OnVideoEncoded;
       videoEncoder.VideoEncoded += messageService.OnVideoEncoded;
       videoEncoder.Encode(video);
    }
    public class MessageService
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("Message: Sending an Message..." + e.Video.Title);
        }
    }
    public class MailService
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("MailService: Sending an email..." + e.Video.Title);
        }
    }
    public class Video
    {
        public string Title { get; set; }
    }
    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }
    public class VideoEncoder
    {
        public event EventHandler<VideoEventArgs> VideoEncoded; 
        public void Encode(Video video)
        {
            Console.WriteLine("Encoding video...");
            Thread.Sleep(3000);
            OnVideoEncoded(video);
         
        }
        protected virtual void OnVideoEncoded(Video video)
        {
            if(VideoEncoded is not null)
            {
                VideoEncoded(this, new VideoEventArgs() { Video = video});
            }
        }
    }
}