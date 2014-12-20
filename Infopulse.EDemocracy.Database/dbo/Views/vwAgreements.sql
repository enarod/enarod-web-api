CREATE VIEW [dbo].[vwAgreements]
	AS 
	SELECT a.ID, a.Name, a.ShortDesc, a.Description, count(av.ID) NumberOfVotes
	  FROM Agreement a
	  LEFT JOIN AgreementVote av on a.ID = av.AgreementID
	GROUP BY a.ID, a.Name, a.ShortDesc, a.Description