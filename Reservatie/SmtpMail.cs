namespace EASendMail
{
    internal class SmtpMail
    {
        private string v;

        public SmtpMail(string v)
        {
            this.v = v;
        }

        public string Subject { get; internal set; }
    }
}