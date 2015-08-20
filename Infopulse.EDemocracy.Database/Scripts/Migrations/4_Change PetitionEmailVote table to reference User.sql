alter table dbo.PetitionEmailVote
add VoterID int null
go

select *
from dbo.PetitionEmailVote pev
left join dbo.PetitionSigner ps on ps.ID = pev.PetitionSignerID
left join auth.App_User u on u.Email = ps.Email

update pev
set pev.VoterID = u.Id
from dbo.PetitionEmailVote pev
	join dbo.PetitionSigner ps on ps.ID = pev.PetitionSignerID
	join auth.App_User u on u.Email = ps.Email
go

delete pev
from dbo.PetitionEmailVote pev
where VoterID is null
go

alter table dbo.PetitionEmailVote
alter column VoterID int not null
go

alter table dbo.PetitionEmailVote
add constraint FK_PetitionEmailVote_User foreign key (VoterID) references auth.App_User(ID)
go

alter table dbo.PetitionEmailVote
drop constraint FK_PetitionEmailVote_PetitionSigner
go

--alter table dbo.PetitionEmailVote
--drop column PetitionSignerID
go

select * from dbo.PetitionEmailVote