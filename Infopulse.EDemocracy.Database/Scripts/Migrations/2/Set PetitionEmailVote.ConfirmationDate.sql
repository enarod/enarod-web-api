update dbo.PetitionEmailVote
set ConfirmationDate = getdate()
where IsConfirmed = 1