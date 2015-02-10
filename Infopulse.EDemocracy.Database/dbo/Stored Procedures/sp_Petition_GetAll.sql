CREATE PROCEDURE [dbo].[sp_Petition_GetAll]
	@PetitionID int = null,
	@ShowPreliminaryPetitions bit = 0,
	@SearchText nvarchar(max) = null,
	@KeyWordText nvarchar(max) = null,
	@Category nvarchar(max) = null
AS
set nocount on
declare
	@EntityGroupId_PetitionCategory int =
	(
		select top 1
			eg.ID
		from dbo.EntityGroup eg
		join dbo.EntityGroup parentEg on parentEg.ID = eg.ParentID
		where
			eg.Name = 'Category'
			and parentEg.Name = 'Petition'
	)
	

	;with cte_emailVotes as
	(
		select
			ev.PetitionID,
			count(ev.PetitionID) as VotesCount
		from dbo.PetitionEmailVote ev
		where
			ev.IsConfirmed = 1
		group by ev.PetitionID
	),
	cte_certVotes as
	(
		select
			pv.PetitionID,
			count(pv.PetitionID) as VotesCount
		from dbo.PetitionVote pv
		group by pv.PetitionID
	),
	cte_petitions as
	(
		select
			p.*,
			(isnull(cv.VotesCount, 0) + isnull(ev.VotesCount, 0)) as VotesCount,
			coalesce(o.PreliminaryVoteCount, p.Limit) as RequiredVotesNumber,
			e.[Description] as Category
		from dbo.Petition p
		left join cte_certVotes cv on cv.PetitionID = p.ID
		left join cte_emailVotes ev on ev.PetitionID = p.ID
		left join dbo.Organization o on o.ID = p.OrganizationID

		join dbo.Entity e on e.ID = p.CategoryID
		where
			e.EntityGroupID = @EntityGroupId_PetitionCategory
	)

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
		,p.[EffectiveFrom]
		,p.[EffectiveTo]
		,p.[Limit]
		,p.[Email]
		,p.[OrganizationID]
		,p.VotesCount
	from cte_petitions p
	
	where
		(@PetitionID is not null and p.ID = @PetitionID)
		or
		(
			@PetitionID is null
			and
			(
				(@ShowPreliminaryPetitions = 1 and p.OrganizationID is not null) -- preliminary petition linked to organization
				or p.VotesCount > p.RequiredVotesNumber
			)
			and
			(
				@SearchText is null
				or
				(
					@SearchText is not null
					and
					(
						charindex(@SearchText, p.[Subject]) > 0
						or
						charindex(@SearchText, p.[Text]) > 0
						or
						charindex(@SearchText, p.Requirements) > 0
						or
						charindex(@SearchText, p.KeyWords) > 0
					)
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
		)
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