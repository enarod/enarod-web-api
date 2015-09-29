alter table dbo.Petition
add [ApprovedBy]       INT            NULL

go

alter table dbo.Petition
add [ApprovedDate]     DATETIME       NULL

go

alter table dbo.Petition
add constraint [FK_Petition_ApprovedBy] foreign key ([ApprovedBy]) references [auth].[App_User] ([Id])
go