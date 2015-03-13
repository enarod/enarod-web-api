CREATE PROCEDURE [dbo].[sp_Petition_GetAll]
	@PetitionID int = null,
	
	@ShowActivePetitions bit = 1,
	@ShowInactivePetitions bit = 0,

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
		@PetitionID is null
		or
		p.ID = @PetitionID
			
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