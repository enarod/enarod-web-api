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
			o.Name as OrganizationName,
			case
				when exists
				(
					select null
					from dbo.PetitionEmailVote pev					
					where
						pev.VoterID = p.CreatedBy
						and pev.IsConfirmed = 1
				)
				then cast(1 as bit)
				else cast(0 as bit)
			end as IsConfirmed
		from dbo.Petition p
		left join cte_certVotes cv on cv.PetitionID = p.ID
		left join cte_emailVotes ev on ev.PetitionID = p.ID
		left join dbo.Organization o on o.ID = p.OrganizationID
		join dbo.Entity e on e.ID = p.CategoryID
		join dbo.PetitionLevel pl on pl.ID = p.LevelID
		where
			e.EntityGroupID = 6
	)
	select
		p.*,
		case
			when p.VotesCount >= p.ActiveVotesNumber
			then cast(1 as bit) else cast(0 as bit)
		end as IsActive
	from cte_petitionsInfo p
	where
		p.IsConfirmed = 1