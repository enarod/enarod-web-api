using System;
using System.Collections.Generic;
using System.Linq;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;
using Candidate = Infopulse.EDemocracy.Model.BusinessEntities.Candidate;
using PetitionLevel = Infopulse.EDemocracy.Model.BusinessEntities.PetitionLevel;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class RegionRepository : BaseRepository, IRegionRepository
	{
		public List<Region> FakeRegions = new List<Region>
		                                  {
			                                  new Region
			                                  {
				                                  ID = 1,
				                                  Name = "Україна",
												  Level = new PetitionLevel
												          {
													          ID = 1
												          },
				                                  Candidates = new List<Candidate>
				                                               {
					                                               new Candidate
					                                               {
						                                               ID = 1,
						                                               Status = EntityDictionary.Candidate.Status.Active,
						                                               FirstName = "Петро",
						                                               MiddleName = "Олексійович",
						                                               LastName = "Порошенко",
						                                               Type = EntityDictionary.Candidate.Type.Independent
					                                               },
																   new Candidate
					                                               {
						                                               ID = 5,
						                                               Status = EntityDictionary.Candidate.Status.Active,
						                                               FirstName = "Дмитро",
						                                               MiddleName = "Анатолійович",
						                                               LastName = "Ярош",
						                                               Type = EntityDictionary.Candidate.Type.Independent
					                                               }
				                                               },

			                                  },
											  new Region
											  {
												  ID = 2,
												  Name = "Київ",
												  Level = new PetitionLevel
												          {
													          ID = 3
												          },
												  Candidates = new List<Candidate>
												               {
													               new Candidate
					                                               {
						                                               ID = 2,
						                                               Status = EntityDictionary.Candidate.Status.Active,
						                                               FirstName = "Віталій",
						                                               MiddleName = "Володимирович",
						                                               LastName = "Кличко",
						                                               Type = EntityDictionary.Candidate.Type.PartyMember
					                                               },
																   new Candidate
					                                               {
						                                               ID = 3,
						                                               Status = EntityDictionary.Candidate.Status.Active,
						                                               FirstName = "Леся",
						                                               MiddleName = "Юріївна",
						                                               LastName = "Оробець",
						                                               Type = EntityDictionary.Candidate.Type.Independent
					                                               },
																   new Candidate
					                                               {
						                                               ID = 4,
						                                               Status = EntityDictionary.Candidate.Status.Active,
						                                               FirstName = "Дарт",
						                                               MiddleName = "Олексійович",
						                                               LastName = "Вейдер",
						                                               Type = EntityDictionary.Candidate.Type.Independent
					                                               }
												               }
											  },
											  //new Region
											  //{
											  //	ID = 3,
											  //	Name = "Харків",
											  //	Level = new PetitionLevel
											  //			{
											  //				ID = 3
											  //			},
											  //	Candidates = new List<Candidate>()
											  //},
											  //new Region
											  //{
											  //	ID = 4,
											  //	Name = "Одеса",
											  //	Level = new PetitionLevel
											  //			{
											  //				ID = 3
											  //			},
											  //	Candidates = new List<Candidate>()
											  //},
											  new Region
											  {
												  ID = 5,
												  Name = "Львів",
												  Level = new PetitionLevel
												          {
													          ID = 3
												          },
												  Candidates = new List<Candidate>
												               {
													               new Candidate
													               {
														               ID = 6,
																	   LastName = "Садовий",
																	   FirstName = "Андрій",
																	   MiddleName = "Іванович",
																	   Status = EntityDictionary.Candidate.Status.Active,
																	   Type = EntityDictionary.Candidate.Type.Independent
													               }
												               }
											  },
											  new Region
											  {
												  ID = 6,
												  Name = "АР Крим",
												  Level = new PetitionLevel
												          {
													          ID = 2
												          },
												  Candidates = new List<Candidate>
												               {
													               new Candidate
													               {
														               ID = 7,
																	   LastName = "Джамілєв",
																	   FirstName = "Мустафа",
																	   MiddleName = "Абдульджеміль",
																	   Status = EntityDictionary.Candidate.Status.Active,
																	   Type = EntityDictionary.Candidate.Type.Independent
													               }
												               }
											  }

		                                  };


		//public OperationResult<IEnumerable<Region>> GetRegions(int petitonLevelId)
		//{
		//	OperationResult<IEnumerable<Region>> result;

		//	try
		//	{
		//		using (var db = new EDEntities())
		//		{
		//			var regions = this.FakeRegions.Where(r => r.Level != null && r.Level.ID == petitonLevelId);
		//			foreach (var fakeRegion in this.FakeRegions)
		//			{
		//				// load full level info
		//			}

		//			result = OperationResult<IEnumerable<Region>>.Success(regions);
		//		}
		//	}
		//	catch (Exception exc)
		//	{
		//		result = OperationResult<IEnumerable<Region>>.ExceptionResult(exc);
		//	}

		//	return result;
		//}


		public OperationResult<IEnumerable<Region>> GetRegions(int petitonLevelId)
		{
			Func<EDEntities, OperationResult<IEnumerable<Region>>> getRegions = (db) =>
			{
				var regions = this.FakeRegions.Where(r => r.Level != null && r.Level.ID == petitonLevelId);
				foreach (var fakeRegion in this.FakeRegions)
				{
					// load full level info
				}

				var result = OperationResult<IEnumerable<Region>>.Success(regions);
				return result;
			};
			var operationResult = DbExecuter.Execute(getRegions);

			return operationResult;
		}
	}
}