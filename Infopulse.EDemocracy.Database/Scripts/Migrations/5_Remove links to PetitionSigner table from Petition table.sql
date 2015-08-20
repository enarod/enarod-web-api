alter table dbo.Petition
drop constraint FK_Petition_Issuer
go

alter table dbo.Petition
drop column IssuerID
go