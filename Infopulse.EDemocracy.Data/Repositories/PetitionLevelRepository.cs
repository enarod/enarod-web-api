﻿using System;
using System.Linq;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.Common;
using System.Collections.Generic;
using PetitionLevel = Infopulse.EDemocracy.Model.BusinessEntities.PetitionLevel;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionLevelRepository : IPetitionLevelRepository
	{
		public OperationResult<IEnumerable<PetitionLevel>> GetPetitionLevels()
		{
			OperationResult<IEnumerable<PetitionLevel>> result;

			try
			{
				using (var db = new EDEntities())
				{
					var levels = db.PetitionLevels.ToList();
					result = OperationResult<IEnumerable<PetitionLevel>>.Success(levels.Select(l => new PetitionLevel(l)));
				}
			}
			catch (Exception exc)
			{
				result = OperationResult<IEnumerable<PetitionLevel>>.ExceptionResult(exc);
			}

			return result;
		}
	}
}