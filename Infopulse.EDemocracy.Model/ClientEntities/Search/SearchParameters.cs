namespace Infopulse.EDemocracy.Model.ClientEntities.Search
{
	public class SearchParameters
	{
		public int? PageNumber { get; set; } = 1;
		public int? PageSize { get; set; } = 50;
		public string OrderBy { get; set; } = "ID";
		public string OrderDirection { get; set; } = "ASC";
	}
}