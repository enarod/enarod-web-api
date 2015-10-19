namespace Infopulse.EDemocracy.Model.ClientEntities.Search
{
	public class SearchParameters
	{
		public int? PageNumber { get; set; } = SearchParameters.DefaultPageNumber;
		public int? PageSize { get; set; } = SearchParameters.DefaultPageSize;
		public string OrderBy { get; set; } = SearchParameters.DefaultOrderBy;
		public string OrderDirection { get; set; } = SearchParameters.DefaultOrderDirection;

		public static int DefaultPageNumber { get; set; } = 1;
		public static int DefaultPageSize { get; set; } = 50;
		public static string DefaultOrderBy { get; set; } = "ID";
		public static string DefaultOrderDirection { get; set; } = "ASC";

		public void SetEmptyValues()
		{
			if (!this.PageSize.HasValue) this.PageSize = SearchParameters.DefaultPageSize;
			if (!this.PageNumber.HasValue) this.PageNumber = SearchParameters.DefaultPageNumber;
			if (string.IsNullOrWhiteSpace(this.OrderBy)) this.OrderBy = SearchParameters.DefaultOrderBy;
			if (string.IsNullOrWhiteSpace(this.OrderDirection)) this.OrderDirection = SearchParameters.DefaultOrderDirection;
		}
	}
}