CREATE PROCEDURE [dbo].[sp_Petition_GetAll]
	@PetitionID int = null,
	@ShowPreliminaryPetitions bit = 0
AS
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
			coalesce(o.PreliminaryVoteCount, p.Limit) as RequiredVotesNumber
		from dbo.Petition p
		left join cte_certVotes cv on cv.PetitionID = p.ID
		left join cte_emailVotes ev on ev.PetitionID = p.ID
		left join dbo.Organization o on o.ID = p.OrganizationID
	)

	select
		*
	from cte_petitions p
	where
		(@PetitionID is not null and p.ID = @PetitionID)
		or
		(
			@PetitionID is null
			and
			(
				(@ShowPreliminaryPetitions = 1 and p.OrganizationID is not null) -- preliminary petition linked to organization
				or
				p.VotesCount > p.RequiredVotesNumber
			)
		)
/**********************************************************************************
declare @start datetime = getdate()

exec [dbo].[sp_Petition_GetAll]
	@PetitionID = null,
	@ShowPreliminaryPetitions = 1

select datediff(ms, @start, getdate()) as 'Duration, ms'
**********************************************************************************/