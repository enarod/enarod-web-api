CREATE PROCEDURE [dbo].[sp_Petition_GetAll]
@PetitionID int = null
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
			(isnull(cv.VotesCount, 0) + isnull(ev.VotesCount, 0)) as VotesCount
		from dbo.Petition p
		left join cte_certVotes cv on cv.PetitionID = p.ID
		left join cte_emailVotes ev on ev.PetitionID = p.ID
	)

	select
		*
	from cte_petitions p
	where
		(@PetitionID is not null and p.ID = @PetitionID)
		or (@PetitionID is null and p.VotesCount > p.Limit)