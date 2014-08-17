using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
    public class PetitionLevelController : ApiController
    {
	    private readonly IPetitionLevelRepository petitionLevelRepository;

	    public PetitionLevelController()
	    {
		    this.petitionLevelRepository = new PetitionLevelRepository();
	    }


	    public OperationResult<IEnumerable<PetitionLevel>> Get()
	    {
		    var result = this.petitionLevelRepository.GetPetitionLevels();
		    return result;
	    }
    }
}
