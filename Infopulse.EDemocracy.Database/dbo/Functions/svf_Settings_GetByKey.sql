CREATE FUNCTION [dbo].[svf_Settings_GetByKey]
(
	@Key nvarchar(max)
)
RETURNS nvarchar(max)
AS
BEGIN
	declare @Value nvarchar(max)

	select top 1 @Value = s.Value
	from dbo.Settings s
	where s.[Key] = @Key

	return @Value
END
