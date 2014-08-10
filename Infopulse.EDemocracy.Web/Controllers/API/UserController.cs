using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Web.CORS;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	[CorsPolicyProvider]
	public class UserController : BaseApiController
    {
	    private readonly IParticipantRepository participantRepository;

	    public UserController()
	    {
		    this.participantRepository = new ParticipantRepository();
	    }

	    public UserController(IParticipantRepository repository)
	    {
		    this.participantRepository = repository;
	    }

        //[HttpGet]
        //public Data.Entities.Participant Get(string thumbPrint)
        //{
        //    try
        //    {
        //        var participant = participantRepository.GetParticipant(thumbPrint);
        //        return participant;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //}
    }
}