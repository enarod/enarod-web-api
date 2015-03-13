namespace Infopulse.EDemocracy.Model.ClientEntities.Search
{
	public class SearchParameters
	{
		public int? PageNumber { get; set; }
		public int? PageSize { get; set; }
		public string OrderBy { get; set; }
		public string OrderDirection { get; set; }

		public static SearchParameters Default
		{
			get
			{
				return new SearchParameters
				{
					PageNumber = 1,
					PageSize = 50,
					OrderBy = "ID",
					OrderDirection = "ASC"
				};
			}
		}
	}
}