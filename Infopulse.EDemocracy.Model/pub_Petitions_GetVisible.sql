CREATE PROCEDURE pub_Petitions_GetVisible	
AS
BEGIN
	;with cte_PetitionVote_Cert as
	(
		select
			p.ID as PetitionID,
			COUNT(*) AS VotesCount
		from dbo.Petition p
		JOIN dbo.PetitionVote pv ON pv.PetitionID = p.ID
		GROUP BY p.ID
	),
	cte_PetitionVote_Email as
	(
		select
			p.ID as PetitionID,
			COUNT(*) AS VotesCount
		from dbo.Petition p
		JOIN dbo.PetitionEmailVote pv ON pv.PatitionID = p.ID
		where pv.IsConfirmed = 1
		GROUP BY p.ID		
	),
	cte_Petition_Votes AS
	(
		SELECT
			p.ID as PetitionID,
			ISNULL(cv.VotesCount, 0) + ISNULL(ev.VotesCount, 0) as VotesCount
		FROM dbo.Petition p
		left join cte_PetitionVote_Cert cv ON cv.PetitionID = p.ID
		left join cte_PetitionVote_Email ev  ON ev.PetitionID = p.ID
	)
	SELECT
		p.*,
		v.VotesCount AS VoteCount
	FROM cte_Petition_Votes v
	JOIN dbo.Petition p on p.ID = v.PetitionID

END
GO
