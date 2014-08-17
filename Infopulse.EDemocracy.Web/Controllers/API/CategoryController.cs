using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;
using System.Collections.Generic;
using System.Web.Http;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
    public class CategoryController : ApiController
    {
	    private readonly IEntityRepository entityRepository;

	    public CategoryController()
	    {
		    this.entityRepository = new EntityRepository();
	    }


	    public OperationResult<IEnumerable<Entity>> Get()
	    {
		    var cateroties = this.entityRepository.GetPetitionCategories();
		    return cateroties;
	    }
    }
}