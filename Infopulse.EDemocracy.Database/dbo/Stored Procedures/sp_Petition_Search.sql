CREATE PROCEDURE [dbo].[sp_Petition_Search]
	@SearchText nvarchar(max) = null,
	@KeyWordText nvarchar(max) = null,
	@Category nvarchar(max) = null,
	@CategoryIDs dbo.IntList readonly,
	@Organization nvarchar(max) = null,
	@OrganizationID int = null,

	@ShowActivePetitions bit = 1,
	@ShowInactivePetitions bit = 0,

	@SearchInPetitions bit = 1,
	@SearchInOrganizations bit = 0,
	@SearchInCategories bit = 0,

	@CreatedDateStart datetime2 = null,
	@CreatedDateEnd datetime2 = null,
	@FinishDateStart datetime2 = null,
	@FinishDateEnd datetime2 = null,

	@PageNumber int = 1,
	@PageSize int = 50,
	@OrderBy nvarchar(max) = 'CreatedDate'

AS
set nocount on
declare
	@Now datetime2 = getutcdate()
	
begin
	if @PageNumber is null or @PageNumber < 1
	set @PageNumber = 1

	if @PageSize is null or @PageSize < 1
	set @PageSize = 50

	if @ShowActivePetitions is null
	set @ShowActivePetitions = 1

	if @ShowInactivePetitions is null
	set @ShowInactivePetitions = 0

	if (@SearchInPetitions = 0 or @SearchInPetitions is null)
		and (@SearchInOrganizations = 0 or @SearchInOrganizations is null)
		and (@SearchInCategories = 0 or @SearchInCategories is null)
		and @SearchText is not null and len(ltrim(rtrim(@Searchtext))) <> 0
	set @SearchInPetitions = 1

	if @SearchText is not null and len(ltrim(rtrim(@Searchtext))) = 0
	set @SearchText = null

	print 'Page number: ' + isnull(cast(@PageNumber as varchar(max)), 'none')
	print 'Page size: ' + isnull(cast(@PageSize as varchar(max)), 'none')

	print 'Show active petitions: ' + case when @ShowActivePetitions = 1 then 'yes' else 'no' end
	print 'Show inactive petitions: ' + case when @ShowInactivePetitions = 1 then 'yes' else 'no' end

	select
		 p.[ID]
		,p.[LevelID]
		,p.[AddressedTo]
		,p.[Subject]
		,p.[CategoryID]
		,p.[Text]
		,p.[Requirements]
		,p.[KeyWords]
		,p.[CreatedDate]
		,p.[CreatedBy]
		,p.[IssuerID]
		,p.[EffectiveFrom]
		,p.[EffectiveTo]
		,p.[RequiredVotesNumber] as [Limit]
		,p.[Email]
		,p.[OrganizationID]
		,p.VotesCount
	from dbo.vPetitionWithVote p
	
	where
		(
			(@ShowActivePetitions = 1 and p.IsActive = 1)
			or
			(@ShowInactivePetitions = 1 and p.IsActive = 0)
		)
		and
		(
			@SearchText is null
			or
			(
				(@SearchInPetitions = 1 and charindex(@SearchText, p.[Subject]) > 0)
				or
				(@SearchInPetitions = 1 and charindex(@SearchText, p.[Text]) > 0)
				or
				(@SearchInPetitions = 1 and charindex(@SearchText, p.Requirements) > 0)
				or
				(@SearchInPetitions = 1 and charindex(@SearchText, p.KeyWords) > 0)
				or
				(@SearchInOrganizations = 1 and charindex(@SearchText, p.OrganizationName ) > 0)
				or
				(@SearchInCategories = 1 and charindex(@SearchText, p.Category) > 0)
			)
		)
		and
		(
			@KeyWordText is null
			or exists(select null from [dbo].[tvf_SplitString](p.KeyWords, ',') kw where kw.Word = @KeyWordText)
		)
		and
		(
			@Category is null
			or charindex(@Category, p.Category ) > 0
		)
		and
		(
			not exists(select null from @CategoryIDs)
			or p.CategoryID in (select Number from @CategoryIDs)
		)
		and
		(
			@Organization is null
			or charindex(@Organization, p.OrganizationName ) > 0
		)
		and
		(
			@OrganizationID is null
			or p.OrganizationID = @OrganizationID
		)
		and
		(
			@CreatedDateStart is null
			or datediff(day, @CreatedDateStart, p.EffectiveFrom) >= 0
		)
		and
		(
			@CreatedDateEnd is null
			or datediff(day, p.EffectiveFrom, @CreatedDateEnd) >= 0
		)
		and
		(
			@FinishDateStart is null
			or datediff(day, @FinishDateStart, p.EffectiveTo) >= 0
		)
		and
		(
			@FinishDateEnd is null
			or datediff(day, p.EffectiveTo, @FinishDateEnd) >= 0
		)
	order by
		case @OrderBy
			when 'CreatedDate' then cast(p.CreatedDate as nvarchar(max))
			else null
		end asc,
		case @OrderBy
			when 'CreatedDate DESC' then cast(p.CreatedDate as nvarchar(max))
			else null
		end desc,
		-- TODO: add other sort columns
	
		p.CreatedDate
		
	offset @PageSize * (@PageNumber - 1) rows
	fetch next @PageSize rows only
end
/**********************************************************************************
declare @start datetime = getdate()

exec [dbo].[sp_Petition_GetAll]
	@PetitionID = null,
	@ShowPreliminaryPetitions = 1,
	@SearchText = null,
	@KeyWordText = null,
	@Category = null

select datediff(ms, @start, getdate()) as 'Duration, ms'
**********************************************************************************/