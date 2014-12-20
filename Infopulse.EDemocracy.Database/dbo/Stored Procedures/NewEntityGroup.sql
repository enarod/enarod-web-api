
create procedure NewEntityGroup @ParentID bigint, @Name varchar(255)
as
begin
	if not exists(select ID from EntityGroup where UPPER(Name) = UPPER(@Name))
		insert into EntityGroup
		select @ParentID, @Name
end