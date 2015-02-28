namespace Infopulse.EDemocracy.Model.ClientEntities.Search
{
	public class SearchParameters
	{
		public int? PageNumber { get; set; }
		public int? PageSize { get; set; }
		public string OrderBy { get; set; }
		public string OrderDirection { get; set; }
	}
}