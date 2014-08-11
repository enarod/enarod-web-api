namespace Infopulse.EDemocracy.Model
{
    public class AgreementVoteResponse
    {
        public int ID;
        public string Response;

        public void SuccessVote()
        {
            this.ID = 0;
            this.Response = "Дякуємо! Ваш голос зараховано!";
        }

        public void ParticipantHasAlreadyVoted()
        {
            this.ID = 1;
            this.Response = "Ви вже проголосували за цю угоду.";
        }
    }
}