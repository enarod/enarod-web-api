create procedure NewEntity @EntityGroupID bigint, @Name varchar(max), @Description nvarchar(max)
as
begin
	if not exists(select ID from Entity where UPPER(Name) = UPPER(@Name))
		insert into Entity
		select @EntityGroupID, @Name, @Description
end