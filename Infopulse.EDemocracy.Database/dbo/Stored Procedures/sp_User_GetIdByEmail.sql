create procedure dbo.sp_User_GetIdByEmail
	@UserEmail varchAr(255)
as
set nocount on
begin
	select u.ID
	from auth.App_User u
	where u.Email = @UserEmail
end