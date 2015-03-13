CREATE VIEW [dbo].[vPetitionWithVote]
AS
	with cte_emailVotes as
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
	cte_petitionsInfo as
	(
		select
			p.*,
			(isnull(cv.VotesCount, 0) + isnull(ev.VotesCount, 0)) as VotesCount,
			coalesce(o.PreliminaryVoteCount, cast(dbo.svf_Settings_GetByKey('ActivePetitionVoteCount') as int)) as ActiveVotesNumber,
			coalesce(o.VoteCount, p.Limit) as RequiredVotesNumber,
			e.[Description] as Category,
			o.Name as OrganizationName
		from dbo.Petition p
		left join cte_certVotes cv on cv.PetitionID = p.ID
		left join cte_emailVotes ev on ev.PetitionID = p.ID
		left join dbo.Organization o on o.ID = p.OrganizationID
		join dbo.Entity e on e.ID = p.CategoryID
		join dbo.PetitionLevel pl on pl.ID = p.LevelID
		where
			e.EntityGroupID = 6
	),
	cte_petitions as
	(
		select
			p.*,
			case
				when p.VotesCount >= p.ActiveVotesNumber
				then cast(1 as bit) else cast(0 as bit) end as IsActive,
			case when exists(
				select null
				from dbo.PetitionEmailVote pev
				join dbo.PetitionSigner ps on ps.ID = pev.PetitionSignerID
				where ps.Email = p.Email)
				then cast(1 as bit)
				else cast(0 as bit) end as IsConfirmed
		from cte_petitionsInfo p
	)
	select
		p.*
	from cte_Petitions p
	where
		p.IsConfirmed = 1