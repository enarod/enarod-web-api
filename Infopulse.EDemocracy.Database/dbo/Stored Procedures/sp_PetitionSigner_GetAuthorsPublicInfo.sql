create procedure [dbo].[sp_PetitionSigner_GetAuthorsPublicInfo]
	@List dbo.IntList readonly
as
select
	ps.ID,
	ps.FirstName,
	ps.MiddleName,
	ps.LastName,
	null as Email,
	null as AddressLine1,
	null as AddressLine2,
	null as City,
	null as Country,
	null as Region,

	null as CreatedBy,
	ps.CreatedDate,
	null as ModifiedBy,
	null as ModifiedDate
from dbo.PetitionSigner ps
where ps.ID in (select Number from @List)